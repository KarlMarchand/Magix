import { Route, Routes } from "react-router-dom";
import { ProtectedRoute } from "./components/protectedRoute";
import { AuthProvider } from "./hooks/useAuth";
import LoginPage from "./pages/loginPage";

function App() {
	return (
		<AuthProvider>
			<Routes>
				<Route path="/" element={<ProtectedRoute />}>
					<Route path="/lobby"></Route>
					<Route path="/deck"></Route>
					<Route path="/profile"></Route>
					<Route path="/game"></Route>
				</Route>
				<Route path="/" element={<LoginPage />} />
			</Routes>
		</AuthProvider>
	);
}

export default App;
