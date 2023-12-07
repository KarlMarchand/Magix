import { createContext, useState, useEffect, useMemo, useContext } from "react";
import { convertCardArrayToRecord } from "@context/deck_manager_context/CardListConversions";
import DeckState, { DEFAULT_DECK_STATE } from "@customTypes/deck/DeckState";
import DeckOperationResult from "@customTypes/deck/DeckOperationResult";
import CreateDeck from "@customTypes/deck/CreateDeck";
import RequestHandler from "@utils/RequestHandler";
import Faction from "@customTypes/Faction";
import Deck from "@customTypes/deck/Deck";
import Talent from "@customTypes/Talent";
import Hero from "@customTypes/Hero";
import Card from "@customTypes/Card";
import { useGameOptions } from "@context/GameOptionsProvider";

interface DeckManagerContextInterface {
	setCurrentDeck: (deck?: Deck) => void;
	playerDeckList: Deck[];
	resetDeck: () => void;
	saveDeck: () => Promise<DeckOperationResult<Deck>>;
	validateDeck: () => DeckOperationResult;
	changeCurrentDeckName: (newName: string) => DeckOperationResult;
	removeCard: (card: Card) => DeckOperationResult;
	canAddCard: (card: Card) => boolean;
	addCard: (card: Card) => DeckOperationResult;
	currentDeck: DeckState;
	filteredCardList: Card[];
	deleteDeck: (deck: Deck) => Promise<DeckOperationResult<Deck>>;
	equipDeck: (deck: Deck) => Promise<DeckOperationResult<Deck>>;
	costHighlight: number | null;
	setCostHighlight: (costHighlight: number | null) => void;
	changeHero: (hero: Hero) => void;
	changeTalent: (talent: Talent) => void;
	changeFaction: (faction: Faction) => void;
	setFilter: (filter: Faction | number | null) => void;
	activeFilter: Faction | number | null;
}

const deckManagerContextDefault: DeckManagerContextInterface = {
	setCurrentDeck: (_?: Deck) => null,
	playerDeckList: [],
	resetDeck: () => null,
	saveDeck: async () => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
	validateDeck: () => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
	changeCurrentDeckName: (_: string) => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
	removeCard: (_: Card) => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
	canAddCard: (_: Card) => false,
	addCard: (_: Card) => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
	currentDeck: DEFAULT_DECK_STATE,
	filteredCardList: [],
	deleteDeck: async () => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
	equipDeck: async () => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
	costHighlight: null,
	setCostHighlight: (_: number | null) => null,
	changeHero: (_: Hero) => null,
	changeTalent: (_: Talent) => null,
	changeFaction: (_: Faction) => null,
	setFilter: (_: Faction | number | null) => null,
	activeFilter: null,
};

// Create the context
const DeckManagerContext = createContext<DeckManagerContextInterface>(deckManagerContextDefault);

