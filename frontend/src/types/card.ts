export type Card = {
	Id: number;
	Cost: number;
	Hp: number;
	Atk: number;
	Mechanics?: string[];
	Dedicated?: string;
	CardNa: string;
	Sound?: string;
	Uid?: number;
	BaseHP?: number;
	State?: string;
	FactionName?: string;
};
