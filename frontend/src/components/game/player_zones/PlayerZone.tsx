import React, { useEffect, useState } from "react";
import useGameState from "@context/GameStateProvider";
import GameCardContainer, { GameCardContainerInterface } from "@components/game/game_card/GameCardContainer";
import GameCard from "@components/game/game_card/GameCard";
import LifeCounter from "@components/game/life_counter/LifeCounter";
import PlayerInfos from "@customTypes/PlayerZoneInfos";
import Avatar from "@components/avatar/Avatar";
import Card from "@customTypes/Card";
import OwnerTypes from "@customTypes/OwnerEnum";

export interface BaseZoneProps extends React.HTMLAttributes<HTMLDivElement> {
	owner: OwnerTypes;
}

interface PlayerZoneProps extends BaseZoneProps, GameCardContainerInterface {
	creditCount: number;
	playerInfos: PlayerInfos | undefined;
	cardsCount: number;
}

const PlayerZone: React.FC<PlayerZoneProps> = ({
	owner,
	playerFaction,
	creditCount,
	playerInfos,
	cardsCount,
	isSelfCard,
	...CardContainerProps
}) => {
	const { activateHeroPower, attackEnnemyHero } = useGameState();
	const fakeCard: Card = {
		id: 1,
		cost: 0,
		hp: 0,
		atk: 0,
		mechanics: [],
		uid: 0,
		baseHP: 0,
		cardName: "PlaceHolder",
		factionName: playerFaction,
	};
	const username = playerInfos?.username ?? "";
	const [avatarIsActive, setAvatarIsActive] = useState<boolean>(false);

	useEffect(() => {
		if (avatarIsActive) {
			if (owner === OwnerTypes.self) {
				activateHeroPower();
			} else {
				attackEnnemyHero();
			}
			const toggleAvatar = setTimeout(() => {
				setAvatarIsActive(false);
			}, 500);
			return () => clearTimeout(toggleAvatar);
		}
	}, [avatarIsActive]);

	return (
		<>
			<div className={`deck ${owner}`}>
				<div>
					<GameCard
						card={fakeCard}
						className={"flipped"}
						playerFaction={playerFaction}
						isStatic={true}
						isSelfCard={owner === OwnerTypes.self}
					/>
					<span className="blue-shadow-text">{cardsCount}</span>
				</div>
			</div>
			<div className={`hand ${owner}`}>
				<GameCardContainer playerFaction={playerFaction} isHandCard={true} {...CardContainerProps} />
			</div>
			<div className={`playerInfos blue-container ${owner}`}>
				<div className="infoJoueur">
					<p>{username}</p>
					<div className="credit blue-shadow-text">
						<p>{creditCount}</p>
					</div>
				</div>
				<div onClick={() => setAvatarIsActive(true)}>
					<Avatar playerClassName={playerFaction} className={avatarIsActive ? "activated" : ""} />
				</div>
			</div>
			<LifeCounter owner={owner} />
		</>
	);
};

export default PlayerZone;
