import React from "react";

const Timer: React.FC = () => {
	const remainingTime = 60;
	const endTurnCallback = () => {};

	return (
		<div id="chrono" onClick={endTurnCallback}>
			<h1 id="countdown">{remainingTime}</h1>
		</div>
	);
};

export default Timer;
