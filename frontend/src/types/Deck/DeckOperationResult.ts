type DeckOperationResult<T = void> = {
	isSuccessful: boolean;
	error?: string;
	data?: T;
};

export default DeckOperationResult;
