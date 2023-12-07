import Card from "@customTypes/Card";
import Faction from "@customTypes/Faction";
import Hero from "@customTypes/Hero";
import Talent from "@customTypes/Talent";

type Deck = {
	id?: string;
	name: string;
	hero: Hero;
	talent: Talent;
	faction: Faction;
	active: boolean;
	cards: Card[];
};
export default Deck;
