import React, { useState } from "react";
import { GameType, GameMode, GameInput, gameSettings } from "../types/GameTypeOptions";

type StartGameFormProps = {
	onStartGame: (gameType: GameType, gameMode: GameMode | null, gameKey: string | null) => void;
};

const StartGameForm: React.FC<StartGameFormProps> = ({ onStartGame }) => {
	const [gameKeys, setGameKeys] = useState<{ [type in GameType]?: { [mode: string]: string } }>({});

	const handleKeyChange = (gameType: GameType, mode: GameMode | null, value: string) => {
		setGameKeys({
			...gameKeys,
			[gameType]: {
				...(gameKeys[gameType] || {}),
				[mode ?? "Standard"]: value,
			},
		});
	};

	const handleGameStart = (gameType: GameType) => {
		const settings = gameSettings[gameType];
		const firstFilledInput = settings.find((input) => gameKeys[gameType]?.[input.mode ?? "Standard"]);

		if (firstFilledInput) {
			const key = gameKeys[gameType]?.[firstFilledInput.mode ?? "Standard"] || null;
			onStartGame(gameType, firstFilledInput.mode, key);
		} else {
			// If no key is entered, default to STANDARD mode
			onStartGame(gameType, GameMode.Standard, null);
		}
	};

	const renderInputFields = (gameType: GameType) => {
		return gameSettings[gameType].map((input: GameInput) => (
			<input
				key={`${gameType}-${input.mode}`}
				type="text"
				value={gameKeys[gameType]?.[input.mode ?? "Standard"] || ""}
				onChange={(e) => handleKeyChange(gameType, input.mode, e.target.value)}
				placeholder={input.privateKey}
				className="custom-input my-2"
			/>
		));
	};

	return (
		<>
			{Object.keys(gameSettings).map((type) => (
				<div key={type}>
					<button
						type="button"
						onClick={() => handleGameStart(type as GameType)}
						className="custom-btn custom-btn-big w-100"
					>
						{type}
					</button>
					<div className="d-flex flex-column">{renderInputFields(type as GameType)}</div>
				</div>
			))}
		</>
	);
};

export default StartGameForm;
