// import React from "react";

// const PlayerZone: React.FC<> = () => {
// 	const [hurt, setHurt] = React.useState(false);
// 	const first = React.useRef(true);
// 	const faction = React.useRef("");
// 	const deck = React.useRef([
// 		{
// 			id: 1,
// 			cost: 0,
// 			hp: 0,
// 			atk: 0,
// 			mechanics: [],
// 			uid: 0,
// 			baseHP: 0,
// 		},
// 	]);

// 	React.useEffect(() => {
// 		if (props.owner === "self") {
// 			faction.current = sessionStorage.getItem("faction");
// 		}
// 	}, []);

// 	React.useEffect(() => {
// 		if (!first.current) {
// 			setHurt(true);
// 			setTimeout(() => {
// 				setHurt(false);
// 			}, 500);
// 		}
// 		first.current = false;
// 	}, [props.life]);

// 	const selection = (e, params) => {
// 		if (props.select) {
// 			props.select(params);
// 			e.target.classList.toggle("activated");
// 			setTimeout(() => {
// 				e.target.classList.toggle("activated");
// 			}, 500);
// 		}
// 	};

// 	const dragStart = (params) => {
// 		if (props.dragStart) {
// 			props.dragStart(params);
// 		}
// 	};

// 	const dragEnd = (params) => {
// 		if (props.dragEnd) {
// 			props.dragEnd(params);
// 		}
// 	};

// 	return (
// 		<React.Fragment>
// 			<div className={["deck", props.owner].filter((e) => e).join(" ")}>
// 				<div>
// 					<CardContainer cards={deck.current} classSup={["flipped", faction.current]} />
// 					<span className="blue-shadow-text">{props.deckSize}</span>
// 				</div>
// 			</div>
// 			<div className={["hand", props.owner].filter((e) => e).join(" ")}>
// 				<CardContainer
// 					cards={props.cards}
// 					classSup={props.classSup}
// 					draggable={props.draggable}
// 					dragStart={dragStart}
// 					dragEnd={dragEnd}
// 				/>
// 			</div>
// 			<div className={["playerInfos", "container", props.owner].filter((e) => e).join(" ")}>
// 				{" "}
// 				<div className="subContainer">
// 					<div className="infoJoueur">
// 						<p>{props.username}</p>
// 						<div className="credit blue-shadow-text">
// 							<p>{props.credits}</p>
// 						</div>
// 					</div>
// 					<div
// 						className={"avatar"}
// 						onClick={(e) => {
// 							props.owner === "opponent"
// 								? selection(e, { card: { uid: 0 }, isEnnemy: true })
// 								: selection(e);
// 						}}
// 					></div>
// 				</div>{" "}
// 			</div>
// 			<div className={["life", props.owner, hurt ? "hurt" : ""].filter((e) => e).join(" ")}>
// 				<div>
// 					<p>{props.life}</p>
// 				</div>
// 			</div>
// 		</React.Fragment>
// 	);
// }
