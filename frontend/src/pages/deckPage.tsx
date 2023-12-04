import React, { useEffect, useState } from "react";
import RequestHandler from "../utils/requestHandler";
import AvailableDeckOptions from "../types/AvailableDeckOptions";
import { Link } from "react-router-dom";
import { Col, Row } from "react-bootstrap";
import { Tabs, Tab } from "../components/Tabs/Tabs";
import Faction from "../types/Faction";
import Hero from "../types/Hero";
import Talent from "../types/Talent";
import Card from "../types/Card";
import Deck, { DeckBuilder } from "../types/Deck";
import Avatar from "../components/Avatar/Avatar";

const DeckPage: React.FC = () => {
	const [cardList, setCardList] = useState<Card[]>();
	const [heroList, setHeroList] = useState<Hero[]>();
	const [talentList, setTalentList] = useState<Talent[]>();
	const [factionList, setFactionList] = useState<Faction[]>();
	const [deckList, setDeckList] = useState<Deck[]>();
	const [currentDeck, setCurrentDeck] = useState<DeckBuilder>(new DeckBuilder());

	useEffect(() => {
		// Show place holders
		// Open deck management modal
		RequestHandler.get<Deck[]>("deck/all").then((response) => {
			if (response.success && response.data) {
				setDeckList(response.data);
			}
		});

		RequestHandler.get<AvailableDeckOptions>("deck/options/all").then((response) => {
			if (response.success && response.data) {
				setCardList(response.data.cards);
				setHeroList(response.data.heroes);
				setTalentList(response.data.talents);
				setFactionList(response.data.factions);
			}
			// Remove place holders
		});
	}, []);

	const handleSave = (button: EventTarget) => {
		// Show spinner and disable buttons
		currentDeck.save().then((response) => {
			// Remove spinner and enable buttons
			if (response.isSuccessful) {
				const message: string = "Deck successfully saved";
				// Show confirmation modal
			} else if (response.error) {
				const error: string = response.error;
				// Show error modal
			}
		});
	};

	const handleManage = () => {
		// Open the decks manager modal
	};

	return (
		<div id="deck-building-page" className="p-4 overflow-hidden d-flex flex-column h-100 fade-in">
			<Link to="/lobby" className="arrow"></Link>
			<div className="blue-container m-4 fade-in p-5 flex-fill d-flex flex-column">
				<Row>
					<Col>
						<div id="deck-stats">
							{/* CardList && BarChart */}
							<p>Stats</p>
							<p>And</p>
							<p>All</p>
						</div>
					</Col>
				</Row>
				<Row className="flex-fill">
					<Col md={10}>
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
					</Col>
					<Col md={2} className="d-flex flex-column justify-content-between">
						<div id="deck-infos" className="blue-container w-100 d-flex flex-column align-items-center">
							<p className="w-100 d-flex justify-content-between">
								<span>Deck Name:</span>
								<strong>{currentDeck.name ?? "New Deck"}</strong>
							</p>
							<Avatar playerClassName={currentDeck.hero?.name} />
							<p className="w-100 mt-2 d-flex justify-content-between">
								<span>Selected Talent:</span>
								<strong>{currentDeck.talent?.name ?? "None"}</strong>
							</p>
							<p className="w-100 d-flex justify-content-between">
								<span>Selected Talent:</span>
								<strong>{currentDeck.talent?.name ?? "None"}</strong>
							</p>
							<p className="w-100 d-flex justify-content-between">
								<span>Selected Faction:</span>
								<strong>{currentDeck.faction?.name ?? "None"}</strong>
							</p>
						</div>
						<div id="deck-building-buttons-wrapper">
							<button className="custom-btn custom-btn-big w-100" onClick={currentDeck.reset}>
								Reset Deck
							</button>
							<button
								className="custom-btn submit-btn custom-btn-big w-100"
								onClick={(e) => {
									handleSave(e.target);
								}}
							>
								Save Deck
							</button>
							<button className="custom-btn custom-btn-big w-100" onClick={handleManage}>
								Manage Decks
							</button>
						</div>
					</Col>
				</Row>
			</div>
		</div>
	);
};

export default DeckPage;
