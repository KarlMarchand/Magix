import { Route, Routes } from "react-router-dom";
import { AuthProvider } from "@context/AuthProvider";
import ProtectedRoute from "@components/ProtectedRoute";
import LoginPage from "@pages/LoginPage";
import LobbyPage from "@pages/LobbyPage";
import GamePage from "@pages/GamePage";
import DeckPage from "@pages/DeckPage";
import ProfilePage from "@pages/ProfilePage";
import { DeckManagerProvider } from "@context/deck_manager_context/DeckManagerContext";
import GameOverScreen from "@components/game/game_over_screen/GameOverScreen";
import LoadingScreen from "@components/loading_screen/LoadingScreen";
import { GameOptionsProvider } from "@context/GameOptionsProvider";

function App() {
	return (
		<AuthProvider>
			<GameOptionsProvider>
				<Routes>
					<Route path="/" element={<ProtectedRoute />}>
						<Route path="/lobby" element={<LobbyPage />}></Route>
						<Route
							path="/deck"
							element={
								<DeckManagerProvider>
									<DeckPage />
								</DeckManagerProvider>
							}
						></Route>
						<Route path="/profile" element={<ProfilePage />}></Route>
						<Route path="/game" element={<GamePage />}></Route>
						<Route path="/gameover" element={<GameOverScreen isVictory={true} />}></Route>
						<Route path="/loading" element={<LoadingScreen />}></Route>
					</Route>
					<Route path="/login" element={<LoginPage />} />
				</Routes>
			</GameOptionsProvider>
		</AuthProvider>
	);
}

export default App;
