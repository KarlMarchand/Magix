import React, { useEffect, useState } from "react";
import RequestHandler from "../utils/requestHandler";
import PlayerStats from "../types/PlayerStats";
import ServerResponse from "../types/ServerResponse";
import { useAuth } from "../context/AuthProvider";
import Avatar from "../components/Avatar";
import { Link } from "react-router-dom";
import CardContainer from "../components/CardContainer";
import "../sass/profileStyle.scss";
import GameHistoryTable from "../components/GameHistoryTable";

const ProfilePage: React.FC = () => {
	const { user } = useAuth();
	const [playerStats, setPlayerStats] = useState<PlayerStats>();
	const [ratio, setRatio] = useState<string>("0 : 0");
	const [resultWins, setResultWins] = useState<string>("0 (0%)");
	const [resultLoses, setResultLoses] = useState<string>("0 (0%)");

	useEffect(() => {
		RequestHandler.get<PlayerStats>("player").then((response: ServerResponse<PlayerStats>) => {
			if (response.success && response.data) {
				const stats = response.data;
				setPlayerStats(stats);
				const hasPlayed = stats.gamePlayed > 0;
				const wins = stats.wins ?? 0;
				const loses = stats.loses ?? 0;
				if (hasPlayed) {
					setRatio(loses ? `${(wins / loses).toFixed(2)} : 1` : `${wins} : 0`);
					setResultWins(`${wins} (${Math.round((wins / stats.gamePlayed) * 100)}%)`);
					setResultLoses(`${loses} (${Math.round((loses / stats.gamePlayed) * 100)}%)`);
				}
				// CSS % for the graph
				const percentWins = hasPlayed ? (wins / stats.gamePlayed) * 100 : 100;
				const percentLoses = 100 - percentWins;
				document.documentElement.style.setProperty("--wins", percentWins.toString());
				document.documentElement.style.setProperty("--loses", percentLoses.toString());
			}
		});
	}, []);

	return (
		<div id="profile-page">
			<section aria-label="Player-Infos">
				<div className="flex-row infoJoueur">
					<div className="blue-container">
						<h1>{user?.username}</h1>
						<h2>{user?.className}</h2>
					</div>
					<Avatar />
				</div>
				<div id="player-stats" className="flex-row">
					<div id="trophies-container" className="flex-column">
						<div id="trophy-wrap">
							<img id="trophy-img" src="/assets/img/horizontal-lined-bg.png" alt="trophy-img" />
						</div>
						<p className="stat-line">
							Number of Trophies: <span>{user?.trophies}</span>
						</p>
						<p className="stat-line">
							Best Trophy Score: <span>{user?.bestTrophyScore}</span>
						</p>
					</div>
					<div id="game-stats" className="blue-container">
						<p className="stat-line">
							Number of Played Games: <span>{playerStats?.gamePlayed}</span>
						</p>
						<p className="stat-line">
							Ratio of Wins to Loses:{" "}
							<span id="ratio-Wins" data-ratio={playerStats?.ratioWins}>
								{ratio}
							</span>
						</p>
						<div id="graph" className="flex-column">
							<svg viewBox="0 0 64 64">
								<circle id="pie-wins" cx="32" cy="32" r="16" />
								<circle id="pie-loses" cx="32" cy="32" r="16" />
							</svg>
							<span id="wins-label">
								<span> Wins: </span>
								<span>{resultWins}</span>
							</span>
							<span id="loses-label">
								<span>Loses: </span>
								<span>{resultLoses}</span>
							</span>
						</div>
					</div>
				</div>
			</section>

			<section aria-label="Cards-Stats">
				<div className="title-section blue-container">
					<h1>Most Victorious Cards</h1>
				</div>
				<div id="top-cards" className="card-container">
					{playerStats && <CardContainer cards={playerStats.topCards} classSup={["flip"]} />}
				</div>
			</section>

			<section aria-label="Games-History">
				<GameHistoryTable />
			</section>
			<Link to="/lobby" className="arrow"></Link>
		</div>
	);
};

export default ProfilePage;
