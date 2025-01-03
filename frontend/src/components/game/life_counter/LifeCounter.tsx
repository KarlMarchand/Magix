import React, { useEffect, useState } from "react";
import useGameState from "@context/GameStateProvider";
import OwnerTypes from "@customTypes/OwnerEnum";
import "./lifeCounter.scss";

enum LifeChangeType {
	healing,
	hurt,
	idle,
}

const LifeCounter: React.FC<{ owner: OwnerTypes }> = ({ owner }) => {
	const { hp, opponentHp } = useGameState();
	const [condition, setCondition] = useState<LifeChangeType>(LifeChangeType.idle);
	const [life, setLife] = useState<number>(0);

	const determineCondition = (newLife: number) => {
		if (newLife < life) {
			return LifeChangeType.hurt;
		} else if (newLife > life) {
			return LifeChangeType.healing;
		}
		return LifeChangeType.idle;
	};

	useEffect(() => {
		const newLife = owner === OwnerTypes.self ? hp : opponentHp;
		const newCondition = determineCondition(newLife);

		if (newCondition !== LifeChangeType.idle) {
			setCondition(newCondition);
			const timeoutId = setTimeout(() => setCondition(LifeChangeType.idle), 500);
			return () => clearTimeout(timeoutId);
		}

		setLife(newLife);
	}, [owner, hp, opponentHp]);

	const conditionClass =
		condition === LifeChangeType.idle ? "" : condition === LifeChangeType.hurt ? " hurt" : " healing";

	return (
		<div className={`life ${owner}${conditionClass}`}>
			<div>
				<p>{life}</p>
			</div>
		</div>
	);
};

export default LifeCounter;
