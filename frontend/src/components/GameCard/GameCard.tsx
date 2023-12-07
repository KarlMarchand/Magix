import Card from "@customTypes/Card";
import React from "react";

interface CardProps extends React.HTMLAttributes<HTMLDivElement> {
	card: Card;
}

const GameCard: React.FC<CardProps> = ({ card, ...props }) => {
	return <div {...props}>{card.cardName}</div>;
};

export default GameCard;
