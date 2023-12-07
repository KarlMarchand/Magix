import React, { useEffect } from "react";
import { useDeckManager } from "@context/deck_manager_context/DeckManagerContext";
import "./cardCostBarChart.scss";
import { useGameOptions } from "@context/GameOptionsProvider";

const CardCostBarChart: React.FC<React.HTMLProps<HTMLDivElement>> = ({ className, ...htmlProps }) => {
	const { setCostHighlight, currentDeck, setFilter, activeFilter } = useDeckManager();
	const { availableCardsList } = useGameOptions();

	useEffect(() => {}, []);

	// Calculate card count per cost
	const cardCountPerCost = Array(10).fill(0); // Assuming costs range from 1 to 10
	Object.entries(currentDeck.cards).forEach(([id, quantity]) => {
		const card = availableCardsList.find((card) => card.id.toString() === id);
		if (card && card.cost >= 1 && card.cost <= 10) {
			cardCountPerCost[card.cost - 1] += quantity;
		}
	});

	// Find the max count to normalize bar heights
	const maxCount = Math.max(...cardCountPerCost, 1);

	return (
		<div {...htmlProps} className={`bar-chart ${className ? className : ""}`}>
			{cardCountPerCost.map((count, index) => {
				const cost = index + 1;
				const barHeight = (count / maxCount) * 100 + "%"; // Normalize height based on the max count

				return (
					<div
						key={cost}
						className={`deckCost my-2${cost === activeFilter ? " active" : ""}`}
						onMouseEnter={() => setCostHighlight(cost)}
						onMouseLeave={() => setCostHighlight(null)}
						onClick={() => setFilter(cost === activeFilter ? null : cost)}
					>
						<div style={{ height: barHeight }} className={"bar"}></div>
						<div>{cost}</div>
					</div>
				);
			})}
		</div>
	);
};

export default CardCostBarChart;
