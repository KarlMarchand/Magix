import Card from "../../types/Card";

export const convertCardArrayToRecord = (cardsArray: Card[]): Record<number, number> => {
	const cardsRecord: Record<number, number> = {};
	cardsArray.forEach((card) => {
		cardsRecord[card.id] = (cardsRecord[card.id] || 0) + 1;
	});
	return cardsRecord;
};

export const convertRecordToCardArray = (cardsRecord: Record<number, number>, allCards: Card[]): Card[] => {
	return Object.entries(cardsRecord).flatMap(([id, quantity]) => {
		const card = allCards.find((card) => card.id === parseInt(id));
		return card ? Array(quantity).fill(card) : [];
	});
};
