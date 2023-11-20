import Chat from "./Chat";

const LobbyChat: React.FC = () => {
	return (
		<div id="chatContainer" className="container">
			<div className="subContainer">
				<Chat />
				<div className="symbol"></div>
			</div>
		</div>
	);
};

export default LobbyChat;
