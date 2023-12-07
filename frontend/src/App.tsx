import { Route, Routes } from "react-router-dom";
import { AuthProvider } from "@context/AuthProvider";
import ProtectedRoute from "@components/ProtectedRoute";
import LoginPage from "@pages/LoginPage";
import LobbyPage from "@pages/LobbyPage";
import GamePage from "@pages/GamePage";
import DeckPage from "@pages/DeckPage";
import ProfilePage from "@pages/ProfilePage";
import { DeckManagerProvider } from "@context/DeckManagerContext/DeckManagerContext";

function App() {
	return (
		<AuthProvider>
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
				</Route>
				<Route path="/login" element={<LoginPage />} />
			</Routes>
		</AuthProvider>
	);
}

export default App;
