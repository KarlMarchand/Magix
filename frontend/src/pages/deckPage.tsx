import React, { useEffect, useState } from "react";
import { Col, Row } from "react-bootstrap";
import { FaRegEdit } from "react-icons/fa";
import { Tabs, Tab } from "../components/Tabs/Tabs";
import BackButton from "../components/BackButton/BackButton";
import Avatar from "../components/Avatar/Avatar";
import "../sass/deckStyle.scss";
import DeckNameModal from "../components/DeckBuilder/DeckNameModal/DeckNameModal";
import { useDeckBuilder } from "../context/DeckManagerContext/DeckManagerContext";

const DeckPage: React.FC = () => {
	const { deckBuilder } = useDeckBuilder();
	const [showDeckNameModal, setShowDeckNameModal] = useState(false);

	useEffect(() => {
		// Open deck selection modal
	}, []);

	const handleSave = (button: HTMLButtonElement): void => {
		// Show spinner
		deckBuilder.save().then((response) => {
			// Remove spinner
			let cssClass = "failed";

			if (response.isSuccessful) {
				cssClass = "success";
			}

			button.classList.add(cssClass);
			setTimeout(() => {
				button.classList.remove(cssClass);
			}, 2000);
		});
	};

	const handleChangeDeck = (): void => {
		alert("Change deck");
	};

	return (
		<div id="deck-building-page" className="p-4 overflow-hidden d-flex flex-column h-100 fade-in">
			<BackButton />
			<div className="blue-container m-4 fade-in p-5 flex-fill d-flex flex-column">
				<Row className="h-100">
					<Col xs={10}>
						<div className="d-flex flex-column h-100">
							<Row>
								<Col xs={12} md={2}>
									<button className="custom-btn w-100" onClick={handleChangeDeck}>
										Change Deck
									</button>
								</Col>
								<Col xs={12} md={7} className="d-flex justify-content-center align-items-center">
									<h1 className="me-5 text-truncate">{deckBuilder.name}</h1>
								</Col>
								<Col xs={12} md={3} className="container">
									<Row>
										<Col xs={4} className="d-flex align-items-center justify-content-end">
											<FaRegEdit
												size="40"
												className="edit-btn me-2"
												style={{ marginTop: "-0.5rem" }}
												onClick={() => setShowDeckNameModal(true)}
												title="Edit Deck Name"
											/>
										</Col>
										<Col xs={8}>
											<button
												className="custom-btn submit-btn w-100"
												onClick={(e) => {
													handleSave(e.currentTarget);
												}}
											>
												<span>Save Deck</span>
											</button>
										</Col>
									</Row>
								</Col>
							</Row>
							<Row className="flex-grow-1">
								<Tabs>
									<Tab name="Cards">
										<p>Cards content</p>
									</Tab>
									<Tab name="Class">
										<p>Class content</p>
									</Tab>
									<Tab name="Power">
										<p>Power content</p>
									</Tab>
									<Tab name="Faction">
										<p>Faction content</p>
									</Tab>
								</Tabs>
							</Row>
						</div>
					</Col>
					<Col xs={2} className="d-flex flex-column justify-content-between">
						{/* CardList && BarChart */}
						<div id="deck-infos" className="blue-container w-100 d-flex flex-column align-items-center">
							<Avatar playerClassName={deckBuilder.hero?.name} />
						</div>
						<div id="deck-building-buttons-wrapper"></div>
						<button className="custom-btn" onClick={deckBuilder.reset}>
							Reset Deck
						</button>
					</Col>
				</Row>
			</div>
			<DeckNameModal show={showDeckNameModal} onHide={() => setShowDeckNameModal(false)} />
		</div>
	);
};

export default DeckPage;
