import { createContext, useState, useEffect, useMemo, useContext } from "react";
import { convertCardArrayToRecord } from "@context/DeckManagerContext/CardListConversions";
import factionsImagery from "@context/DeckManagerContext/Imageries/factionsImagery";
import talentsImagery from "@context/DeckManagerContext/Imageries/talentsImagery";
import heroesImagery from "@context/DeckManagerContext/Imageries/heroesImagery";
import DeckState, { DEFAULT_DECK_STATE } from "@customTypes/Deck/DeckState";
import DeckOperationResult from "@customTypes/Deck/DeckOperationResult";
import AvailableDeckOptions from "@customTypes/AvailableDeckOptions";
import CreateDeck from "@customTypes/Deck/CreateDeck";
import RequestHandler from "@utils/RequestHandler";
import Faction from "@customTypes/Faction";
import Deck from "@customTypes/Deck/Deck";
import Talent from "@customTypes/Talent";
import Hero from "@customTypes/Hero";
import Card from "@customTypes/Card";

interface DeckManagerContextInterface {
	setCurrentDeck: (deck?: Deck) => void;
	playerDeckList: Deck[];
	availableTalentsList: Talent[];
	availableHeroesList: Hero[];
	availableCardsList: Card[];
	availableFactionsList: Faction[];
	resetDeck: () => void;
	saveDeck: () => Promise<DeckOperationResult<Deck>>;
	validateDeck: () => DeckOperationResult;
	changeCurrentDeckName: (newName: string) => DeckOperationResult;
	removeCard: (card: Card) => DeckOperationResult;
	canAddCard: (card: Card) => boolean;
	addCard: (card: Card) => DeckOperationResult;
	currentDeck: DeckState;
	filterCardsByFaction: (factionFilter: Faction | null) => void;
	filterCardsByCost: (costFilter?: number | null) => void;
	filteredCardList: Card[];
	factionsImages: Record<string, { symbol: string; image: string }>;
	heroesImages: Record<string, string>;
	talentsImages: Record<string, string>;
	deleteDeck: (deck: Deck) => Promise<DeckOperationResult<Deck>>;
	equipDeck: (deck: Deck) => Promise<DeckOperationResult<Deck>>;
}

const deckManagerContextDefault: DeckManagerContextInterface = {
	setCurrentDeck: (deck?: Deck) => null,
	playerDeckList: [],
	availableTalentsList: [],
	availableHeroesList: [],
	availableCardsList: [],
	availableFactionsList: [],
	resetDeck: () => null,
	saveDeck: async () => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
	validateDeck: () => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
	changeCurrentDeckName: (newName: string) => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
	removeCard: (card: Card) => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
	canAddCard: (card: Card) => false,
	addCard: (card: Card) => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
	currentDeck: DEFAULT_DECK_STATE,
	filterCardsByFaction: (factionFilter: Faction | null) => null,
	filterCardsByCost: (costFilter?: number | null) => null,
	filteredCardList: [],
	factionsImages: {},
	heroesImages: {},
	talentsImages: {},
	deleteDeck: async () => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
	equipDeck: async () => ({
		isSuccessful: false,
		error: "Not implemented",
	}),
};

// Create the context
const DeckManagerContext = createContext<DeckManagerContextInterface>(deckManagerContextDefault);

