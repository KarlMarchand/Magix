type Card = {
	id: number;
	cost: number;
	hp: number;
	atk: number;
	mechanics?: string[];
	dedicated?: string;
	cardName: string;
	sound?: string;
	uid?: number;
	baseHP?: number;
	state?: string;
	factionName?: string;
};

export default Card;

export const CardMechanics = {
	TAUNT: "Taunt",
	STEALTH: "Stealth",
};

export const CardConditions = {
	SLEEPING: "SLEEP",
};
