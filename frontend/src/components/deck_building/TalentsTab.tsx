import React from "react";
import StandardChoiceTab from "@components/deck_building/standard_choice_tab/StandardChoiceTab";
import { useDeckManager } from "@context/deck_manager_context/DeckManagerContext";
import { useGameOptions } from "@context/GameOptionsProvider";

const TalentsTab: React.FC<React.HTMLAttributes<HTMLDivElement>> = (props) => {
	const { currentDeck, changeTalent } = useDeckManager();
	const { talentsImages, availableTalentsList } = useGameOptions();

	const choiceList = availableTalentsList.map((talent) => {
		return {
			id: talent.id,
			name: talent.name,
			description: talent.description,
			image: talentsImages[talent.name.replace(/\s+/g, "")],
		};
	});

	const handleSelection = (id: string | number) => {
		const selectedTalent = availableTalentsList.find((talent) => talent.id === id);
		if (selectedTalent) changeTalent(selectedTalent);
	};

	return (
		<StandardChoiceTab
			id="faction-selector-tab"
			content={choiceList}
			initialChoice={
				currentDeck?.talent
					? {
							id: currentDeck.talent.id,
							name: currentDeck.talent.name,
							description: currentDeck.talent.description,
							image: talentsImages[currentDeck.talent.name.replace(/\s+/g, "")],
					  }
					: choiceList[0]
			}
			onSelection={handleSelection}
		/>
	);
};

export default TalentsTab;
