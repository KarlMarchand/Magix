import React from "react";
import Starfield from "../components/Starfield";

const GamePage: React.FC = () => {
	return (
		<main>
			<div className="container">
				<div id="game" className="subContainer"></div>
			</div>
			<Starfield />
		</main>
	);
};

export default GamePage;
