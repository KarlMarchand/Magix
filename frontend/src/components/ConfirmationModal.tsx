import React, { useEffect, useState } from "react";
import { Modal, ModalProps } from "react-bootstrap";

interface ConfirmationModalProps extends ModalProps {
	show: boolean;
	message: string;
	onContinueCallback: () => void;
}

const ConfirmationModal: React.FC<ConfirmationModalProps> = ({ show, message, onContinueCallback, ...props }) => {
	const [isOpen, setIsOpen] = useState<boolean>(props.show);

	const handleCancel = () => {
		setIsOpen(false);
		props.onHide?.();
	};

	useEffect(() => {
		setIsOpen(show);
	}, [show]);

	return (
		<Modal {...props} centered show={isOpen}>
			<Modal.Header>
				<Modal.Title>Confirmation</Modal.Title>
			</Modal.Header>
			<Modal.Body>
				<p>{message}</p>
			</Modal.Body>
			<Modal.Footer>
				<div className="d-flex w-100 px-1">
					<button onClick={onContinueCallback} className="custom-btn w-50 me-2">
						Continue
					</button>
					<button onClick={handleCancel} className="custom-btn w-50 ms-2" style={{ opacity: 0.5 }}>
						Cancel
					</button>
				</div>
			</Modal.Footer>
		</Modal>
	);
};

export default ConfirmationModal;
