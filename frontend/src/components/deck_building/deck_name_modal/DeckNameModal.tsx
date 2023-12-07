import React, { useEffect, useState } from "react";
import { Modal, ModalProps, Form } from "react-bootstrap";
import { useDeckManager } from "@context/deck_manager_context/DeckManagerContext";
import "./deckNameModal.scss";

const DeckNameModal: React.FC<ModalProps> = (props) => {
	const { currentDeck, changeCurrentDeckName } = useDeckManager();
	const [name, setName] = useState<string>("");
	const [error, setError] = useState<string | null>(null);

	const handleSave = () => {
		const changeResult = changeCurrentDeckName(name);
		if (!changeResult.isSuccessful) {
			setError(changeResult.error ?? "Name can't be changed");
		} else {
			props.onHide?.();
		}
	};

	useEffect(() => {
		setName(currentDeck.name);
	}, [props.show]);

	return (
		<Modal {...props} centered id="DeckNameModal">
			<Modal.Header closeButton>
				<Modal.Title id="contained-modal-title-vcenter">Edit Deck Name</Modal.Title>
			</Modal.Header>
			<Modal.Body>
				<Form>
					<Form.Group controlId="deckName">
						<Form.Control
							type="text"
							value={name}
							onChange={(e) => setName(e.target.value)}
							isInvalid={!!error}
							className="custom-input my-3"
							maxLength={20}
						/>
						<Form.Control.Feedback type="invalid">{error}</Form.Control.Feedback>
					</Form.Group>
				</Form>
			</Modal.Body>
			<Modal.Footer>
				<button onClick={handleSave} className="custom-btn">
					Save Changes
				</button>
			</Modal.Footer>
		</Modal>
	);
};

export default DeckNameModal;
