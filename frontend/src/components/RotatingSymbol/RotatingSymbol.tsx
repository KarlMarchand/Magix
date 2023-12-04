import React, { useState, useEffect } from "react";
import empireSymbole from "../../assets/factions/empire-symbol.png";
import rebelSymbole from "../../assets/factions/rebel-symbol.png";
import republicSymbole from "../../assets/factions/republic-symbol.png";
import separatistSymbole from "../../assets/factions/separatist-symbol.png";
import criminalSymbole from "../../assets/factions/criminal-symbol.png";

const symbolUrls = [empireSymbole, rebelSymbole, republicSymbole, separatistSymbole, criminalSymbole];

const RotatingSymbol: React.FC = () => {
	const [currentSymbol, setCurrentSymbol] = useState<number>(0);

	useEffect(() => {
		const intervalId = setInterval(() => {
			setCurrentSymbol((prevSymbol) => (prevSymbol + 1) % symbolUrls.length);
		}, 10000); // Change symbol every 10 seconds

		return () => clearInterval(intervalId);
	}, []);

	return (
		<div className="rotating-symbol" style={{ animation: "rotateSymbol 20s linear infinite" }}>
			<img src={symbolUrls[currentSymbol]} alt="Symbol" />
		</div>
	);
};

export default RotatingSymbol;
