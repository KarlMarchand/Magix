type ServerResponse<T> = {
	data: T | null;
	success: boolean;
	message: string;
};

export default ServerResponse;
