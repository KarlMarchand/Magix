export enum GameType {
	Observe = "OBSERVE",
	Training = "TRAINING",
	Pvp = "PVP",
}

export enum GameMode {
	Standard = "STANDARD",
	Coop = "COOP",
	Arena = "ARENA",
}

export type GameInput = {
	type: GameType;
	mode: GameMode | null;
	privateKey: string;
};

export type GameSettings = {
	[key in GameType]: GameInput[];
};

export const gameSettings: GameSettings = {
	PVP: [
		{ type: GameType.Pvp, mode: GameMode.Standard, privateKey: "Private game key..." },
		{ type: GameType.Pvp, mode: GameMode.Coop, privateKey: "Coop game key..." },
		{ type: GameType.Pvp, mode: GameMode.Arena, privateKey: "Arena game key..." },
	],
	TRAINING: [
		{ type: GameType.Training, mode: GameMode.Coop, privateKey: "Coop training key..." },
		{ type: GameType.Training, mode: GameMode.Arena, privateKey: "Arena training key..." },
	],
	OBSERVE: [{ type: GameType.Observe, mode: null, privateKey: "Player name..." }],
};
