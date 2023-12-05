type CreateDeck = {
	id?: string;
	name: string;
	heroId: number;
	talentId: number;
	factionId: number;
	cards: number[];
};

export default CreateDeck;
