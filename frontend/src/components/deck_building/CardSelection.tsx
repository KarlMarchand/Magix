import React from "react";
import { useDeckManager } from "@context/deck_manager_context/DeckManagerContext";
import GameCard from "@components/game/game_card/GameCard";
import CardType from "@customTypes/Card";
import { useGameOptions } from "@context/GameOptionsProvider";

interface CardSelectionProps extends React.HTMLProps<HTMLDivElement> {
	card: CardType;
}

const CardSelection: React.FC<CardSelectionProps> = ({ card, className, ...props }) => {
	const { currentDeck, addCard, removeCard, canAddCard } = useDeckManager();
	const { availableFactionsList } = useGameOptions();

	const changeQty = (modifier: number) => {
		if (modifier > 0) {
			addCard(card);
		} else {
			removeCard(card);
		}
	};

	return (
		<div
			className={`d-flex flex-column align-items-center justify-content-center mb-5${className ? className : ""}`}
			{...props}
		>
			<GameCard
				card={card}
				playerFaction={currentDeck.faction?.name ?? availableFactionsList[0].name}
				isStatic={true}
			/>
			{card.id != 1 && (
				<div className="d-flex justify-content-center align-items-center w-100">
					<button
						className="custom-btn px-3 py-1"
						onClick={() => changeQty(-1)}
						disabled={currentDeck.cards[card.id] === 0 || !currentDeck.cards[card.id]}
					>
						-
					</button>
					<span className="mx-3">{currentDeck.cards[card.id] ?? 0}</span>
					<button className="custom-btn px-3 py-1" onClick={() => changeQty(1)} disabled={!canAddCard(card)}>
						+
					</button>
				</div>
			)}
		</div>
	);
};

export default CardSelection;
