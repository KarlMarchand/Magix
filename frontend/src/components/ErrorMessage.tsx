import React, { useEffect, useState } from "react";

interface ErrorMessageProps {
	errorMessage: string;
	errorMessageHandler?: () => void;
}

const ErrorMessage: React.FC<ErrorMessageProps> = ({ errorMessage, errorMessageHandler }) => {
	const [shouldClose, setShouldClose] = useState<boolean>(true);

	const handleClick = () => {
		setShouldClose(true);
		errorMessageHandler?.();
	};

	useEffect(() => {
		setShouldClose(errorMessage != "");
	}, [errorMessage]);

	return (
		<>
			{shouldClose && (
				<div className="message-container">
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
