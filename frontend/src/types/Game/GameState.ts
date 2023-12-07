import Card from "@customTypes/Card";

type GameState = {
	username?: string;
	remainingTurnTime?: number;
	yourTurn?: boolean;
	heroPowerAlreadyUsed?: boolean;
	hp?: number;
	mp?: number;
	maxMp?: number;
	hand: Card[];
	board: Card[];
	welcomeText?: string;
	heroClass?: string;
	remainingCardsCount?: number;
	opponent?: OpponentState;
	latestActions: LatestAction[];
	handSize?: number;
};
export default GameState;

export type OpponentState = {
	username: string;
	heroClass: string;
	hp: number;
	mp: number;
	board: Card[];
	welcomeText: string;
	remainingCardsCount: number;
	handSize: number;
};

export type LatestAction = {
	id?: number;
	from?: string;
	action?: ActionDetails;
};

export type ActionDetails = {
	type?: string;
	uid?: number;
	id?: number;
};
