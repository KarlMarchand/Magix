import { useDeckManager } from "@context/DeckManagerContext/DeckManagerContext";
import React, { useState } from "react";
import { Spinner } from "react-bootstrap";
import "./deckSubmissionButton.scss";

interface DecksubmissionProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
	onFailure?: (message?: string) => void;
	onSuccess?: (data?: any) => void;
}

const DeckSubmissionButton: React.FC<DecksubmissionProps> = ({ onFailure, onSuccess, ...props }) => {
	const { saveDeck } = useDeckManager();
	const [saveStatus, setSaveStatus] = useState<"idle" | "saving" | "success" | "failed">("idle");

	const handleSave = async (): Promise<void> => {
		setSaveStatus("saving");
		try {
			const response = await saveDeck();
			if (response.isSuccessful) {
				setSaveStatus("success");
				onSuccess?.(response.data);
			} else {
				setSaveStatus("failed");
				onFailure?.(response.error);
			}
		} catch {
			setSaveStatus("failed");
		} finally {
			setTimeout(() => setSaveStatus("idle"), 2000);
		}
	};

	const renderButtonContent = () => {
		switch (saveStatus) {
			case "saving":
				return (
					<Spinner animation="border" role="status">
						<span className="visually-hidden">Loading...</span>
					</Spinner>
				);
			case "success":
				return "Saved";
			case "failed":
				return "Failed";
			default:
				return "Save Deck";
		}
	};

	return (
		<button {...props} className={`custom-btn submit-btn w-100 ${saveStatus}`} onClick={handleSave}>
			{renderButtonContent()}
		</button>
	);
};

export default DeckSubmissionButton;
