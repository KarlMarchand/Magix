import Card from "@customTypes/Card";
import React, { createContext, useState, useContext, useEffect, useMemo } from "react";
import { LatestAction } from "@customTypes/game/GameState";
import { useGameOptions } from "./GameOptionsProvider";
import RequestHandler from "@utils/RequestHandler";
import Deck from "@customTypes/deck/Deck";
import PlayerInfos from "@customTypes/PlayerZoneInfos";
import GameStatus, { GameStatusConverter } from "@customTypes/game/GameStatus";
import GameStateContainer from "@customTypes/game/GameStateContainer";
import ServerResponse from "@customTypes/ServerResponse";
import GameAction, { GameActionsTypes } from "@customTypes/game/GameActions";

export const GameStateProvider: React.FC<{ children: JSX.Element }> = ({ children }) => {
	const { availableCardsList, availableFactionsList } = useGameOptions();
	const [selectedCard, _setSelectedCard] = useState<Card | null>(null);
	const [shouldHighLightBoard, _setShouldHighLightBoard] = useState<boolean>(false);
	const [creditCount, _setCreditCount] = useState<number>(0);
	const [isMyTurn, _setIsMyTurn] = useState<boolean>(false);
	const [playerInfos, _setPlayerInfos] = useState<PlayerInfos | undefined>();
	const [opponentInfos, _setOpponentInfos] = useState<PlayerInfos | undefined>();
	const [remainingTurnTime, _setRemainingTurnTime] = useState<number>(0);
	const [heroPowerAlreadyUsed, _setHeroPowerAlreadyUsed] = useState<boolean>(false);
	const [hp, _setHp] = useState<number>(0);
	const [maxCreditCount, _setmaxCreditCount] = useState<number>(0);
	const [hand, _setHand] = useState<Card[]>([]);
	const [board, _setBoard] = useState<Card[]>([]);
	const [remainingCardsCount, _setRemainingCardsCount] = useState<number>(0);
	const [latestActions, _setLatestActions] = useState<LatestAction[]>([]);
	const [handSize, _setHandSize] = useState<number>(0);
	const [opponentHp, _setOpponentHp] = useState<number>(0);
	const [opponentCreditCount, _setOpponentCreditCount] = useState<number>(0);
	const [opponentBoard, _setOpponentBoard] = useState<Card[]>([]);
	const [opponentRemainingCardsCount, _setOpponentRemainingCardsCount] = useState<number>(0);
	const [opponentHand, _setOpponentHand] = useState<Card[]>([]);
	const [playerActiveDeck, _setPlayerActiveDeck] = useState<Deck>();
	const [playedCards, _setPlayedCards] = useState<Set<number>>(new Set<number>());
	const [gameStatus, _setGameStatus] = useState<GameStatus>(GameStatus.loading);
	const [errorMessage, setErrorMessage] = useState<string>("");
	const [isObserving, setIsObserving] = useState<boolean>(false);

	useEffect(() => {
		RequestHandler.get<Deck>("deck/active").then((response) => {
			if (response.success && response.data) {
				_setPlayerActiveDeck;
				response.data;
			}
		});
		const updateInterval = setInterval(() => {
			_updateGameState();
		}, 5000);
		return () => clearInterval(updateInterval);
	}, []);

	useEffect(() => {
		if (errorMessage) {
			const eraseMessage = setTimeout(() => {
				setErrorMessage("");
			}, 5000);
			return () => clearTimeout(eraseMessage);
		}
	}, [errorMessage]);

	useEffect(() => {
		const gameIsOver = gameStatus === GameStatus.defeat || gameStatus === GameStatus.victory;
		if (gameIsOver && !isObserving) {
			_saveGame();
		}
	}, [gameStatus]);

	const _saveGame = (): void => {
		const gameResult = {
			opponent: opponentInfos?.username,
			victory: gameStatus === GameStatus.victory,
			deckId: playerActiveDeck?.id,
			playedCardsIds: Array.from(playedCards),
		};
		RequestHandler.post("game/save", gameResult);
	};

	const _updateGameState = async (): Promise<void> => {
		const response = await RequestHandler.get<GameStateContainer>("game");
		_processGameStateResponse(response);
	};

	const _processGameStateResponse = (response: ServerResponse<GameStateContainer>): void => {
		if (response.success && response.data?.gameState) {
			// Need to set the state to playing or something
			const gameState = response.data.gameState;
			if (!playerInfos && playerActiveDeck) {
				_setPlayerInfos({
					username: gameState.username ?? "",
					welcomeText: gameState.welcomeText ?? "",
					heroName: gameState.heroClass ?? "",
					faction: playerActiveDeck?.faction.name ?? "rebel",
				});
			}
			if (!opponentInfos && availableFactionsList) {
				const randomFaction =
					availableFactionsList.find((faction) => faction.name !== playerActiveDeck?.faction.name) ??
					availableFactionsList[0];
				if (randomFaction) {
					_setOpponentInfos({
						username: gameState.opponent?.username ?? "",
						welcomeText: gameState.opponent?.welcomeText ?? "",
						heroName: gameState.opponent?.heroClass ?? "",
						faction: randomFaction?.name ?? "empire",
					});
				}
			}
			if (gameState.board) {
				_setBoard(gameState.board);
			}
			if (gameState.hand) {
				_setHand(gameState.hand);
			}
			if (gameState.opponent?.board) {
				_setOpponentBoard(gameState.opponent.board);
			}
			if (gameState.yourTurn) {
				_setIsMyTurn(gameState.yourTurn);
			}
			if (gameState.hp && hp !== gameState.hp) {
				_setHp(gameState.hp);
			}
			if (gameState.remainingTurnTime && remainingTurnTime !== gameState.remainingTurnTime) {
				_setRemainingTurnTime(gameState.remainingTurnTime);
			}
			if (gameState.heroPowerAlreadyUsed && heroPowerAlreadyUsed !== gameState.heroPowerAlreadyUsed) {
				_setHeroPowerAlreadyUsed(gameState.heroPowerAlreadyUsed);
			}
			if (gameState.mp && creditCount !== gameState.mp) {
				_setCreditCount(gameState.mp);
			}
			if (gameState.maxMp && maxCreditCount !== gameState.maxMp) {
				_setmaxCreditCount(gameState.maxMp);
			}
			if (gameState.remainingCardsCount && remainingCardsCount !== gameState.remainingCardsCount) {
				_setRemainingCardsCount(gameState.remainingCardsCount);
			}
			if (gameState.latestActions && latestActions !== gameState.latestActions) {
				_setLatestActions(gameState.latestActions);
			}
			if (gameState.handSize && handSize !== gameState.handSize) {
				_setHandSize(gameState.handSize);
			}
			if (gameState.opponent?.hp && opponentHp !== gameState.opponent.hp) {
				_setOpponentHp(gameState.opponent.hp);
			}
			if (gameState.opponent?.mp && opponentCreditCount !== gameState.opponent.mp) {
				_setOpponentCreditCount(gameState.opponent.mp);
			}
			if (
				gameState.opponent?.remainingCardsCount &&
				opponentRemainingCardsCount !== gameState.opponent.remainingCardsCount
			) {
				_setOpponentRemainingCardsCount(gameState.opponent.remainingCardsCount);
			}
			if (gameState.opponent?.handSize && opponentHand.length !== gameState.opponent.handSize) {
				_adjustOpponentFakeHand(gameState.opponent.handSize);
			}
		} else if (response.success && response.data?.message) {
			// TODO: Check the other potentials responses messages like errors and stuff
			_setGameStatus(GameStatusConverter(response.data?.message));
		} else {
			setErrorMessage(response.message);
		}
	};

	const _sendGameAction = async (action: GameAction): Promise<boolean> => {
		if (isObserving && action.type !== GameActionsTypes.Surrender) {
			setErrorMessage("You can't do actions when observing");
		} else if (!isMyTurn && action.type !== GameActionsTypes.Surrender) {
			setErrorMessage("wait for your turn!");
		} else {
			const response = await RequestHandler.post<GameStateContainer>("game", action);
			_processGameStateResponse(response);

			return response.success;
		}

		return false;
	};

	const onCardDragEnd = (ev: React.DragEvent<HTMLDivElement>) => {
		if (ev.dataTransfer.dropEffect !== "none") {
			if (board.length >= 7) {
				setErrorMessage("Maximum number of cards reached");
			} else {
				_sendGameAction({ type: GameActionsTypes.Play, uid: selectedCard?.uid ?? 0 });
			}
		}

		_setSelectedCard(null);
		_setShouldHighLightBoard(false);
	};

	const onCardDragStart = (card: Card) => {
		const canPlay = canCardBePlayed(card.cost);

		if (canPlay) {
			_setSelectedCard(card);
			_setShouldHighLightBoard(true);
		}
	};

	const playCard = async (card: Card, isEnnemy: boolean): Promise<boolean> => {
		const moveIsValid = true;

		if (selectedCard === card) {
			_setSelectedCard(null);
		}

		if (moveIsValid && !isEnnemy && card.id > 1) {
			_setPlayedCards((prevPlayedCards) => new Set(prevPlayedCards).add(card.id));
		}

		return Promise.resolve(moveIsValid);
	};

	const canCardBePlayed = (cardCost: number): boolean => {
		return isMyTurn && creditCount >= cardCost && board.length < 7;
	};

	const activateHeroPower = async (): Promise<void> => {
		await _sendGameAction({ type: GameActionsTypes.HeroPower });
	};

	const endTurn = async (): Promise<void> => {
		await _sendGameAction({ type: GameActionsTypes.EndTurn });
	};

	const surrender = async (): Promise<void> => {
		await _sendGameAction({ type: GameActionsTypes.Surrender });
	};

	const attackEnnemyHero = async (): Promise<void> => {
		await _sendGameAction({ type: GameActionsTypes.Attack, uid: selectedCard?.uid ?? 0, targetuid: 0 });
	};

	const _adjustOpponentFakeHand = (newSize: number) => {
		let cards = [...opponentHand];
		const sizeDifference = newSize - (opponentHand?.length ?? 0);

		if (sizeDifference > 0) {
			// If newSize is larger, add minion cards
			cards = cards.concat(Array(sizeDifference).fill(availableCardsList[0]));
		} else {
			// If newSize is smaller, remove cards
			cards = cards.splice(0, newSize);
		}

		_setOpponentHand(cards);
	};

	const value = useMemo(
		() => ({
			shouldHighLightBoard,
			creditCount,
			isMyTurn,
			playerInfos,
			opponentInfos,
			remainingTurnTime,
			heroPowerAlreadyUsed,
			hp,
			maxCreditCount,
			hand,
			board,
			remainingCardsCount,
			latestActions,
			handSize,
			opponentHp,
			opponentCreditCount,
			opponentBoard,
			opponentRemainingCardsCount,
			opponentHand,
			playerActiveDeck,
			gameStatus,
			onCardDragEnd,
			onCardDragStart,
			playCard,
			canCardBePlayed,
			activateHeroPower,
			endTurn,
			attackEnnemyHero,
			surrender,
			errorMessage,
			setErrorMessage,
			selectedCard,
			setIsObserving,
		}),
		[
			creditCount,
			isMyTurn,
			playerInfos,
			opponentInfos,
			remainingTurnTime,
			heroPowerAlreadyUsed,
			hp,
			maxCreditCount,
			hand,
			board,
			remainingCardsCount,
			latestActions,
			handSize,
			opponentHp,
			opponentCreditCount,
			opponentBoard,
			opponentRemainingCardsCount,
			opponentHand,
			playerActiveDeck,
			gameStatus,
			shouldHighLightBoard,
			errorMessage,
			selectedCard,
		]
	);

	return <GameStateContext.Provider value={value}>{children}</GameStateContext.Provider>;
};

