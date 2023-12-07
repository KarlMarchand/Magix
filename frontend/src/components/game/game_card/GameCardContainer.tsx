import React from "react";
import GameCard, { CardProps } from "@components/game/game_card/GameCard";
import Card from "@customTypes/Card";

interface GameCardContainerInterface extends Omit<CardProps, "card"> {
	cards: Card[];
}

const GameCardContainer: React.FC<GameCardContainerInterface> = ({ cards, ...cardProps }) => {
	return (
		<>
			{cards.map((card) => {
				return <GameCard {...cardProps} card={card} />;
			})}
		</>
	);
};

export default GameCardContainer;
