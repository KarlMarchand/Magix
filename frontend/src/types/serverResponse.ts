export type ServerResponse<T> = {
	data: T | null;
	success: boolean;
	message: string;
};