// Provider component
export const DeckManagerProvider: React.FC<{ children: JSX.Element }> = ({ children }) => {
	const [availableCardsList, setCardList] = useState<Card[]>([]);
	const [availableHeroesList, setHeroList] = useState<Hero[]>([]);
	const [availableTalentsList, setTalentList] = useState<Talent[]>([]);
	const [availableFactionsList, setFactionList] = useState<Faction[]>([]);
	const [playerDeckList, setPlayerDeckList] = useState<Deck[]>([]);
	const [currentDeck, _setCurrentDeck] = useState<DeckState>(DEFAULT_DECK_STATE);
	const [filteredCardList, setFilteredCardList] = useState<Card[]>([]);

	useEffect(() => {
		RequestHandler.get<Deck[]>("deck/all").then((response) => {
			if (response.success && response.data) {
				setPlayerDeckList(response.data);
			}
		});

		RequestHandler.get<AvailableDeckOptions>("deck/options/all").then((response) => {
			if (response.success && response.data) {
				setCardList(response.data.cards);
				setHeroList(response.data.heroes);
				setTalentList(response.data.talents);
				setFactionList(response.data.factions);
			}
		});
	}, []);

	useEffect(() => {
		setFilteredCardList(availableCardsList);
	}, [availableCardsList]);

	const filterCardsByFaction = (factionFilter: Faction | null): void => {
		setFilteredCardList(
			factionFilter
				? availableCardsList.filter(
						(card) => card.factionName === factionFilter?.name || card.factionName === null
				  )
				: [...availableCardsList]
		);
	};

	const filterCardsByCost = (costFilter?: number | null): void => {
		setFilteredCardList(
			costFilter !== null
				? availableCardsList.filter((card) => card.cost === costFilter)
				: [...availableCardsList]
		);
	};

	const canAddCard = (card: Card): boolean => {
		return currentDeck.cardNumber !== 30 && currentDeck.cards[card.id] < 3;
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
		currentDeck.cardNumber++;
		return result;
	};

	const removeCard = (card: Card): DeckOperationResult => {
		const result: DeckOperationResult = { isSuccessful: true };
		if (currentDeck.cards[card.id]) {
			currentDeck.cards[card.id] -= 1;
			currentDeck.cardNumber--;
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
					sendDeckToServer({
						id: deck.id,
						name: deck.name,
						heroId: deck.hero.id,
						talentId: deck.talent.id,
						factionId: deck.faction.id,
						cards: deck.cards.map((card) => card.id),
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
				name: currentDeck.name!,
				heroId: currentDeck.hero!.id,
				talentId: currentDeck.talent!.id,
				factionId: currentDeck.faction!.id,
				cards: Object.entries(currentDeck.cards).flatMap(([id, quantity]) => Array(quantity).fill(Number(id))),
			};
			result = await sendDeckToServer(newDeck);
		}
		return result;
	};

	const factionsImages = factionsImagery;
	const heroesImages = heroesImagery;
	const talentsImages = talentsImagery;

	const sendDeckToServer = async (body: CreateDeck): Promise<DeckOperationResult<Deck>> => {
		const result: DeckOperationResult<Deck> = {
			isSuccessful: true,
		};
		// const response = id
		// 	? await RequestHandler.put<Deck>("deck", {
		// 			id: id,
		// 			...body,
		// 	  })
		// 	: await RequestHandler.post<Deck>("deck", body);
		// result.isSuccessful = response.success;
		// result.data = response.data;
		// result.error = response.message;
		return Promise.resolve(result);
	};

	const resetDeck = (): void => {
		_setCurrentDeck((current) => ({
			...DEFAULT_DECK_STATE,
			id: current.id,
			name: current.name,
			newDeck: current.newDeck,
		}));
	};

	const setCurrentDeck = (deck?: Deck): void => {
		if (!deck) {
			_setCurrentDeck({
				...DEFAULT_DECK_STATE,
				name: "New Deck",
				newDeck: true,
			});
		} else {
			_setCurrentDeck({
				...deck,
				cardNumber: deck.cards.length,
				cards: convertCardArrayToRecord(deck.cards),
				newDeck: false,
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
		}
		result.error = response.message;
		return result;
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
			setCurrentDeck,
			resetDeck,
			saveDeck,
			validateDeck,
			changeCurrentDeckName,
			removeCard,
			canAddCard,
			addCard,
			filterCardsByFaction,
			filterCardsByCost,
			factionsImages,
			heroesImages,
			talentsImages,
			deleteDeck,
			equipDeck,
		}),
		[
			playerDeckList,
			availableTalentsList,
			availableHeroesList,
			availableCardsList,
			availableFactionsList,
			currentDeck,
			filteredCardList,
			factionsImages,
			heroesImages,
			talentsImages,
		]
	);

	return <DeckManagerContext.Provider value={value}>{children}</DeckManagerContext.Provider>;
};

export const useDeckManager = () => {
	return useContext(DeckManagerContext);
};
