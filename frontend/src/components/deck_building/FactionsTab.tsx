import React from "react";
import StandardChoiceTab from "@components/deck_building/standard_choice_tab/StandardChoiceTab";
import { useDeckManager } from "@context/deck_manager_context/DeckManagerContext";
import { useGameOptions } from "@context/GameOptionsProvider";

const FactionsTab: React.FC = () => {
	const { currentDeck, changeFaction } = useDeckManager();
	const { factionsImages, availableFactionsList } = useGameOptions();

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
		if (selectedFaction) changeFaction(selectedFaction);
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
