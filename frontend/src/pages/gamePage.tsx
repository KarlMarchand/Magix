import React from "react";
import Starfield from "@components/starfield/Starfield";

const GamePage: React.FC = () => {
	return (
		<main>
			<div id="game" className="blue-container" />
			<Starfield />
		</main>
	);
};

export default GamePage;
