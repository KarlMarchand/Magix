import { createContext, useContext, useMemo, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { useLocalStorage } from "./useLocalStorage";
import { request, API_URL_USERS } from "../utils/requestHandler";
import { UserLogin } from "../types/userTypes";

const AuthContext = createContext({ user: null });

export const AuthProvider: React.FC<{ children: JSX.Element }> = ({ children }) => {
	const [user, setUser] = useLocalStorage("user", null);
	const navigate = useNavigate();

	const login = async (userData: UserLogin) => {
		return request(API_URL_USERS, userData);
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
