import React from "react";
import StandardChoiceTab from "@components/DeckBuilding/StandardChoiceTab/StandardChoiceTab";
import { useDeckManager } from "@context/DeckManagerContext/DeckManagerContext";

const FactionsTab: React.FC = () => {
	const { currentDeck, factionsImages, availableFactionsList } = useDeckManager();

	const choiceList = availableFactionsList.map((faction) => {
		return {
			id: faction.id,
			name: faction.name,
			description: faction.description,
			image: factionsImages[faction.name?.toLowerCase()]?.image,
		};
	});

	const handleSelection = (id: string | number) => {
		const selectedFaction = availableFactionsList.find((faction) => faction.id === id);
		currentDeck.faction = selectedFaction;
		console.log(currentDeck.faction?.name);
	};

	return (
		<StandardChoiceTab
			id="faction-selector-tab"
			content={choiceList}
			initialChoice={
				currentDeck?.faction
					? {
							id: currentDeck.faction.id,
							name: currentDeck.faction.name,
							description: currentDeck.faction.description,
							image: factionsImages[currentDeck.faction.name.toLowerCase()]?.image,
					  }
					: choiceList[0]
			}
			onSelection={handleSelection}
		/>
	);
};

export default FactionsTab;
