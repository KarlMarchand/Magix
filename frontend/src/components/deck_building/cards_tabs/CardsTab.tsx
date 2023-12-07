import React from "react";
import { Container, Row, Col } from "react-bootstrap";
import { useDeckManager } from "@context/deck_manager_context/DeckManagerContext";
import CardSelection from "@components/deck_building/CardSelection";
import Card from "@customTypes/Card";
import FiltersBar from "@components/deck_building/filters_bar/FiltersBar";
import "./cardsTabs.scss";
import { LuRefreshCw } from "react-icons/lu";

const CardsTab: React.FC = () => {
	const { currentDeck, filteredCardList, setFilter } = useDeckManager();

	return (
		<Container id="card-selector-tab">
			<Row className="align-items-center  mb-3">
				<div className="col-1 d-flex flex-column justify-content-center align-items-center">
					<LuRefreshCw
						size="40"
						onClick={() => setFilter(null)}
						className="refresh-btn"
						title="Refresh Filters"
					/>
				</div>
				<div className="col">
					<FiltersBar />
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
