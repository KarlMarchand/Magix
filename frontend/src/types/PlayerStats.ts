import Card from "@customTypes/Card";

type PlayerStats = {
	gamePlayed: number;
	wins?: number;
	loses?: number;
	ratioWins?: number;
	topCards: Card[];
};

export default PlayerStats;
