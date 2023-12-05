import React, { useState } from "react";
import { Modal, ModalProps } from "react-bootstrap";
import { useDeckManager } from "@context/DeckManagerContext/DeckManagerContext";
import Deck from "@customTypes/Deck/Deck";

const DeckSelectionModal: React.FC<ModalProps> = (props) => {
	const { currentDeck, setCurrentDeck, playerDeckList } = useDeckManager();
	const [selectedDeck, setSelectedDeck] = useState<Deck>();

	const handleButtons = (newDeck: boolean) => {
		setCurrentDeck(newDeck ? selectedDeck : undefined);
		props.onHide?.();
	};

	const handleSelection = (deck: Deck) => {
		if (deck.id === selectedDeck?.id || deck.id === currentDeck.id) {
			setSelectedDeck(undefined);
		} else {
			setSelectedDeck(deck);
		}
	};

	return (
		<Modal {...props} backdrop="static" keyboard={false} size="lg" centered id="DeckSelectionModal">
			<Modal.Header closeButton>
				<Modal.Title>Edit Deck Name</Modal.Title>
			</Modal.Header>
			<Modal.Body>
				{playerDeckList.map((deck) => {
					return (
						<button
							className={`custom-btn ${
								selectedDeck?.id === deck.id || currentDeck?.id === deck.id ? "selected" : ""
							}`}
							onClick={() => handleSelection(deck)}
						>
							{deck.name}
						</button>
					);
				})}
			</Modal.Body>
			<Modal.Footer>
				<button
					onClick={() => handleButtons(false)}
					className="custom-btn"
					disabled={selectedDeck !== undefined}
				>
					Edit Selected Deck
				</button>
				<button onClick={() => handleButtons(true)} className="custom-btn">
					New Deck
				</button>
			</Modal.Footer>
		</Modal>
	);
};

export default DeckSelectionModal;
