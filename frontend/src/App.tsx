import { Route, Routes } from "react-router-dom";
import { ProtectedRoute } from "./components/protectedRoute";
import { AuthProvider } from "./context/AuthProvider";
import LoginPage from "./pages/loginPage";
import LobbyPage from "./pages/lobbyPage";
import GamePage from "./pages/gamePage";
import DeckPage from "./pages/deckPage";
import ProfilePage from "./pages/profilePage";

function App() {
	return (
		<AuthProvider>
			<Routes>
				<Route path="/" element={<ProtectedRoute />}>
					<Route path="/lobby" element={<LobbyPage />}></Route>
					<Route path="/deck" element={<DeckPage />}></Route>
					<Route path="/profile" element={<ProfilePage />}></Route>
					<Route path="/game" element={<GamePage />}></Route>
				</Route>
				<Route path="/login" element={<LoginPage />} />
			</Routes>
		</AuthProvider>
	);
}

export default App;
