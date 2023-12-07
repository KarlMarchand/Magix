import Card from "@customTypes/Card";

type GameState = {
	Username?: string;
	RemainingTurnTime?: number;
	YourTurn?: boolean;
	HeroPowerAlreadyUsed?: boolean;
	Hp?: number;
	Mp?: number;
	MaxMp?: number;
	Hand: Card[];
	Board: Card[];
	WelcomeText?: string;
	HeroClass?: string;
	RemainingCardsCount?: number;
	Opponent?: GameState;
	LatestActions: LatestAction[];
	HandSize?: number;
};
export default GameState;

export type LatestAction = {
	Id?: number;
	From?: string;
	Action?: ActionDetails;
};

export type ActionDetails = {
	Type?: string;
	Uid?: number;
	Id?: number;
};
