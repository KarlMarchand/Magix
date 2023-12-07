import ReactDOM from "react-dom/client";
import App from "./App";
import { BrowserRouter } from "react-router-dom";
import "./sass/global.scss";
import Starfield from "@components/Starfield/Starfield";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
	<BrowserRouter>
		<Starfield />
		<App />
	</BrowserRouter>
);
