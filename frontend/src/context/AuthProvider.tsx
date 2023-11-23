import { createContext, useContext, useMemo, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import useSessionStorage from "../hooks/UseSessionStorage";
import RequestHandler from "../utils/requestHandler";
import UserLogin from "../types/UserLogin";
import User from "../types/UserProfile";
import ServerResponse from "../types/ServerResponse";
import AuthHelper from "../utils/AuthHelper";

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

	const forceLogout = AuthHelper.forceLogout;

	const value = useMemo(
		() => ({
			user,
			setUser,
			login,
			logout,
			forceLogout,
		}),
		[user]
	);
	return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
	return useContext(AuthContext);
};
