type CreateDeck = {
	id?: string;
	name: string;
	heroId: number;
	talentId: number;
	factionId: number;
	cards: { id: number }[];
};

export default CreateDeck;