interface GameStateInterface {
	creditCount: number;
	onCardDragEnd: (ev: React.DragEvent<HTMLDivElement>) => void;
	onCardDragStart: (card: Card) => void;
	playCard: (card: Card, isEnnemy: boolean) => Promise<boolean>;
	canCardBePlayed: (cardCost: number) => boolean;
	isMyTurn: boolean;
	playerInfos: PlayerInfos | undefined;
	opponentInfos: PlayerInfos | undefined;
	remainingTurnTime: number;
	heroPowerAlreadyUsed: boolean;
	hp: number;
	maxCreditCount: number;
	hand: Card[];
	board: Card[];
	remainingCardsCount: number;
	latestActions: LatestAction[];
	handSize: number;
	opponentHp: number;
	opponentCreditCount: number;
	opponentBoard: Card[];
	opponentRemainingCardsCount: number;
	opponentHand: Card[];
	playerActiveDeck: Deck | undefined;
	activateHeroPower: () => Promise<void>;
	endTurn: () => Promise<void>;
	attackEnnemyHero: () => Promise<void>;
	gameStatus: GameStatus;
	surrender: () => Promise<void>;
	shouldHighLightBoard: boolean;
	errorMessage: string;
	setErrorMessage: (message: string) => void;
	selectedCard: Card | null;
	setIsObserving: (isObserving: boolean) => void;
}

