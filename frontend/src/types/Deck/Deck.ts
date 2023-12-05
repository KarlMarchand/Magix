import Card from "../Card";
import Faction from "../Faction";
import Hero from "../Hero";
import Talent from "../Talent";

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
