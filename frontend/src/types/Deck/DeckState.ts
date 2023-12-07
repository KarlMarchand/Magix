import Faction from "@customTypes/Faction";
import Hero from "@customTypes/Hero";
import Talent from "@customTypes/Talent";

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
	name: "",
	hero: undefined,
	talent: undefined,
	faction: undefined,
	cards: {},
	cardNumber: 0,
};
