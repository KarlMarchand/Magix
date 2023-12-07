// import { CardContainer } from "./CardContainer.js";
// import { PlayerZone } from "./PlayerZone.js";
// import { GameChat } from "./GameChat.js";
// import { Timer } from "./Timer.js";
// import { MessageBox } from "./MessageBox.js";
// import { LoadingScreen } from "./LoadingScreen.js";
// import { GameOverScreen } from "./GameOverScreen.js";
// import { adjustHand, applyStyles, opponentAvatar } from "../gameScript.js";
// import { customClasses } from "../customData.js";
// import { useEffect, useRef, useState } from "react";

// export const Game = (props) => {
// 	const key = useRef(sessionStorage.getItem("key"));
// 	const [isLoading, setIsLoading] = useState(true);
// 	const [gameOver, setGameOver] = useState(false);
// 	const [victory, setVictory] = useState(false);
// 	const won = useRef(false);
// 	const [openChat, setOpenChat] = useState(false);

// 	const [highlight, setHighlight] = useState(false);
// 	const selectedCard = useRef(null);
// 	const playedCards = useRef([]);
// 	const [message, setMessage] = useState("");
// 	const [time, setTime] = useState(50);

// 	const opponentHand = useRef([]);
// 	const opponentField = useRef([]);
// 	const oppDeckSize = useRef(30);
// 	const oppLife = useRef(30);
// 	const oppCredits = useRef(1);
// 	const oppHandSize = useRef(0);
// 	const oppUsername = useRef(null);

// 	const selfUsername = useRef(sessionStorage.getItem("username"));
// 	const selfField = useRef([]);
// 	const selfHand = useRef([]);
// 	const selfCredits = useRef(1);
// 	const selfDeckSize = useRef(30);
// 	const selfLife = useRef(30);
// 	const yourTurn = useRef(true);
// 	const powerUsed = useRef(false);

// 	let observing = sessionStorage.getItem("observing");

// 	useEffect(() => {
// 		getData();
// 		setTimeout(applyStyles, 500);
// 	}, []);

// 	useEffect(() => {
// 		if (gameOver && !observing) {
// 			setTimeout(() => {
// 				const cardsPlayed = playedCards.current.filter((id) => {
// 					return id > 1;
// 				});
// 				let formData = new FormData();
// 				formData.append("action", "sendGameData");
// 				formData.append("opponent", oppUsername.current);
// 				formData.append("victory", won.current);
// 				formData.append("deck", cardsPlayed);
// 				fetch("ajax.php", {
// 					method: "POST",
// 					body: formData,
// 				});
// 			}, 1000);
// 		} else if (gameOver && observing) {
// 			endObserving();
// 		}
// 	}, [gameOver]);

// 	const getData = () => {
// 		fetch("ajax-state.php", {
// 			method: "POST",
// 			credentials: "include",
// 		})
// 			.then((response) => response.json())
// 			.then((jsondata) => {
// 				if (jsondata === "LAST_GAME_WON" || jsondata === "LAST_GAME_LOST") {
// 					if (isLoading) {
// 						setIsLoading(false);
// 					}
// 					setGameOver(true);
// 					setVictory(jsondata === "LAST_GAME_WON");
// 					won.current = jsondata == "LAST_GAME_WON";
// 				} else if (jsondata === "WAITING") {
// 					setIsLoading(true);
// 				} else if (typeof jsondata === "string" && jsondata !== "undefined") {
// 					if (observing) {
// 						endObserving();
// 					} else {
// 						console.log(jsondata);
// 						doAction(["SURRENDER"]);
// 					}
// 				} else {
// 					updateState(jsondata);
// 					if (observing) {
// 						selfUsername.current = sessionStorage.getItem("observing");
// 						var img =
// 							"url(../../static/img/classe/" +
// 							customClasses[jsondata.heroClass].replace(/\s+/g, "") +
// 							".webp)";
// 						document.querySelector(":root").style.setProperty("--avatar", img);
// 					}
// 					setIsLoading(false);
// 				}
// 				setTimeout(() => {
// 					getData();
// 				}, 1000);
// 			});
// 	};

// 	const updateState = (jsondata) => {
// 		setTime(jsondata.remainingTurnTime);
// 		yourTurn.current = jsondata.yourTurn;
// 		powerUsed.current = jsondata.heroPowerAlreadyUsed;

// 		if (selfLife.current !== jsondata.hp) {
// 			selfLife.current = jsondata.hp;
// 		}
// 		selfCredits.current = jsondata.mp;
// 		selfHand.current = jsondata.hand;
// 		selfField.current = jsondata.board;
// 		addPlayedCards(jsondata.board);
// 		selfDeckSize.current = jsondata.remainingCardsCount;

// 		oppUsername.current = jsondata.opponent.username;
// 		opponentAvatar(jsondata.opponent.heroClass);
// 		oppLife.current = jsondata.opponent.hp;
// 		oppCredits.current = jsondata.opponent.mp;
// 		opponentField.current = jsondata.opponent.board;
// 		oppDeckSize.current = jsondata.opponent.remainingCardsCount;
// 		if (jsondata.opponent.handSize !== oppHandSize.current) {
// 			opponentHand.current = adjustHand(
// 				jsondata.opponent.handSize,
// 				oppHandSize.current,
// 				opponentHand.current,
// 				props.cards
// 			);
// 			oppHandSize.current = jsondata.opponent.handSize;
// 		}
// 	};

