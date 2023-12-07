import React from "react";
import "./avatar.scss";
import heroesImagery from "@context/deck_manager_context/imageries/heroesImagery";

interface AvatarProps extends React.HTMLProps<HTMLDivElement> {
	playerClassName?: string;
}

const Avatar: React.FC<AvatarProps> = ({ playerClassName, className, ...htmlProps }) => {
	const getAvatarImageUrl = () => {
		const className = playerClassName || "AcePilot";
		return heroesImagery[className.replace(/\s+/g, "")];
	};

	const avatarImageUrl = getAvatarImageUrl();
	const avatarStyle = {
		backgroundImage: `url(${avatarImageUrl}), linear-gradient(to bottom right, var(--light-blue), var(--purple), var(--light-blue))`,
	};

	return <div {...htmlProps} className={`avatar ${className ? className : ""}`} style={avatarStyle}></div>;
};

export default Avatar;
