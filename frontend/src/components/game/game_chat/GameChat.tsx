import { useState } from "react";
import useGameState from "@context/GameStateProvider";
import Chat from "@components/Chat";
import "./gameChat.scss";

const GameChat: React.FC = () => {
	const [isChatOpen, setIsChatOpen] = useState<boolean>(false);
	const { surrender } = useGameState();

	return (
		<div id="chat-game" className={isChatOpen ? "pushed blue-container" : ""}>
			<div className="toggle-icon" onClick={() => setIsChatOpen(!isChatOpen)}>
				<span className="saber"></span>
				<span className="saber"></span>
				<span className="saber"></span>
			</div>
			<button className="custom-btn custom-btn-big" onClick={surrender}>
				Surrender The Game
			</button>
			<Chat />
		</div>
	);
};

export default GameChat;
