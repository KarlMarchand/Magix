import { createContext, useContext, useMemo, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { useSessionStorage } from "../hooks/useSessionStorage";
import { RequestHandler } from "../utils/requestHandler";
import { UserLogin } from "../types/userLogin";
import { User } from "../types/userProfile";
import { ServerResponse } from "../types/serverResponse";

interface AuthContextInterface {
	user: User | null;
	setUser: (user: User) => void;
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

		if (response.success && response.data?.token) {
			RequestHandler.setAccessToken(response.data.token);
			localStorage.setItem("username", userData.username);
		}

		return response;
	};

	const logout = useCallback(() => {
		setUser(null);
		navigate("/", { replace: true });
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
