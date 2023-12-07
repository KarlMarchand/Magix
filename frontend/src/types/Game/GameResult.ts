import Deck from "@customTypes/Deck/Deck";

type GameResult = {
	id: string;
	date: Date;
	won: boolean;
	opponent: string;
	deck?: Deck;
};

export default GameResult;