// Provider component
export const DeckManagerProvider: React.FC<{ children: JSX.Element }> = ({ children }) => {
	const { availableCardsList, availableHeroesList, availableTalentsList, availableFactionsList } = useGameOptions();
	const [, _setCardList] = useState<Card[]>([]);
	const [playerDeckList, _setPlayerDeckList] = useState<Deck[]>([]);
	const [currentDeck, _setCurrentDeck] = useState<DeckState>(DEFAULT_DECK_STATE);
	const [filteredCardList, _setFilteredCardList] = useState<Card[]>([]);
	const [costHighlight, setCostHighlight] = useState<number | null>(null);
	const [activeFilter, setFilter] = useState<Faction | number | null>(null);

	useEffect(() => {
		_fetchPlayerDeckList();
	}, []);

	const _fetchPlayerDeckList = async () => {
		return RequestHandler.get<Deck[]>("deck/all").then((response) => {
			if (response.success && response.data) {
				_setPlayerDeckList(response.data);
			}
		});
	};

	useEffect(() => {
		_setFilteredCardList(availableCardsList);
	}, [availableCardsList]);

	useEffect(() => {
		if (activeFilter && typeof activeFilter === "number") {
			_filterCardsByCost(activeFilter);
		} else if (activeFilter && typeof activeFilter === "object") {
			_filterCardsByFaction(activeFilter);
		} else {
			_setFilteredCardList([...availableCardsList]);
		}
	}, [activeFilter]);

	const _filterCardsByFaction = (factionFilter: Faction): void => {
		_setFilteredCardList(
			availableCardsList.filter((card) => card.factionName === factionFilter?.name || card.factionName === null)
		);
	};

	const _filterCardsByCost = (costFilter?: number): void => {
		_setFilteredCardList(availableCardsList.filter((card) => card.cost === costFilter));
	};

	const canAddCard = (card: Card): boolean => {
		return currentDeck.cardNumber < 30 && currentDeck.cards[card.id] !== 3;
	};

	const addCard = (card: Card): DeckOperationResult => {
		const result: DeckOperationResult = { isSuccessful: true };
		if (currentDeck.cards[card.id]) {
			if (currentDeck.cards[card.id] < 3) {
				currentDeck.cards[card.id] += 1;
			} else {
				result.error = `Cannot add more than 3 of the same card: ${card.id}`;
				result.isSuccessful = false;
				return result;
			}
		} else {
			currentDeck.cards[card.id] = 1;
		}
		_setCurrentDeck({
			...currentDeck,
			cardNumber: ++currentDeck.cardNumber,
		});
		return result;
	};

	const removeCard = (card: Card): DeckOperationResult => {
		const result: DeckOperationResult = { isSuccessful: true };
		if (currentDeck.cards[card.id]) {
			currentDeck.cards[card.id] -= 1;
			_setCurrentDeck({
				...currentDeck,
				cardNumber: --currentDeck.cardNumber,
			});
			if (currentDeck.cards[card.id] <= 0) {
				delete currentDeck.cards[card.id];
			}
		} else {
			result.error = "Card not found";
			result.isSuccessful = false;
		}
		return result;
	};

	const changeCurrentDeckName = (newName: string): DeckOperationResult => {
		const result: DeckOperationResult = { isSuccessful: isNameAvailable(newName) };

		if (!result.isSuccessful) {
			result.error = "This deck name is already in use.";
		} else {
			currentDeck.name = newName;
			if (currentDeck.id) {
				const deck = playerDeckList.find((deck) => deck.id === currentDeck.id);
				if (deck?.name) {
					deck.name = newName;
					_sendDeckToServer({
						id: deck.id,
						name: deck.name,
						heroId: deck.hero.id,
						talentId: deck.talent.id,
						factionId: deck.faction.id,
						cards: deck.cards.map((card) => ({ id: card.id })),
					});
				}
			}
		}

		return result;
	};

	const isNameAvailable = (newName: string): boolean => {
		const alreadyUsed: boolean = playerDeckList.some((deck: Deck) => {
			deck.name === newName;
		});
		return !alreadyUsed;
	};

	const validateDeck = (): DeckOperationResult => {
		const result: DeckOperationResult = { isSuccessful: true };
		if (currentDeck.cardNumber !== 30) {
			result.error = "Deck needs 30 cards";
			result.isSuccessful = false;
			return result;
		}
		if (currentDeck.name === "") {
			result.error = "Deck name must be changed";
			result.isSuccessful = false;
			return result;
		}
		if (currentDeck.hero == null) {
			result.error = `Hero can't be empty`;
			result.isSuccessful = false;
			return result;
		}
		if (currentDeck.talent == null) {
			result.error = `Talent can't be empty`;
			result.isSuccessful = false;
			return result;
		}
		if (currentDeck.faction == null) {
			result.error = `Faction can't be empty`;
			result.isSuccessful = false;
			return result;
		}
		return result;
	};

	const saveDeck = async (): Promise<DeckOperationResult<Deck>> => {
		const validation = validateDeck();
		let result: DeckOperationResult<Deck> = { isSuccessful: validation.isSuccessful, error: validation.error };
		if (validation.isSuccessful) {
			const newDeck: CreateDeck = {
				id: currentDeck.id,
				name: currentDeck.name!,
				heroId: currentDeck.hero!.id,
				talentId: currentDeck.talent!.id,
				factionId: currentDeck.faction!.id,
				cards: Object.entries(currentDeck.cards).flatMap(([id, quantity]) =>
					Array(quantity).fill({ id: Number(id) })
				),
			};
			result = await _sendDeckToServer(newDeck);
			if (result.isSuccessful) {
				await _fetchPlayerDeckList();
			}
		}
		return result;
	};

	const _sendDeckToServer = async (deck: CreateDeck): Promise<DeckOperationResult<Deck>> => {
		const result: DeckOperationResult<Deck> = {
			isSuccessful: true,
		};
		const response = deck.id
			? await RequestHandler.put<Deck>("deck", {
					id: deck.id,
					...deck,
			  })
			: await RequestHandler.post<Deck>("deck", deck);
		result.isSuccessful = response.success;
		result.data = response.data ? response.data : undefined;
		result.error = response.message;
		return result;
	};

	const resetDeck = (): void => {
		_setCurrentDeck({
			...currentDeck,
			cards: {},
			cardNumber: 0,
		});
	};

	const setCurrentDeck = (deck?: Deck): void => {
		if (!deck) {
			_setCurrentDeck({
				...DEFAULT_DECK_STATE,
				name: "New Deck",
			});
		} else {
			_setCurrentDeck({
				...deck,
				cardNumber: deck.cards.length,
				cards: convertCardArrayToRecord(deck.cards),
			});
		}
	};

	const deleteDeck = async (deck: Deck): Promise<DeckOperationResult<Deck>> => {
		const result: DeckOperationResult<Deck> = {
			isSuccessful: true,
		};
		const response = await RequestHandler.delete<Deck>(`deck/${deck.id}`);
		result.isSuccessful = response.success;
		if (response.success && response.data) {
			result.data = response.data;
			if (result.isSuccessful) {
				await _fetchPlayerDeckList();
			}
		}
		result.error = response.message;
		return result;
	};

	const equipDeck = async (deck: Deck): Promise<DeckOperationResult<Deck>> => {
		const result: DeckOperationResult<Deck> = {
			isSuccessful: true,
		};
		const response = await RequestHandler.post<Deck>(`deck/switch/${deck.id}`, {});
		result.isSuccessful = response.success;
		if (response.success && response.data) {
			result.data = response.data;
			if (result.isSuccessful) {
				await _fetchPlayerDeckList();
			}
		}
		result.error = response.message;
		return result;
	};

	const changeHero = (hero: Hero) => {
		_setCurrentDeck({
			...currentDeck,
			hero,
		});
	};

	const changeTalent = (talent: Talent) => {
		_setCurrentDeck({
			...currentDeck,
			talent,
		});
	};

	const changeFaction = (faction: Faction) => {
		_setCurrentDeck({
			...currentDeck,
			faction,
		});
	};

	const value = useMemo(
		() => ({
			playerDeckList,
			availableTalentsList,
			availableHeroesList,
			availableCardsList,
			availableFactionsList,
			currentDeck,
			filteredCardList,
			costHighlight,
			setCurrentDeck,
			resetDeck,
			saveDeck,
			validateDeck,
			changeCurrentDeckName,
			removeCard,
			canAddCard,
			addCard,
			setFilter,
			activeFilter,
			deleteDeck,
			equipDeck,
			setCostHighlight,
			changeHero,
			changeTalent,
			changeFaction,
		}),
		[
			playerDeckList,
			availableTalentsList,
			availableHeroesList,
			availableCardsList,
			availableFactionsList,
			currentDeck,
			filteredCardList,
			costHighlight,
			activeFilter,
		]
	);

	return <DeckManagerContext.Provider value={value}>{children}</DeckManagerContext.Provider>;
};

export const useDeckManager = () => {
	return useContext(DeckManagerContext);
};
