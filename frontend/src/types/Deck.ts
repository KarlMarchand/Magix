import RequestHandler from "../utils/requestHandler";
import Card from "./Card";
import Faction from "./Faction";
import Hero from "./Hero";
import Talent from "./Talent";

type Deck = {
	id?: string;
	name: string;
	hero: Hero;
	talent: Talent;
	faction: Faction;
	active: boolean;
	cards: Card[];
};
export default Deck;

type CreateDeckDto = {
	id?: string;
	name: string;
	heroId: number;
	talentId: number;
	factionId: number;
	cards: number[];
};

export type DeckOperationResult = {
	isSuccessful: boolean;
	error?: string;
	data?: any;
};

export class DeckBuilder {
	id?: string;
	name?: string;
	hero?: Hero;
	talent?: Talent;
	faction?: Faction;
	cards: Record<number, number>;
	cardNumber: number;

	constructor(deck?: Deck) {
		this.cards = {};
		this.cardNumber = 0;
		if (deck) {
			this.id = deck.id;
			this.name = deck.name;
			this.hero = deck.hero;
			this.talent = deck.talent;
			this.faction = deck.faction;
			deck.cards.forEach((card) => this.addCard(card));
		}
		this.name = "New Deck";
	}

	canAddCard = (card: Card): boolean => {
		return this.cardNumber !== 30 && this.cards[card.id] < 3;
	};

	addCard = (card: Card): DeckOperationResult => {
		const result: DeckOperationResult = { isSuccessful: true };
		if (this.cards[card.id]) {
			if (this.cards[card.id] < 3) {
				this.cards[card.id] += 1;
			} else {
				result.error = `Cannot add more than 3 of the same card: ${card.id}`;
				result.isSuccessful = false;
				return result;
			}
		} else {
			this.cards[card.id] = 1;
		}
		this.cardNumber += 1;
		return result;
	};

	removeCard = (card: Card): DeckOperationResult => {
		const result: DeckOperationResult = { isSuccessful: true };
		if (this.cards[card.id]) {
			this.cards[card.id] -= 1;
			this.cardNumber -= 1;
			if (this.cards[card.id] <= 0) {
				delete this.cards[card.id];
			}
		} else {
			result.error = "Card not found";
			result.isSuccessful = false;
		}
		return result;
	};

	validate = (): DeckOperationResult => {
		const result: DeckOperationResult = { isSuccessful: true };
		if (this.cardNumber !== 30) {
			result.error = "Deck needs 30 cards";
			result.isSuccessful = false;
			return result;
		}
		for (const property of Object.keys(this)) {
			if (this[property as keyof DeckBuilder] === undefined) {
				result.error = `${property} can't be empty`;
				result.isSuccessful = false;
				break;
			}
		}
		return result;
	};

	save = async (): Promise<DeckOperationResult> => {
		const result: DeckOperationResult = this.validate();
		if (result.isSuccessful) {
			const body: CreateDeckDto = {
				name: this.name!,
				heroId: this.hero!.id,
				talentId: this.talent!.id,
				factionId: this.faction!.id,
				cards: Object.entries(this.cards).flatMap(([id, quantity]) => Array(quantity).fill(Number(id))),
			};
			// const response = this.id
			// 	? await RequestHandler.put<Deck>("deck", {
			// 			id: this.id,
			// 			...body,
			// 	  })
			// 	: await RequestHandler.post<Deck>("deck", body);
			// result.isSuccessful = response.success;
			// result.data = response.data;
			// result.error = response.message;
		}
		return result;
	};

	reset = (): void => {
		this.cards = {};
		this.hero = undefined;
		this.faction = undefined;
		this.talent = undefined;
		this.cardNumber = 0;
	};
}
