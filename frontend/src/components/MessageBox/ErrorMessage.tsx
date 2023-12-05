import React, { useEffect, useState } from "react";
import "./messageBox.scss";

interface ErrorMessageProps extends React.HTMLProps<HTMLDivElement> {
	errorMessage: string;
	errorMessageHandler?: () => void;
}

const ErrorMessage: React.FC<ErrorMessageProps> = ({ errorMessage, errorMessageHandler, className, ...htmlProps }) => {
	const [shouldClose, setShouldClose] = useState<boolean>(true);

	const handleClick = () => {
		setShouldClose(true);
		errorMessageHandler?.();
	};

	useEffect(() => {
		setShouldClose(errorMessage === "");
	}, [errorMessage]);

	return (
		<>
			{!shouldClose && (
				<div {...htmlProps} className={`message-container ${className}`}>
					<div className="alert-message alert-danger" onClick={handleClick}>
						<span>Error</span>
						{errorMessage}
					</div>
				</div>
			)}
		</>
	);
};

export default ErrorMessage;
