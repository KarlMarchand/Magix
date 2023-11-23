import React, { useState, useEffect } from "react";
import LobbyChat from "../components/LobbyChat";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthProvider";
import Avatar from "../components/Avatar";
import HomeOne3DScene from "../components/HomeOne3DScene";
import RequestHandler from "../utils/requestHandler";
import useSessionStorage from "../hooks/UseSessionStorage";
import GameStateContainer from "../types/GameStateContainer";

const LobbyPage: React.FC = () => {
	const { logout, user } = useAuth();
	const navigate = useNavigate();
	const [returningPlayer, setReturningPlayer] = useSessionStorage("returningPlayer", false);
	const [_, setObservingKey] = useSessionStorage("observing", null);
	const [isGameLaunching, setIsGameLaunching] = useState(false);
	const [privateKey, setPrivateKey] = useState<string>("");
	const [coopKey, setCoopKey] = useState<string>("");
	const [playerName, setPlayerName] = useState<string>("");
	const [trainKey, setTrainKey] = useState<string>("");
	const [errorMessage, setErrorMessage] = useState<string>("");
	const [startGameCallback, setStartGameCallback] = useState<(() => Promise<any>) | null>(null);

	useEffect(() => {
		setTimeout(() => {
			if (returningPlayer) {
				new Audio("./static/sounds/lobby/good_to_see_you_again.mp3").play();
			} else {
				new Audio("./static/sounds/lobby/c3po_and_r2d2_at_your_service.mp3").play();
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
				const hasKey = trainKey.length > 0;
				gameInfos = { Type: gameType, Mode: hasKey ? GameMode.Coop : GameMode.Standard };
				if (hasKey) {
					gameInfos = { ...gameInfos, PrivateKey: trainKey };
				}
			} else {
				const hasCoopKey = coopKey.length > 0;
				const hasPrivateKey = privateKey.length > 0;
				if (hasCoopKey) {
					gameInfos = { Type: gameType, Mode: GameMode.Coop, PrivateKey: coopKey };
				} else if (hasPrivateKey) {
					gameInfos = { Type: gameType, Mode: GameMode.Standard, PrivateKey: privateKey };
				} else {
					gameInfos = { Type: gameType, Mode: GameMode.Standard };
				}
			}
			setStartGameCallback(async () => await RequestHandler.post<string>("game/join", gameInfos));
		}
		new Audio("./static/sounds/lobby/lets_go_r2.mp3").play();
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
		new Audio("./static/sounds/lobby/excellent_idea_sir.mp3").play();
		setTimeout(() => {
			navigate("/deck");
		}, 1500);
	};

	return (
		<>
			{!returningPlayer && <div className="cover opening"></div>}
			<HomeOne3DScene startGameAnimation={isGameLaunching} onAnimationEndCallback={animationEndHandler} />
			<main className={isGameLaunching ? "fade-out" : undefined}>
				<div id="leftContainer" className="container">
					<div className="subContainer">
						<div id="playContainer" className="flex-column">
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
								type="test"
								placeholder="Coop game key..."
								value={coopKey}
								onChange={(e) => setCoopKey(e.target.value)}
							/>
							<button onClick={() => handleStartGame(GameType.Training)} className="btn btn-big">
								Training
							</button>
							<input
								type="test"
								placeholder="Coop training key..."
								value={trainKey}
								onChange={(e) => setTrainKey(e.target.value)}
							/>
							<button onClick={() => handleStartGame(GameType.Observe)} className="btn btn-big">
								<input
									type="text"
									placeholder="Player name..."
									value={playerName}
									onChange={(e) => setPlayerName(e.target.value)}
								/>
								Observe
							</button>
						</div>
						<div className="container">
							<div className="subContainer">
								<Avatar />
								<div className="infoJoueur">
									<p>{user?.username}</p>
									<p>{user?.className}</p>
								</div>
							</div>
						</div>
						<div id="navContainer" className="flex-column">
							<Link to="/profile" className="btn btn-big">
								Profile
							</Link>
							<button className="btn btn-big" onClick={handleDeckNavigation}>
								Deck
							</button>
							<button onClick={logout} className="btn btn-big">
								Disconnect
							</button>
						</div>
					</div>
				</div>
				<LobbyChat />
			</main>
			{errorMessage !== "" && (
				<div className="lobby message-container" onClick={() => navigate("/lobby")}>
					<div className="alert alert-danger">
						<span>Error</span>
						{errorMessage}
					</div>
				</div>
			)}
		</>
	);
};

export default LobbyPage;
