import Card from "../types/Card";

interface CardsContainerProps {
	cards: Card[];
	classSup?: string[];
}

const CardContainer: React.FC<CardsContainerProps> = ({ cards, classSup }) => {
	return (
		<>
			{cards.map((card, i) => {
				let key;
				card.uid ? (key = card.uid) : (key = i);
				return <span className={classSup?.toString()}>{card.cardName}</span>;
			})}
		</>
	);
};

export default CardContainer;
