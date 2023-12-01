import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { BrowserRouter } from "react-router-dom";
import "./sass/global.scss";
import Starfield from "./components/Starfield";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
	<React.StrictMode>
		<BrowserRouter>
			<Starfield />
			<App />
		</BrowserRouter>
	</React.StrictMode>
);
