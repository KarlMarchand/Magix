import RotatingSymbol from "@components/rotating_symbol/RotatingSymbol";
import React from "react";
import "./loadingScreen.scss";

const LoadingScreen: React.FC = () => {
	return (
		<div
			id="loading-screen"
			className="d-flex flex-column justify-content-center align-items-center h-100 w-100 overflow-hidden"
		>
			<div className="loading-wrapper blue-container text-center d-flex flex-column align-items-center justify-content-center slide-in-bottom">
				<RotatingSymbol className="lg mb-5" />
				<span className="loading-text mt-5">Loading</span>
			</div>
		</div>
	);
};

export default LoadingScreen;
