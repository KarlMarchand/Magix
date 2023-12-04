import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthProvider";
import RequestHandler from "../utils/requestHandler";
import useSessionStorage from "../hooks/UseSessionStorage";
import GameStateContainer from "../types/GameStateContainer";
import Chat from "../components/Chat";
import RotatingSymbol from "../components/RotatingSymbol/RotatingSymbol";
import StartGameForm from "../components/StartGameForm";
import { GameType, GameMode } from "../types/GameTypeOptions";
import ErrorMessage from "../components/MessageBox/ErrorMessage";
import ServerResponse from "../types/ServerResponse";

const LobbyPage: React.FC = () => {
	const { logout } = useAuth();
	const navigate = useNavigate();
	const [returningPlayer, setReturningPlayer] = useSessionStorage("returningPlayer", false);
	const [_, setObservingKey] = useSessionStorage("observing", null);
	const [isNavigating, setIsNavigating] = useState(false);
	const [errorMessage, setErrorMessage] = useState<string>("");

	useEffect(() => {
		setTimeout(() => {
			if (returningPlayer) {
				// !new Audio("./assets/sounds/lobby/good_to_see_you_again.mp3").play();
			} else {
				// !new Audio("./assets/sounds/lobby/c3po_and_r2d2_at_your_service.mp3").play();
				setReturningPlayer(true);
			}
		}, 3000);
	}, []);

	const handleStartGame = async (type: GameType, mode: GameMode | null, privateKey: string | null) => {
		if (type === GameType.Observe) {
			if (!privateKey) {
				setErrorMessage("You need to provide a valid player name");
				return;
			}
			setObservingKey(privateKey);
			RequestHandler.get<GameStateContainer>(`observe/${privateKey}`).then((res) => startGame(res));
		} else {
			setObservingKey(null);
			const gameData: { type: GameType; mode: GameMode | null; privateKey?: string } = { type, mode };
			if (privateKey) {
				gameData.privateKey = privateKey;
			}
			RequestHandler.post<string>("game/join", gameData).then((res) => startGame(res));
		}
	};

	const startGame = (res: ServerResponse<string> | ServerResponse<GameStateContainer>) => {
		if (!res.success) {
			const error: string = res.message.replaceAll("_", " ").toLowerCase();
			error.charAt(0).toUpperCase() + error.slice(1);
			setErrorMessage(error);
		} else {
			setIsNavigating(true);
			// !new Audio("./assets/sounds/lobby/lets_go_r2.mp3").play();
			setTimeout(() => {
				navigate("/game");
			}, 1500);
		}
	};

	const handleNavigation = (destination: string) => {
		setIsNavigating(true);
		// !new Audio("./assets/sounds/lobby/excellent_idea_sir.mp3").play();
		setTimeout(() => {
			navigate(destination);
		}, 2000);
	};

	return (
		<div id="lobby-page" className="h-100 overflow-x-hidden">
			<div className="container-fluid h-100">
				<div className="row h-100 p-2">
					<div
						id="left-container"
						className={`col-3 blue-container d-flex flex-column h-100 justify-content-between ${
							isNavigating ? "slide-out-left" : "slide-in-left"
						}`}
					>
						<StartGameForm onStartGame={handleStartGame} />
						<button onClick={() => handleNavigation("/profile")} className="custom-btn custom-btn-big">
							Profile
						</button>
						<button onClick={() => handleNavigation("/deck")} className="custom-btn custom-btn-big">
							Deck
						</button>
						<button onClick={logout} className="custom-btn custom-btn-big">
							Disconnect
						</button>
					</div>
					<div className="col-6 h-100 d-flex flex-column justify-content-center align-items-center">
						<ErrorMessage errorMessage={errorMessage} errorMessageHandler={() => setErrorMessage("")} />
					</div>
					<div
						id="right-container"
						className={`col-3 h-100 blue-container d-flex flex-column ${
							isNavigating ? "slide-out-right" : "slide-in-right"
						}`}
					>
						<Chat />
						<div className="symbole-wrapper align-self-center">
							<RotatingSymbol />
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};

export default LobbyPage;
