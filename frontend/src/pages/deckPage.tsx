import React, { useState } from "react";
import { Col, Row } from "react-bootstrap";
import { FaRegEdit } from "react-icons/fa";
import { useDeckManager } from "@context/deck_manager_context/DeckManagerContext";
import { Tabs, Tab } from "@components/tabs/Tabs";
import DeckSubmissionButton from "@components/deck_building/deck_submission_button/DeckSubmissionButton";
import DeckCompositionList from "@components/deck_building/deck_composition_list/DeckCompositionList";
import DeckSelectionModal from "@components/deck_building/deck_selection_modal/DeckSelectionModal";
import DeckNameModal from "@components/deck_building/deck_name_modal/DeckNameModal";
import CardCostBarChart from "@components/deck_building/card_cost_bar_chart/CardCostBarChart";
import ErrorMessage from "@components/message_box/ErrorMessage";
import FactionsTab from "@components/deck_building/FactionsTab";
import TalentsTab from "@components/deck_building/TalentsTab";
import HeroesTab from "@components/deck_building/HeroesTab";
import CardsTab from "@components/deck_building/cards_tabs/CardsTab";
import BackButton from "@components/back_button/BackButton";
import Avatar from "@components/avatar/Avatar";
import ConfirmationModal from "@components/ConfirmationModal";

const DeckPage: React.FC = () => {
	const { currentDeck, resetDeck } = useDeckManager();
	const [showDeckSelectionModal, setShowDeckSelectionModal] = useState<boolean>(true);
	const [showDeckNameModal, setShowDeckNameModal] = useState<boolean>(false);
	const [showConfirmationModal, setShowConfirmationModal] = useState<boolean>(false);
	const [errorMessage, setErrorMessage] = useState<string>("");

	return (
		<div
			id="deck-building-page"
			className="p-4 overflow-hidden d-flex flex-column h-100 fade-in"
			style={{ minWidth: "1050px" }}
		>
			<BackButton />
			{!showDeckSelectionModal && (
				<div className="blue-container m-4 fade-in p-5 flex-fill d-flex flex-column">
					<Row className="h-100">
						<Col xs={10}>
							<div className="d-flex flex-column h-100">
								<Row>
									<Col xs={12} md={2}>
										<button
											className="custom-btn w-100"
											onClick={() => setShowConfirmationModal(true)}
										>
											Change Deck
										</button>
									</Col>
									<Col xs={12} md={8} className="d-flex justify-content-between align-items-center">
										<div style={{ flexGrow: 1, textAlign: "center" }}>
											<h1 className="text-truncate" style={{ margin: "0 auto" }}>
												{currentDeck.name}
											</h1>
										</div>
										<div>
											<FaRegEdit
												size="40"
												className="icon-btn"
												style={{ marginTop: "-0.5rem" }}
												onClick={() => setShowDeckNameModal(true)}
												title="Edit Deck Name"
											/>
										</div>
									</Col>
									<Col xs={12} md={2} className="container">
										<Row>
											<Col>
												<DeckSubmissionButton
													onFailure={(error) =>
														setErrorMessage(error ?? "Couldn't save the deck")
													}
												/>
											</Col>
										</Row>
									</Col>
								</Row>
								<Row className="flex-grow-1">
									{errorMessage === "" ? (
										<Tabs>
											<Tab name="Cards">
												<CardsTab />
											</Tab>
											<Tab name="Heroes">
												<HeroesTab />
											</Tab>
											<Tab name="Talents">
												<TalentsTab />
											</Tab>
											<Tab name="Factions">
												<FactionsTab />
											</Tab>
										</Tabs>
									) : (
										<div className="d-flex flex-column justify-content-center align-items-center">
											<ErrorMessage
												errorMessage={errorMessage}
												errorMessageHandler={() => setErrorMessage("")}
											/>
										</div>
									)}
								</Row>
							</div>
						</Col>
						<Col xs={2} className="d-flex flex-column justify-content-between">
							<Avatar playerClassName={currentDeck.hero?.name} className="mb-3" />
							<CardCostBarChart />
							<DeckCompositionList className="flex-grow-1" />
							<button className="custom-btn" onClick={resetDeck}>
								Reset Deck
							</button>
						</Col>
					</Row>
				</div>
			)}
			<DeckNameModal show={showDeckNameModal} onHide={() => setShowDeckNameModal(false)} />
			<DeckSelectionModal show={showDeckSelectionModal} onHide={() => setShowDeckSelectionModal(false)} />
			<ConfirmationModal
				show={showConfirmationModal}
				onHide={() => setShowConfirmationModal(false)}
				message={"Any unsaved progress will be lost if you change to another deck or quit the page!"}
				onContinueCallback={() => {
					setShowConfirmationModal(false);
					setShowDeckSelectionModal(true);
				}}
			/>
		</div>
	);
};

export default DeckPage;
