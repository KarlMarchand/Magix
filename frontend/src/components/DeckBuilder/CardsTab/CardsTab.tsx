import React from "react";
import { useDeckManager } from "../../../context/DeckManagerContext/DeckManagerContext";

const CardsTab: React.FC = () => {
	const { availableFactionsList } = useDeckManager();

	return (
		<div id="card-selector-tab">
			{/* <FiltersBar
				faction={availableFactionsList.map((faction) => faction.name)}
				filter={filter}
				activeFilter={activeFilter}
			/>
			<div id="card-selector">
				{cards.map((card) => {
					return (
						<CardSelection
							card={card}
							key={card.id}
							adjustDeck={adjustDeck}
							filter={
								activeFilter === null || activeFilter === customCards[card.id].faction || card.id < 2
							}
						/>
					);
				})}
			</div> */}
		</div>
	);
};

export default CardsTab;
