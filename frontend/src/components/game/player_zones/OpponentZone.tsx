import React from "react";
import useGameState from "@context/GameStateProvider";
import PlayerZone, { BaseZoneProps } from "./PlayerZone";

const OpponentZone: React.FC<BaseZoneProps> = ({ owner }) => {
	const { opponentCreditCount, opponentInfos, opponentRemainingCardsCount, opponentHand } = useGameState();

	return (
		<PlayerZone
			owner={owner}
			playerFaction={opponentInfos?.faction ?? "empire"}
			creditCount={opponentCreditCount}
			playerInfos={opponentInfos}
			cardsCount={opponentRemainingCardsCount}
			cards={opponentHand}
		/>
	);
};

export default OpponentZone;
