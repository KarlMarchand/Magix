import React, { useEffect, useState } from "react";
import RequestHandler from "../utils/RequestHandler";
import PlayerStats from "../types/PlayerStats";
import ServerResponse from "../types/ServerResponse";
import { useAuth } from "../context/AuthProvider";
import "../sass/profileStyle.scss";
import GameHistoryTable from "../components/GameHistoryTable";
import BackButton from "../components/BackButton/BackButton";

const ProfilePage: React.FC = () => {
	const { user } = useAuth();
	const [playerStats, setPlayerStats] = useState<PlayerStats>();
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
		<div id="profile-page" className="d-flex flex-column align-items-center p-3 fade-in">
			<BackButton />
			<section aria-label="Player-Infos" className="w-75 mb-5">
				<div className="section-title mb-4 blue-container">
					<h1>{user?.username}</h1>
				</div>
				<div id="player-stats" className="container mt-5">
					<div className="row gap-5">
						<div className="d-flex flex-column col blue-container">
							<div id="trophy-wrap">
								<div id="trophy-img" />
							</div>
							<p className="stat-line align-self-center">
								Number of Trophies: <span>{user?.trophies}</span>
							</p>
							<p className="stat-line align-self-center">
								Best Trophy Score: <span>{user?.bestTrophyScore}</span>
							</p>
						</div>
						<div className="blue-container d-flex flex-column align-items-center col">
							<div id="graph" className="d-flex gap-5 my-3">
								<svg viewBox="0 0 64 64">
									<circle id="pie-wins" cx="32" cy="32" r="16" />
									<circle id="pie-loses" cx="32" cy="32" r="16" />
								</svg>
								<div className="d-flex flex-column justify-content-center mt-3">
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
							<p className="stat-line align-self-center">
								Number of Played Games: <span>{playerStats?.gamePlayed ?? 0}</span>
							</p>
						</div>
					</div>
				</div>
			</section>

			<section aria-label="Cards-Stats" className="w-75 mb-5">
				<div className="blue-container section-title mb-4">
					<h1>Most Victorious Cards</h1>
				</div>
				<div id="top-cards" className="card-container">
					{playerStats &&
						playerStats.topCards.map((card, i) => {
							return (
								<span className="flip" key={card.id}>
									{card.cardName}
								</span>
							);
						})}
				</div>
			</section>

			<section aria-label="Games-History" className="w-75 mb-5">
				<div className="blue-container section-title">
					<h1 className="mb-4">Game history</h1>
					<GameHistoryTable />
				</div>
			</section>
		</div>
	);
};

export default ProfilePage;
