import React from "react";
import StandardChoiceTab from "@components/deck_building/standard_choice_tab/StandardChoiceTab";
import { useDeckManager } from "@context/deck_manager_context/DeckManagerContext";
import { useGameOptions } from "@context/GameOptionsProvider";

const HeroesTab: React.FC = () => {
	const { currentDeck, changeHero } = useDeckManager();
	const { heroesImages, availableHeroesList } = useGameOptions();

	const choiceList = availableHeroesList.map((hero) => {
		return {
			id: hero.id,
			name: hero.name,
			description: hero.power,
			image: heroesImages[hero.name.replace(/\s+/g, "")],
		};
	});

	const handleSelection = (id: string | number) => {
		const selectedHero = availableHeroesList.find((hero) => hero.id === id);
		if (selectedHero) changeHero(selectedHero);
	};

	return (
		<StandardChoiceTab
			id="faction-selector-tab"
			content={choiceList}
			initialChoice={
				currentDeck?.hero
					? {
							id: currentDeck.hero.id,
							name: currentDeck.hero.name,
							description: currentDeck.hero.power,
							image: heroesImages[currentDeck.hero.name.replace(/\s+/g, "")],
					  }
					: choiceList[0]
			}
			onSelection={handleSelection}
		/>
	);
};

export default HeroesTab;
