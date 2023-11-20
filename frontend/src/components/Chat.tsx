import { useEffect, useState } from "react";
import { RequestHandler } from "../utils/requestHandler";

const Chat: React.FC = () => {
	const [chatUrl, setChatUrl] = useState<string>("");

	useEffect(() => {
		if (chatUrl !== "") {
			let iframe = document.querySelector("#chat") as HTMLIFrameElement;
			let styles = {
				fontColor: "#fff",
				backgroundColor: "rgba(0, 0, 0, 0)",
				fontGoogleName: "Roboto",
				fontSize: "18px",
				hideIcons: true,
				inputBackgroundColor: "rgba(22, 104, 159, 0.2)",
				inputFontColor: "white",
				height: "100%",
				memberListFontColor: "#00aeff",
				memberListBackgroundColor: "rgba(25, 25, 25, 0.75)",
			};
			setTimeout(() => {
				iframe?.contentWindow?.postMessage(JSON.stringify(styles), "*");
			}, 100);
		}
	}, [chatUrl]);

	useEffect(() => {
		RequestHandler.get<string>("chat").then((response) => {
			if (response.success && response.data) {
				setChatUrl(response.data);
			}
		});
	}, []);

	return <iframe id="chat" src={chatUrl}></iframe>;
};

export default Chat;
