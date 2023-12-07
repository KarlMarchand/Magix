import useGameManager from "@context/GameManagerContext";
import React, { useEffect, useState } from "react";
import Card from "@customTypes/Card";
import "./cards.scss";
import { useGameOptions } from "@context/GameOptionsProvider";

export interface CardProps extends React.HTMLAttributes<HTMLDivElement> {
	card: Card;
	playerFaction: string;
	isStatic?: boolean;
	isHandCard?: boolean;
	isSelfCard?: boolean;
}

const GameCard: React.FC<CardProps> = ({
	card,
	playerFaction,
	isStatic = false,
	isHandCard = false,
	isSelfCard = true,
	className,
	...htmlStandardProps
}) => {
	const { factionsImages } = useGameOptions();
	const { creditCount, onCardDragFailed, onCardDragStart, playCard } = useGameManager();
	const [isSleeping, setIsSleeping] = useState<boolean>(false);
	const [hasStealth, setHasStealth] = useState<boolean>(false);
	const [hasTaunt, setHasTaunt] = useState<boolean>(false);
	const [isSelected, setIsSelected] = useState<boolean>(false);
	const [dragging, setDragging] = useState<boolean>(false);

	const selection = async () => {
		if (!isStatic) {
			if (isSelfCard && !isHandCard) {
				setIsSelected(!isSelected);
			}
			const moveIsValid = await playCard(card, !isSelfCard);
			setIsSelected(false);
			setIsSleeping(moveIsValid);
		}
	};

	const onDragStart = () => {
		setDragging(true);
		onCardDragStart(card);
	};

	const onDragEnd = (ev: React.DragEvent<HTMLDivElement>) => {
		setDragging(false);
		if (ev.dataTransfer.dropEffect === "none") {
			onCardDragFailed();
		} else {
			playCard(card, !isSelfCard);
		}
	};

	useEffect(() => {
		if (card.sound && !isStatic && !isHandCard) {
			new Audio(card.sound).play();
		}
	}, []);

	const getCardImage = (): string => {
		const isMinion = card.id === 1;
		const isMissingCard = card.cardName === "Missing Card";
		const cardName = isMinion ? `${playerFaction.toLowerCase()}-minion` : isMissingCard ? "0" : card.id.toString();
		return `url('assets/img/cards/${cardName}.webp')`;
	};

	useEffect(() => {
		setHasTaunt(card.mechanics?.includes("Taunt") ?? false);
		setHasStealth(card.mechanics?.includes("Stealth") ?? false);
		setIsSleeping(card.state === "SLEEP");
	}, [card]);

	const factionName = card.factionName ? card.factionName.toLowerCase() : playerFaction.toLowerCase();

	const classList = `game-card ${factionName}${className ? ` ${className}` : ""}${dragging ? " dragging" : ""}${
		card.id === 1 ? " minion" : ""
	}${isSelfCard ? " self" : " opponent"}`;

	return (
		<div
			className={classList}
			draggable={!isStatic && creditCount >= card.cost}
			onClick={selection}
			onDragStart={onDragStart}
			onDragEnd={(e) => onDragEnd(e)}
			id={`card-wrapper-${card.uid ?? card.id}`}
			{...htmlStandardProps}
		>
			<div id={`card-${card.id}`} className={`game-card-inner ${isSelected ? "selectedCard" : ""}`}>
				{hasTaunt && !isHandCard && (
					<div>
						<div className="taunt"></div>
					</div>
				)}
				<div
					className={`game-card-front${isSleeping ? " sleeping" : ""}${hasStealth ? " stealth" : ""}`}
					style={{ backgroundImage: getCardImage() }}
				>
					<div className="game-card-name">
						<span>{card.cardName}</span>
					</div>
					<div className="bullet-shape reverse"></div>
					<div className="bullet-shape reverse"></div>
					<div className="game-card-description">
						{card.mechanics?.map(function (mechanic) {
							return <span key={mechanic}> {mechanic} </span>;
						})}
					</div>
					<div className="bullet-shape cost">
						<span>{card.cost}</span>
					</div>
					<div
						className="bullet-shape faction"
						style={{ backgroundImage: factionsImages[factionName].symbol }}
					></div>
					<div className="bullet-shape attack">
						<span>{card.atk}</span>
					</div>
					<div className="bullet-shape defense">
						<span>{card.hp}</span>
					</div>
				</div>
				<div className="game-card-back"></div>
			</div>
		</div>
	);
};

export default GameCard;
