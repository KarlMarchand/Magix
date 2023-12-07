import React from "react";
import { useDeckManager } from "@context/DeckManagerContext/DeckManagerContext";
import CardSelection from "@components/DeckBuilding/CardSelection";
import Card from "@customTypes/Card";
import FiltersBar from "@components/DeckBuilding/FiltersBar/FiltersBar";
import "./cardsTabs.scss";
import { Container, Row, Col } from "react-bootstrap";

const CardsTab: React.FC = () => {
	const { currentDeck, filteredCardList } = useDeckManager();

	return (
		<Container id="card-selector-tab">
			<Row className="row">
				<div className="col-1"></div>
				<div className="col">
					<FiltersBar className="mb-3" />
				</div>
				<div className="col-1 d-flex align-items-center text-center">
					<h1>{currentDeck.cardNumber}/30</h1>
				</div>
			</Row>
			<Row xs={1} sm={2} md={3} lg={4} xl={5} style={{ maxHeight: "55vh" }} className="overflow-y-scroll">
				{filteredCardList.map((card: Card) => (
					<Col key={card.id}>
						<CardSelection card={card} />
					</Col>
				))}
			</Row>
		</Container>
	);
};

export default CardsTab;
