enum GameStatus {
	playing,
	loading,
	victory,
	defeat,
}

export default GameStatus;

export const GameStatusConverter = (gameStatusFromServer: string): GameStatus => {
	switch (gameStatusFromServer) {
		case "LAST_GAME_WON":
			return GameStatus.victory;
		case "LAST_GAME_LOST":
			return GameStatus.defeat;
		case "WAITING":
			return GameStatus.loading;
		default:
			return GameStatus.playing;
	}
};
