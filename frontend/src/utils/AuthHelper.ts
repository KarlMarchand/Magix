import { useAuth } from "../context/AuthProvider";

const AuthHelper = {
	forceLogout: () => {
		const { setUser } = useAuth();
		setUser(null);
		window.location.href = "/login"; // Redirect to login page
	},
};

export default AuthHelper;
