export type User = {
	username: string;
	className: string;
	winCount: number;
	lossCount: number;
	lastLogin: Date;
	welcomeText: string | undefined;
	trophies: number;
	bestTrophyScore: number;
	token: string;
};
