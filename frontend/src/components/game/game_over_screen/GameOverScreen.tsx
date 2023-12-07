import React from "react";
import { Link } from "react-router-dom";
import lukeImage from "@assets/luke.webp";
import darthImage from "@assets/darth.webp";
import "./gameOverScreen.scss";

interface GameOverScreenProps {
	isVictory: boolean;
}

const GameOverScreen: React.FC<GameOverScreenProps> = ({ isVictory }) => {
	const backgroundImageUrl = isVictory ? lukeImage : darthImage;

	return (
		<div className="gameOver">
			<div className={isVictory ? "plasma luke" : "plasma vader"}></div>
			<div
				className={isVictory ? "img victory" : "img defeat"}
				style={{ backgroundImage: `url(${backgroundImageUrl})` }}
			>
				<div className="wrapper">
					<h1>{isVictory ? "Victory!" : "Defeat!"}</h1>
					<Link to="/lobby" className="custom-btn custom-big-btn">
						Menu
					</Link>
				</div>
			</div>
		</div>
	);
};

export default GameOverScreen;
