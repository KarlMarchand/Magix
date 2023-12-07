import React from "react";
import { useDeckManager } from "@context/deck_manager_context/DeckManagerContext";
import "./deckCompositionList.scss";
import { StackProps } from "react-bootstrap";
import { useGameOptions } from "@context/GameOptionsProvider";

const DeckCompositionList: React.FC<StackProps> = ({ className, ...htmlStandardProps }) => {
	const { currentDeck, costHighlight } = useDeckManager();
	const { availableCardsList } = useGameOptions();

	const handleHighlightCard = (id: string): void => {
		const node = document.getElementById("card-" + id);
		if (!node) return;

		node.scrollIntoView();

		node.classList.add("selectedCard");
		setTimeout(() => {
			node.classList.remove("selectedCard");
		}, 3000);
	};

	return (
		<div {...htmlStandardProps} className={`card-list my-2 overflow-y-scroll ${className ? className : ""}`}>
			{Object.entries(currentDeck.cards).map(([id, quantity]) => {
				const cardDetails = availableCardsList.find((card) => card.id.toString() === id);

				if (!cardDetails) {
					return null;
				}

				return (
					<div
						key={id}
						className={`card-list-line d-flex justify-content-between px-2 ${
							costHighlight === cardDetails.cost ? "cost-highlight" : ""
						}`}
						onClick={() => handleHighlightCard(id)}
					>
						<span>{cardDetails.cardName}</span>
						<span>{`${quantity}x`}</span>
					</div>
				);
			})}
		</div>
	);
};

export default DeckCompositionList;