const GameStateDefault: GameStateInterface = {
	creditCount: 0,
	onCardDragEnd: (ev) => null,
	onCardDragStart: (card) => null,
	playCard: (card, isEnnemy) => Promise.resolve(false),
	canCardBePlayed: (cardCost) => false,
	isMyTurn: false,
	playerInfos: undefined,
	opponentInfos: undefined,
	remainingTurnTime: 0,
	heroPowerAlreadyUsed: false,
	hp: 0,
	maxCreditCount: 0,
	hand: [],
	board: [],
	remainingCardsCount: 0,
	latestActions: [],
	handSize: 0,
	opponentHp: 0,
	opponentCreditCount: 0,
	opponentBoard: [],
	opponentRemainingCardsCount: 0,
	opponentHand: [],
	playerActiveDeck: undefined,
	activateHeroPower: () => Promise.resolve(),
	endTurn: () => Promise.resolve(),
	attackEnnemyHero: () => Promise.resolve(),
	gameStatus: GameStatus.loading,
	surrender: () => Promise.resolve(),
	shouldHighLightBoard: false,
	errorMessage: "",
	setErrorMessage: (message) => null,
	selectedCard: null,
	setIsObserving: (isObserving: boolean) => null,
};

const GameStateContext = createContext(GameStateDefault);

const useGameState = () => useContext(GameStateContext);

export default useGameState;
