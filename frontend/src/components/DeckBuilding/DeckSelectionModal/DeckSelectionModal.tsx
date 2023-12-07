import React from "react";
import { Modal, ModalProps, Stack } from "react-bootstrap";
import { useDeckManager } from "@context/DeckManagerContext/DeckManagerContext";
import "./deckSelectionModal.scss";
import { FaRegEdit, FaRegStar, FaRegTrashAlt, FaStar } from "react-icons/fa";
import Deck from "@customTypes/Deck/Deck";

const DeckSelectionModal: React.FC<ModalProps> = (props) => {
	const { setCurrentDeck, playerDeckList, factionsImages, deleteDeck, equipDeck } = useDeckManager();

	const handleEditingButtons = (deckToEdit?: Deck): void => {
		setCurrentDeck(deckToEdit ? playerDeckList.find((deck) => deck.id === deckToEdit.id) : undefined);
		props.onHide?.();
	};

	return (
		<Modal {...props} backdrop="static" keyboard={false} size="lg" centered id="DeckSelectionModal">
			<Modal.Header closeButton>
				<Modal.Title>Deck Selection</Modal.Title>
			</Modal.Header>
			<Modal.Body>
				<Stack className="overflow-y-scroll p-2" style={{ border: "1px solid white", height: "30rem" }}>
					{playerDeckList.map((deck, index) => {
						return (
							<div key={index} className={"deck-line container"}>
								<div className="row align-content-center">
									<div className="col-2 d-flex flex-column justify-content-center align-items-center">
										<img
											className="thumbnail"
											src={factionsImages[deck.faction.name.toLowerCase()].symbol}
											alt={`${deck.faction.name}`}
										/>
									</div>
									<span className="col">{deck.name}</span>
									<div className="col-3">
										{deck.active ? (
											<FaStar
												size="30"
												className="icon-btn always-active me-4"
												title="Equipped Deck"
											/>
										) : (
											<FaRegStar
												size="30"
												className="icon-btn me-4"
												onClick={() => {
													deck.active = true;
													equipDeck(deck);
												}}
												title="Equip Deck"
											/>
										)}
										<FaRegEdit
											size="30"
											className="icon-btn edit-btn me-4"
											onClick={() => handleEditingButtons(deck)}
											title="Edit Deck"
										/>
										<FaRegTrashAlt
											size="30"
											className="icon-btn delete-btn"
											onClick={() => deleteDeck(deck)}
											title="Delete Deck"
										/>
									</div>
								</div>
							</div>
						);
					})}
				</Stack>
			</Modal.Body>
			<Modal.Footer>
				<div className="d-flex w-100 px-1">
					<button onClick={() => handleEditingButtons()} className="custom-btn w-100 me-2">
						New Deck
					</button>
				</div>
			</Modal.Footer>
		</Modal>
	);
};

export default DeckSelectionModal;
