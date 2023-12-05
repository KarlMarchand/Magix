import { createContext, useState, useEffect, useMemo, useContext } from "react";
import RequestHandler from "@utils/RequestHandler";
import Deck from "@customTypes/Deck/Deck";
import Faction from "@customTypes/Faction";
import Hero from "@customTypes/Hero";
import Talent from "@customTypes/Talent";
import Card from "@customTypes/Card";
import AvailableDeckOptions from "@customTypes/AvailableDeckOptions";
import { convertCardArrayToRecord } from "./CardListConversions";
import DeckOperationResult from "@customTypes/Deck/DeckOperationResult";
import DeckState, { DEFAULT_DECK_STATE } from "@customTypes/Deck/DeckState";
import CreateDeck from "@customTypes/Deck/CreateDeck";

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
		const result: DeckOperationResult<Deck> = { isSuccessful: validation.isSuccessful, error: validation.error };
		if (result.isSuccessful) {
			const body: CreateDeck = {
				name: currentDeck.name!,
				heroId: currentDeck.hero!.id,
				talentId: currentDeck.talent!.id,
				factionId: currentDeck.faction!.id,
				cards: Object.entries(currentDeck.cards).flatMap(([id, quantity]) => Array(quantity).fill(Number(id))),
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
		}
		return Promise.resolve(result);
	};

	const resetDeck = (): void => {
		_setCurrentDeck((current) => ({
			...DEFAULT_DECK_STATE,
			id: current.id,
			name: current.name,
		}));
	};

	const setCurrentDeck = (deck?: Deck): void => {
		if (!deck) {
			_setCurrentDeck(DEFAULT_DECK_STATE);
		} else {
			_setCurrentDeck({
				...deck,
				cardNumber: deck.cards.length,
				cards: convertCardArrayToRecord(deck.cards),
			});
		}
	};

	const value = useMemo(
		() => ({
			playerDeckList,
			availableTalentsList,
			availableHeroesList,
			availableCardsList,
			availableFactionsList,
			currentDeck,
			setCurrentDeck,
			resetDeck,
			saveDeck,
			validateDeck,
			changeCurrentDeckName,
			removeCard,
			canAddCard,
			addCard,
		}),
		[
			playerDeckList,
			availableTalentsList,
			availableHeroesList,
			availableCardsList,
			availableFactionsList,
			currentDeck,
		]
	);

	return <DeckManagerContext.Provider value={value}>{children}</DeckManagerContext.Provider>;
};

export const useDeckManager = () => {
	return useContext(DeckManagerContext);
};
