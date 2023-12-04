import Card from "./Card";
import Hero from "./Hero";
import Talent from "./Talent";
import Faction from "./Faction";

type AvailableDeckOptions = {
	cards: Card[];
	talents: Talent[];
	heroes: Hero[];
	factions: Faction[];
};

export default AvailableDeckOptions;
