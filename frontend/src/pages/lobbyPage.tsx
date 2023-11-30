import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthProvider";
import HomeOne3DScene from "../components/HomeOne3DScene";
import RequestHandler from "../utils/requestHandler";
import useSessionStorage from "../hooks/UseSessionStorage";
import GameStateContainer from "../types/GameStateContainer";
import Chat from "../components/Chat";
import "../sass/lobbyStyle.scss";
import RotatingSymbol from "../components/RotatingSymbol";

const LobbyPage: React.FC = () => {
	const { logout, user } = useAuth();
	const navigate = useNavigate();
	const [returningPlayer, setReturningPlayer] = useSessionStorage("returningPlayer", false);
	const [_, setObservingKey] = useSessionStorage("observing", null);
	const [isGameLaunching, setIsGameLaunching] = useState(false);
	const [privateKey, setPrivateKey] = useState<string>("");
	const [coopGameKey, setCoopGameKey] = useState<string>("");
	const [arenaGameKey, setArenaGameKey] = useState<string>("");
	const [playerName, setPlayerName] = useState<string>("");
	const [coopTrainingKey, setCoopTrainingKey] = useState<string>("");
	const [arenaTrainingKey, setArenaTrainingKey] = useState<string>("");
	const [errorMessage, setErrorMessage] = useState<string>("");
	const [startGameCallback, setStartGameCallback] = useState<(() => Promise<any>) | null>(null);

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

	const handleStartGame = async (gameType: GameType) => {
		if (gameType === GameType.Observe) {
			if (playerName) {
				setObservingKey(playerName);
				setStartGameCallback(async () => await RequestHandler.get<GameStateContainer>(`observe/${playerName}`));
			} else {
				setErrorMessage("You need to provide a valid player name");
				return;
			}
		} else {
			setObservingKey(null);
			let gameInfos = {};
			if (gameType === GameType.Training) {
				const hasKey = coopTrainingKey.length > 0;
				gameInfos = { Type: gameType, Mode: hasKey ? GameMode.Coop : GameMode.Standard };
				if (hasKey) {
					gameInfos = { ...gameInfos, PrivateKey: coopTrainingKey };
				}
			} else {
				const hasCoopKey = coopGameKey.length > 0;
				const hasPrivateKey = privateKey.length > 0;
				if (hasCoopKey) {
					gameInfos = { Type: gameType, Mode: GameMode.Coop, PrivateKey: coopGameKey };
				} else if (hasPrivateKey) {
					gameInfos = { Type: gameType, Mode: GameMode.Standard, PrivateKey: privateKey };
				} else {
					gameInfos = { Type: gameType, Mode: GameMode.Standard };
				}
			}
			setStartGameCallback(async () => await RequestHandler.post<string>("game/join", gameInfos));
		}
		// !new Audio("./assets/sounds/lobby/lets_go_r2.mp3").play();
		setIsGameLaunching(true);
	};

	const animationEndHandler = () => {
		if (startGameCallback) {
			startGameCallback().then((res) => {
				if (!res.success) {
					const error: string = res.message.replaceAll("_", " ").toLowerCase();
					error.charAt(0).toUpperCase() + error.slice(1);
					setErrorMessage(error);
				} else {
					navigate("/game");
				}
			});
		} else {
			setErrorMessage("An Error occured please try again");
		}
	};

	const handleDeckNavigation = () => {
		// !new Audio("./assets/sounds/lobby/excellent_idea_sir.mp3").play();
		setTimeout(() => {
			navigate("/deck");
		}, 1500);
	};

	return (
		<div id="lobby-page" className="h-100">
			<HomeOne3DScene startGameAnimation={isGameLaunching} onAnimationEndCallback={animationEndHandler} />
			{!returningPlayer && <div className="cover opening"></div>}
			<div id="lobby-interface" className={`container-fluid h-100 ${isGameLaunching ? "fade-out" : ""}`}>
				<div className="row h-100 gx-1">
					<div id="leftContainer" className="col-md-2 d-flex flex-column">
						<div className="blue-container d-flex flex-column">
							<button onClick={() => handleStartGame(GameType.Pvp)} className="btn btn-big">
								Play
							</button>
							<input
								type="text"
								placeholder="Private game key..."
								value={privateKey}
								onChange={(e) => setPrivateKey(e.target.value)}
							/>
							<input
								type="text"
								placeholder="Coop game key..."
								value={coopGameKey}
								onChange={(e) => setCoopGameKey(e.target.value)}
							/>
							<input
								type="text"
								placeholder="Arena game key..."
								value={arenaGameKey}
								onChange={(e) => setArenaGameKey(e.target.value)}
							/>
							<button onClick={() => handleStartGame(GameType.Training)} className="btn btn-big">
								Training
							</button>
							<input
								type="text"
								placeholder="Coop training key..."
								value={coopTrainingKey}
								onChange={(e) => setCoopTrainingKey(e.target.value)}
							/>
							<input
								type="text"
								placeholder="Arena training key..."
								value={arenaTrainingKey}
								onChange={(e) => setArenaTrainingKey(e.target.value)}
							/>
							<button onClick={() => handleStartGame(GameType.Observe)} className="btn btn-big">
								Observe
							</button>
							<input
								type="text"
								placeholder="Player name..."
								value={playerName}
								onChange={(e) => setPlayerName(e.target.value)}
							/>
							<Link to="/profile" className="btn btn-big">
								Profile
							</Link>
							<button onClick={handleDeckNavigation} className="btn btn-big">
								Deck
							</button>
							<button onClick={logout} className="btn btn-big">
								Disconnect
							</button>
						</div>
					</div>
					{errorMessage !== "" && (
						<div className="message-container" onClick={() => navigate("/lobby")}>
							<div className="alert alert-danger">
								<span>Error</span>
								{errorMessage}
							</div>
						</div>
					)}
					<div id="chatContainer" className="col-md-2">
						<div className="blue-container">
							<Chat />
							<RotatingSymbol />
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};

export default LobbyPage;
