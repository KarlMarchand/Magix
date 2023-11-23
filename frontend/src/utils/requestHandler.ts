import ServerResponse from "../types/ServerResponse";
import AuthHelper from "./AuthHelper";

class RequestHandler {
	private static BASE_API_URL: string = "https://localhost:7194/api/";
	private static accessToken: string | null = null;
	private static invalidTokenErrorString: string = "INVALID_KEY";

	static setAccessToken = (token: string) => {
		RequestHandler.accessToken = token;
	};

	static get = async <T>(url: string, options: globalThis.RequestInit = {}): Promise<ServerResponse<T>> => {
		return await RequestHandler.request<T>(url, {
			...options,
			method: "GET",
		});
	};

	static post = async <T>(
		url: string,
		body: object,
		options: globalThis.RequestInit = {}
	): Promise<ServerResponse<T>> => {
		return await RequestHandler.request<T>(url, {
			...options,
			method: "POST",
			body: JSON.stringify(body),
		});
	};

	static put = async <T>(
		url: string,
		body: object = {},
		options: globalThis.RequestInit = {}
	): Promise<ServerResponse<T>> => {
		return await RequestHandler.request<T>(url, {
			...options,
			method: "PUT",
			body: JSON.stringify(body),
		});
	};

	static delete = async <T>(url: string, options: globalThis.RequestInit = {}): Promise<ServerResponse<T>> => {
		return await RequestHandler.request<T>(url, {
			...options,
			method: "DELETE",
		});
	};

	private static request = async <T>(
		url: string,
		options: globalThis.RequestInit = {}
	): Promise<ServerResponse<T>> => {
		options.headers = {
			Authorization: RequestHandler.accessToken ? `Bearer ${RequestHandler.accessToken}` : "",
			"Content-Type": "application/json",
		};

		const response: Response = await fetch(RequestHandler.BASE_API_URL.concat(url), options);

		const result = await response.json();

		if (typeof result === "string" && result === this.invalidTokenErrorString) {
			this.accessToken = null;
			AuthHelper.forceLogout();
		}

		return result;
	};
}

export default RequestHandler;
