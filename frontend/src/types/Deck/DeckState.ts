import Faction from "../Faction";
import Hero from "../Hero";
import Talent from "../Talent";

interface DeckState {
	id?: string;
	name: string;
	hero?: Hero;
	talent?: Talent;
	faction?: Faction;
	cards: Record<number, number>;
	cardNumber: number;
}
export default DeckState;

export const DEFAULT_DECK_STATE: DeckState = {
	id: undefined,
	name: "New Deck",
	hero: undefined,
	talent: undefined,
	faction: undefined,
	cards: {},
	cardNumber: 0,
};
