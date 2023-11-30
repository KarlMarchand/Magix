import Chat from "./Chat";

interface GameChatProp {
	opened: boolean;
	toggleHandler: () => void;
	surrenderHandler: () => void;
}

const GameChat: React.FC<GameChatProp> = ({ opened, toggleHandler, surrenderHandler }: GameChatProp) => {
	return (
		<div id="chat-game" className={opened ? "pushed blue-container" : ""}>
			<div className="toggle-icon" onClick={toggleHandler}>
				<span className="saber"></span>
				<span className="saber"></span>
				<span className="saber"></span>
			</div>
			<button className="btn big-btn" onClick={surrenderHandler}>
				Surrender The Game
			</button>
			<Chat />
		</div>
	);
};

export default GameChat;
