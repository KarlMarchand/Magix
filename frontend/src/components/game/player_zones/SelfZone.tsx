import React from "react";
import useGameState from "@context/GameStateProvider";
import PlayerZone, { BaseZoneProps } from "./PlayerZone";

const SelfPlayerZone: React.FC<BaseZoneProps> = ({ owner }) => {
	const { remainingCardsCount, creditCount, playerInfos, hand } = useGameState();

	return (
		<PlayerZone
			owner={owner}
			playerFaction={playerInfos?.faction ?? "rebel"}
			creditCount={creditCount}
			playerInfos={playerInfos}
			cardsCount={remainingCardsCount}
			cards={hand}
		/>
	);
};

export default SelfPlayerZone;
