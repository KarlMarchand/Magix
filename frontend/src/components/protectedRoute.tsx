import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../context/AuthProvider";
import RequestHandler from "../utils/requestHandler";

const ProtectedRoute: React.FC = () => {
	const { user } = useAuth();
	if (!user) {
		// user is not authenticated
		return <Navigate to="/login" />;
	} else {
		RequestHandler.setAccessToken(user.token);
	}
	return <Outlet />;
};

export default ProtectedRoute;
