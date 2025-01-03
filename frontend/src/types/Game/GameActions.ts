export enum GameActionsTypes {
	EndTurn = "END_TURN",
	Surrender = "SURRENDER",
	HeroPower = "HERO_POWER",
	Play = "PLAY",
	Attack = "ATTACK",
}

type BaseGameAction = {
	type: GameActionsTypes.EndTurn | GameActionsTypes.Surrender | GameActionsTypes.HeroPower;
};

type PlayGameAction = {
	type: GameActionsTypes.Play;
	uid: number;
};

type AttackGameAction = {
	type: GameActionsTypes.Attack;
	uid: number;
	targetuid: number;
};

type GameAction = BaseGameAction | PlayGameAction | AttackGameAction;

export default GameAction;
