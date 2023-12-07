import "./backButton.scss";
import { Link } from "react-router-dom";

export const BackButton = () => {
	return <Link to="/lobby" className="arrow"></Link>;
};

export default BackButton;
