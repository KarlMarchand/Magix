import Card from "@customTypes/Card";
import React, { createContext, useState, useContext, useEffect, useMemo } from "react";
import GameState from "@customTypes/game/GameState";
import { useGameOptions } from "./GameOptionsProvider";

export const GameManagerProvider: React.FC<{ children: JSX.Element }> = ({ children }) => {
	const { availableCardsList } = useGameOptions();
	const [selectedCard, setSelectedCard] = useState<Card | null>(null);
	const [creditCount, _setCreditCount] = useState<number>(0);
	const [opponent, _setOpponent] = useState<GameState>();
	const [gameState, _setGameState] = useState<GameState>();

	// functions
	const onCardDragFailed = () => {
		// The card drag has stopped
	};

	const onCardDragStart = (card: Card) => {
		// Highlight the available options
	};

	const playCard = async (card: Card, isEnnemy: boolean) => {
		const moveIsValid = true;
		return Promise.resolve(moveIsValid);
	};

	const _adjustOpponentHand = (newSize: number) => {
		let cards = [...(opponent?.hand ?? [])];
		const sizeDifference = newSize - (opponent?.handSize ?? 0);

		if (sizeDifference > 0) {
			// If newSize is larger, add cards
			cards = cards.concat(Array(sizeDifference).fill(availableCardsList[0]));
		} else {
			// If newSize is smaller, remove cards from the end
			cards = cards.slice(0, newSize);
		}

		return cards;
	};

	const value = useMemo(
		() => ({
			selectedCard,
			setSelectedCard,
			creditCount,
			onCardDragFailed,
			onCardDragStart,
			playCard,
			opponent,
		}),
		[selectedCard]
	);

	return <GameManagerContext.Provider value={value}>{children}</GameManagerContext.Provider>;
};

interface GameManagerInterface {
	selectedCard: Card | null;
	setSelectedCard: (card: Card) => void;
	creditCount: number;
	onCardDragFailed: () => void;
	onCardDragStart: (card: Card) => void;
	playCard: (card: Card, isEnnemy: boolean) => Promise<boolean>;
	opponent: GameState | undefined;
}

const GameManagerDefault: GameManagerInterface = {
	selectedCard: null,
	setSelectedCard: (card: Card) => null,
	creditCount: 0,
	onCardDragFailed: () => null,
	onCardDragStart: (card: Card) => null,
	playCard: (card: Card, isEnnemy: boolean) => Promise.resolve(false),
	opponent: undefined,
};

const GameManagerContext = createContext(GameManagerDefault);

const useGameManager = () => useContext(GameManagerContext);

export default useGameManager;
