import { useEffect, useState } from "react";
import RequestHandler from "../utils/requestHandler";

const Chat: React.FC = () => {
	const [chatUrl, setChatUrl] = useState<string>("");

	useEffect(() => {
		if (chatUrl !== "") {
			const iframe = document.querySelector("#chat") as HTMLIFrameElement;
			const styles = {
				fontColor: "#fff",
				backgroundColor: "rgba(0, 0, 0, 0)",
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
			}, 1000);
		}
	}, [chatUrl]);

	useEffect(() => {
		if (!chatUrl) {
			RequestHandler.get<string>("chat").then((response) => {
				if (response.success && response.data) {
					setChatUrl(response.data);
				}
			});
		}
	}, []);

	return <>{chatUrl !== "" && <iframe id="chat" className="h-100" src={chatUrl}></iframe>}</>;
};

export default Chat;
