import Deck from "@customTypes/deck/Deck";

type GameResult = {
	id: string;
	date: Date;
	won: boolean;
	opponent: string;
	deck?: Deck;
};

export default GameResult;
