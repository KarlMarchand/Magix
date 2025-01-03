import useGameState from "@context/GameStateProvider";
import React, { useEffect, useState } from "react";

const Timer: React.FC = () => {
	const { endTurn, remainingTurnTime } = useGameState();
	const [localTimer, setLocalTimer] = useState(remainingTurnTime);

	useEffect(() => {
		const interval = setInterval(() => {
			setLocalTimer((t) => (t > 0 ? t - 1 : 0));
		}, 1000);

		return () => clearInterval(interval);
	}, []);

	useEffect(() => {
		// Adjust the local timer if the discrepancy is significant
		if (Math.abs(localTimer - remainingTurnTime) > 3) {
			setLocalTimer(remainingTurnTime);
		}
	}, [remainingTurnTime]);

	return (
		<div id="chrono" onClick={endTurn}>
			<h1 id="countdown">{localTimer}</h1>
		</div>
	);
};

export default Timer;
