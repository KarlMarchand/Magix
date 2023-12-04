import React from "react";
import { useAuth } from "../../context/AuthProvider";
import "./avatar.scss";

interface AvatarProps {
	playerClassName?: string;
}

const Avatar: React.FC<AvatarProps> = ({ playerClassName }) => {
	const { user } = useAuth();

	const getAvatarImageUrl = () => {
		const className = playerClassName || user?.className || "AcePilot";
		return `url("assets/img/classe/${className.replace(/\s+/g, "")}.webp")`;
	};

	const avatarImageUrl = getAvatarImageUrl();
	const avatarStyle = {
		backgroundImage: `${avatarImageUrl}, linear-gradient(to bottom right, var(--light-blue), var(--purple), var(--light-blue))`,
	};

	return <div className="avatar" style={avatarStyle}></div>;
};

export default Avatar;
