import { createContext, useState, useEffect, useMemo, useContext } from "react";
import factionsImagery from "@context/deck_manager_context/imageries/factionsImagery";
import talentsImagery from "@context/deck_manager_context/imageries/talentsImagery";
import heroesImagery from "@context/deck_manager_context/imageries/heroesImagery";
import AvailableDeckOptions from "@customTypes/AvailableDeckOptions";
import RequestHandler from "@utils/RequestHandler";
import Faction from "@customTypes/Faction";
import Talent from "@customTypes/Talent";
import Hero from "@customTypes/Hero";
import Card from "@customTypes/Card";

interface GameOptionsContextInterface {
	availableTalentsList: Talent[];
	availableHeroesList: Hero[];
	availableCardsList: Card[];
	availableFactionsList: Faction[];
	factionsImages: Record<string, { symbol: string; image: string }>;
	heroesImages: Record<string, string>;
	talentsImages: Record<string, string>;
}

const GameOptionsContextDefault: GameOptionsContextInterface = {
	availableTalentsList: [],
	availableHeroesList: [],
	availableCardsList: [],
	availableFactionsList: [],
	factionsImages: {},
	heroesImages: {},
	talentsImages: {},
};

// Create the context
const GameOptionsContext = createContext<GameOptionsContextInterface>(GameOptionsContextDefault);

// Provider component
export const GameOptionsProvider: React.FC<{ children: JSX.Element }> = ({ children }) => {
	const [availableCardsList, _setCardList] = useState<Card[]>([]);
	const [availableHeroesList, _setHeroList] = useState<Hero[]>([]);
	const [availableTalentsList, _setTalentList] = useState<Talent[]>([]);
	const [availableFactionsList, _setFactionList] = useState<Faction[]>([]);

	useEffect(() => {
		RequestHandler.get<AvailableDeckOptions>("deck/options/all").then((response) => {
			if (response.success && response.data) {
				_setCardList(response.data.cards);
				_setHeroList(response.data.heroes);
				_setTalentList(response.data.talents);
				_setFactionList(response.data.factions);
			}
		});
	}, []);

	const factionsImages = factionsImagery;
	const heroesImages = heroesImagery;
	const talentsImages = talentsImagery;

	const value = useMemo(
		() => ({
			availableTalentsList,
			availableHeroesList,
			availableCardsList,
			availableFactionsList,
			factionsImages,
			heroesImages,
			talentsImages,
		}),
		[
			availableTalentsList,
			availableHeroesList,
			availableCardsList,
			availableFactionsList,
			factionsImages,
			heroesImages,
			talentsImages,
		]
	);

	return <GameOptionsContext.Provider value={value}>{children}</GameOptionsContext.Provider>;
};

export const useGameOptions = () => {
	return useContext(GameOptionsContext);
};
