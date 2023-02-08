export const URL = "http://localhost:5028/api";
export const API_URL_USERS = URL + "/Player/";

export const getRequest = async (url: string) => {
	const response = await fetch(url, {
		method: "GET",
		headers: {
			"Content-Type": "application/json",
		},
	});

	const res = await response.json();
	return res;
};

export const request = async (
	url: string,
	data: object,
	accessToken: string | null = null,
	requestMethod: string = "POST"
) => {
	const headers: HeadersInit = {
		Authorization: accessToken ? `Bearer ${accessToken}` : "",
		"Content-Type": "application/json",
	};

	const response = await fetch(url, {
		method: requestMethod,
		headers,
		body: JSON.stringify(data),
	});

	const res = await response.json();
	return res;
};
