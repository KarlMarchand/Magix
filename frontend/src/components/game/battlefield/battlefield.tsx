import SelfPlayerZone from "../player_zones/SelfZone";
import OwnerTypes from "@customTypes/OwnerEnum";
import ErrorMessage from "@components/message_box/ErrorMessage";
import Timer from "../timer/Timer";
import useGameState from "@context/GameStateProvider";
import GameCardContainer from "../game_card/GameCardContainer";
import OpponentZone from "../player_zones/OpponentZone";

const Battlefield: React.FC = () => {
	const { opponentBoard, board, opponentInfos, playerInfos, shouldHighLightBoard, errorMessage, setErrorMessage } =
		useGameState();

	return (
		<div id="game-battlefield" className="blue-container">
			<OpponentZone owner={OwnerTypes.opponent} />
			<div className="field opponent">
				<GameCardContainer
					cards={opponentBoard}
					className={"opponent"}
					playerFaction={opponentInfos?.faction ?? "empire"}
					isSelfCard={false}
				/>
			</div>
			<div className="message-slider">
				<ErrorMessage
					errorMessage={errorMessage}
					errorMessageHandler={() => {
						setErrorMessage("");
					}}
				/>
			</div>
			<Timer />
			<div
				className={`field self${shouldHighLightBoard ? " highlight" : ""}`}
				onDragOver={(e) => e.preventDefault()}
			>
				<GameCardContainer
					cards={board}
					className={"played-card"}
					playerFaction={playerInfos?.faction ?? "rebel"}
				/>
			</div>
			<SelfPlayerZone owner={OwnerTypes.self} />
		</div>
	);
};

export default Battlefield;
