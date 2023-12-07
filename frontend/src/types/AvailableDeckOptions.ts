import Card from "@customTypes/Card";
import Hero from "@customTypes/Hero";
import Talent from "@customTypes/Talent";
import Faction from "@customTypes/Faction";

type AvailableDeckOptions = {
	cards: Card[];
	talents: Talent[];
	heroes: Hero[];
	factions: Faction[];
};

export default AvailableDeckOptions;
