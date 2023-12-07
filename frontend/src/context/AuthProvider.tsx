import { createContext, useContext, useMemo, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import useSessionStorage from "@hooks/UseSessionStorage";
import RequestHandler from "@utils/RequestHandler";
import UserLogin from "@customTypes/UserLogin";
import User from "@customTypes/UserProfile";
import ServerResponse from "@customTypes/ServerResponse";

interface AuthContextInterface {
	user: User | null;
	setUser: (user: User | null) => void;
	login: (userData: UserLogin) => Promise<ServerResponse<User>>;
	logout: () => void;
}

const authContextDefault: AuthContextInterface = {
	user: null,
	setUser: () => null,
	login: async () => {
		return {
			data: null,
			success: false,
			message: "You must login first",
		} as ServerResponse<User>;
	},
	logout: () => null,
};

const AuthContext = createContext<AuthContextInterface>(authContextDefault);

export const AuthProvider: React.FC<{ children: JSX.Element }> = ({ children }) => {
	const [user, setUser] = useSessionStorage("user", null);
	const navigate = useNavigate();

	const login = async (userData: UserLogin): Promise<ServerResponse<User>> => {
		const response = await RequestHandler.post<User>("player/login", userData);

		if (response.success && response.data) {
			setUser(response.data);
			RequestHandler.setAccessToken(response.data.token);
			localStorage.setItem("username", userData.username);
		}

		return response;
	};

	const logout = useCallback(async () => {
		try {
			const _ = await RequestHandler.get<string>("player/logout");
			setUser(null);
			navigate("/", { replace: true });
		} catch (error) {
			// Handle API call error
			console.error("API error during logout:", error);
		}
	}, [navigate, setUser]);

	const value = useMemo(
		() => ({
			user,
			setUser,
			login,
			logout,
		}),
		[user]
	);
	return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
	return useContext(AuthContext);
};
