import React, { useEffect } from "react";
import { useLocation } from "react-router-dom";
import { GameInput, GameMode, GameType } from "@customTypes/game/GameTypeOptions";
import LoadingScreen from "@components/loading_screen/LoadingScreen";
import useGameState from "@context/GameStateProvider";
import GameStatus from "@customTypes/game/GameStatus";
import GameOverScreen from "@components/game/game_over_screen/GameOverScreen";
import Battlefield from "@components/game/battlefield/battlefield";
import GameChat from "@components/game/game_chat/GameChat";
import "@sass/gameStyle.scss";

const GamePage: React.FC = () => {
	const { gameStatus, setIsObserving } = useGameState();
	const { type } = (useLocation().state || {
		type: GameType.Pvp,
		mode: GameMode.Standard,
	}) as GameInput;

	useEffect(() => {
		setIsObserving(type === GameType.Observe);
	}, []);

	return (
		<>
			{gameStatus === GameStatus.loading && (
				<>
					<GameChat />
					<LoadingScreen />
				</>
			)}
			{gameStatus === GameStatus.playing && (
				<>
					<GameChat />
					<Battlefield />
				</>
			)}
			{gameStatus === GameStatus.victory && <GameOverScreen isVictory={true} />}
			{gameStatus === GameStatus.defeat && <GameOverScreen isVictory={false} />}
		</>
	);
};

export default GamePage;
