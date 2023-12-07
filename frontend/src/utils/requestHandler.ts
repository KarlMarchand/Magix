import ServerResponse from "@customTypes/ServerResponse";

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

		let response: Response;
		let result: ServerResponse<T> = { success: false, data: null, message: "" };

		try {
			response = await fetch(RequestHandler.BASE_API_URL.concat(url), options);

			if (response.ok) {
				result = await response.json();

				if (typeof result.data === "string" && result.data === this.invalidTokenErrorString) {
					this.handleUnauthorized();
				}
			} else if (response.status === 403) {
				this.handleUnauthorized();
			}
		} catch (error: any) {
			result.message = "Error while processing the request";
		} finally {
			return result;
		}
	};

	private static handleUnauthorized = () => {
		this.accessToken = null;
		window.sessionStorage.removeItem("user");
		window.location.href = "/login";
	};
}

export default RequestHandler;