// 	const selection = (params) => {
// 		if (selectedCard.current === null) {
// 			params.isEnnemy ? error("You must select one of your card first!") : (selectedCard.current = params);
// 		} else {
// 			if (selectedCard.current.card.uid !== params.card.uid) {
// 				if (params.isEnnemy) {
// 					doAction(["ATTACK", selectedCard.current.card.uid.toString(), params.card.uid.toString()]);
// 					selectedCard.current.reset();
// 					selectedCard.current = null;
// 					if (params.reset) {
// 						params.reset();
// 					}
// 				} else {
// 					selectedCard.current.reset();
// 					selectedCard.current = params;
// 				}
// 			} else {
// 				params.reset();
// 				selectedCard.current = null;
// 			}
// 		}
// 	};

// 	const error = (message) => {
// 		setMessage(message);
// 		setTimeout(() => {
// 			setMessage("");
// 		}, 5000);
// 	};

// 	const endObserving = () => {
// 		if (sessionStorage.getItem("observing")) {
// 			sessionStorage.removeItem("observing");
// 			setGameOver(true);
// 		}
// 	};

// 	const onDragOver = (event) => {
// 		event.preventDefault();
// 	};

// 	const dragEnd = (params) => {
// 		setHighlight(false);
// 		if (params.result === "success") {
// 			if (selfField.current.length >= 7) {
// 				error("Maximum number of cards reached");
// 			} else {
// 				doAction(["PLAY", params.target.uid.toString()]);
// 			}
// 		}
// 	};

// 	const doAction = (params) => {
// 		if (observing && params[0] !== "SURRENDER") {
// 			error("You can't do actions when observing");
// 		} else if (yourTurn.current || params[0] === "SURRENDER") {
// 			let formData = new FormData();
// 			formData.append("action", "gameAction");
// 			formData.append("type", params[0]);
// 			if (params[1]) {
// 				formData.append("uid", params[1]);
// 			}
// 			if (params[2]) {
// 				formData.append("targetuid", params[2]);
// 			}
// 			fetch("ajax.php", {
// 				method: "POST",
// 				body: formData,
// 			})
// 				.then((res) => res.json())
// 				.then((res) => {
// 					if (params[0] === "SURRENDER" && observing) {
// 						endObserving();
// 					} else if (typeof res.answer === "string" || res.answer instanceof String) {
// 						res.answer = res.answer.replaceAll("_", " ");
// 						error(res.answer);
// 					} else {
// 						updateState(res.answer);
// 					}
// 				});
// 		} else {
// 			error("wait for your turn!");
// 		}
// 	};

// 	const addPlayedCards = (jsonData) => {
// 		if (typeof jsonData != "undefined") {
// 			let newArray = jsonData.map(({ id }) => id);
// 			playedCards.current = [...new Set([...playedCards.current, ...newArray])];
// 		}
// 	};

// 	const gameContent = (
// 		<>
// 			<GameChat
// 				opened={openChat}
// 				toggle={() => {
// 					setOpenChat(!openChat);
// 				}}
// 				playerKey={key.current}
// 				surrender={() => {
// 					doAction(["SURRENDER"]);
// 				}}
// 			/>
// 			<PlayerZone
// 				cards={opponentHand.current}
// 				classSup={["opponent"]}
// 				owner={"opponent"}
// 				username={oppUsername.current}
// 				deckSize={oppDeckSize.current}
// 				life={oppLife.current}
// 				credits={oppCredits.current}
// 				select={selection}
// 			/>
// 			<div className="field opponent">
// 				<CardContainer cards={opponentField.current} classSup={["opponent"]} select={selection} />
// 			</div>
// 			<div className="message-slider">
// 				<MessageBox
// 					message={message}
// 					onClick={() => {
// 						setMessage("");
// 					}}
// 				/>
// 			</div>
// 			<Timer
// 				time={time}
// 				endTurn={() => {
// 					doAction(["END_TURN"]);
// 				}}
// 			/>
// 			<div
// 				className={["field", "self", highlight ? "highlight" : ""].filter((e) => e).join(" ")}
// 				onDragOver={(e) => onDragOver(e)}
// 			>
// 				<CardContainer cards={selfField.current} classSup={["self", "played-card"]} select={selection} />
// 			</div>
// 			<PlayerZone
// 				cards={selfHand.current}
// 				owner={"self"}
// 				username={selfUsername.current}
// 				classSup={["self", "handCard"]}
// 				draggable={selfCredits.current}
// 				dragStart={(params) => {
// 					setHighlight(
// 						selfCredits.current >= params.card.cost && yourTurn.current && selfField.current.length < 7
// 					);
// 				}}
// 				dragEnd={dragEnd}
// 				deckSize={selfDeckSize.current}
// 				life={selfLife.current}
// 				credits={selfCredits.current}
// 				select={() => {
// 					doAction(["HERO_POWER"]);
// 				}}
// 			/>
// 			{gameOver ? <GameOverScreen victory={victory} /> : null}
// 		</>
// 	);

// 	const loadingScreen = (
// 		<>
// 			<GameChat
// 				opened={openChat}
// 				toggle={() => {
// 					setOpenChat(!openChat);
// 				}}
// 				playerKey={key.current}
// 				surrender={() => {
// 					doAction(["SURRENDER"]);
// 				}}
// 			/>
// 			<LoadingScreen />
// 		</>
// 	);

// 	const content = isLoading ? loadingScreen : gameContent;

// 	return content;
// };
