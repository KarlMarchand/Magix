import { createContext, useContext, useMemo, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { useLocalStorage } from "../hooks/useLocalStorage";
import { request, getRequest, API_URL_USERS } from "../utils/requestHandler";
import { ErrorMessage, User, UserLogin } from "../types/userTypes";

interface AuthContextInterface {
	user: string;
	setUser: (user: User) => void;
	login: (userData: UserLogin) => Promise<User>;
	logout: () => void;
}

const authContextDefault: AuthContextInterface = {
	user: "",
	setUser: () => null,
	login: async () => {
		return {};
	},
	logout: () => null,
};

const AuthContext = createContext<AuthContextInterface>(authContextDefault);

export const AuthProvider: React.FC<{ children: JSX.Element }> = ({ children }) => {
	const [user, setUser] = useLocalStorage("user", null);
	const navigate = useNavigate();

	const login = async (userData: UserLogin): Promise<User | ErrorMessage> => {
		const res = await getRequest(API_URL_USERS + "GetAll");
		console.log(res);
		return {}; //request(API_URL_USERS, userData);
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
