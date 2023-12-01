import React, { useEffect, useState } from "react";
import GameResult from "../types/GameResult";
import { Table, Pagination } from "react-bootstrap";
import RequestHandler from "../utils/requestHandler";

const pageSide = 30;

const GameHistoryTable: React.FC = () => {
	const [gamesHistory, setGamesHistory] = useState<GameResult[]>([]);
	const [currentPage, setCurrentPage] = useState<number>(1);
	const [totalPages, setTotalPages] = useState<number>(0);

	const fetchGameHistory = (page: number) => {
		RequestHandler.get<PaginatedResponse<GameResult>>(`game/history?pageNumber=${page}&pageSize=${pageSide}`)
			.then((response) => {
				if (response.success && response.data) {
					setGamesHistory(response.data.items);
					setTotalPages(response.data.totalPages);
					setCurrentPage(response.data.currentPage);
				}
			})
			.catch((error) => {
				console.error("Error fetching game history:", error);
			});
	};

	useEffect(() => {
		fetchGameHistory(currentPage);
	}, []);

	const renderPagination = () => {
		let items = [];

		const isFirstPage = currentPage === 1 || totalPages === 0;

		// Add First and Previous buttons
		items.push(
			<Pagination.First key="first" onClick={() => fetchGameHistory(1)} disabled={isFirstPage} />,
			<Pagination.Prev key="prev" onClick={() => fetchGameHistory(currentPage - 1)} disabled={isFirstPage} />
		);

		// Calculate page numbers to display
		let startPage = Math.max(1, currentPage - 2);
		let endPage = Math.min(totalPages === 0 ? 1 : currentPage, currentPage + 2);

		// Show ellipsis if startPage is more than 1
		if (startPage > 1) {
			items.push(<Pagination.Ellipsis key="startEllipsis" />);
		}

		// Show page numbers
		for (let number = startPage; number <= endPage; number++) {
			items.push(
				<Pagination.Item key={number} active={number === currentPage} onClick={() => fetchGameHistory(number)}>
					{number}
				</Pagination.Item>
			);
		}

		// Show ellipsis if endPage is less than totalPages
		if (endPage < totalPages) {
			items.push(<Pagination.Ellipsis key="endEllipsis" />);
		}

		// Add Next and Last buttons
		items.push(
			<Pagination.Next
				key="next"
				onClick={() => fetchGameHistory(currentPage + 1)}
				disabled={currentPage >= totalPages}
			/>,
			<Pagination.Last
				key="last"
				onClick={() => fetchGameHistory(totalPages)}
				disabled={currentPage >= totalPages}
			/>
		);

		return <Pagination>{items}</Pagination>;
	};

	return (
		<div className="d-flex flex-column align-items-center" data-bs-theme="dark">
			<Table striped bordered hover>
				<thead>
					<tr>
						<th className="text-white">Date</th>
						<th className="text-white">Win/Lose</th>
						<th className="text-white">Deck</th>
						<th className="text-white">Opponent</th>
					</tr>
				</thead>
				<tbody>
					{gamesHistory.map((game: GameResult) => {
						return (
							<tr key={game.id}>
								<td>{game.date.toLocaleDateString()}</td>
								{game.won ? <td>Victory</td> : <td style={{ color: "#F96D52" }}>Defeat</td>}
								<td>{game.deck?.name}</td>
								<td>{game.opponent}</td>
							</tr>
						);
					})}
				</tbody>
			</Table>
			<div className="pagination-controls">{renderPagination()}</div>
		</div>
	);
};

export default GameHistoryTable;
