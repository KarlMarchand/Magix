import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../context/AuthProvider";

const ProtectedRoute: React.FC = () => {
	const { user } = useAuth();
	if (!user) {
		// user is not authenticated
		return <Navigate to="/login" />;
	}
	return (
		<div>
			<Outlet />
		</div>
	);
};

export default ProtectedRoute;
