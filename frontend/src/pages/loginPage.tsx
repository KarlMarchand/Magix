import { useState, useRef, useCallback, useEffect } from "react";
import { useAuth } from "../context/AuthProvider";
import "../sass/loginStyle.scss";
import { User } from "../types/userProfile";
import { UserLogin } from "../types/userLogin";
import { ServerResponse } from "../types/serverResponse";

const LoginPage: React.FC = () => {
	const { setUser, login } = useAuth();
	const userRef = useRef<HTMLInputElement>(null);
	const errRef = useRef<HTMLDivElement>(null);

	const [username, setUsername] = useState(localStorage.getItem("username") ?? "");
	const [pwd, setPwd] = useState("");
	const [errMsg, setErrMsg] = useState("");

	useEffect(() => {
		userRef.current?.focus();
	}, []);

	useEffect(() => {
		if (errMsg !== "") {
			setErrMsg("");
		}
	}, [username, pwd]);

	const submitCredentials = useCallback(async (logins: UserLogin) => {
		login(logins).then((response: ServerResponse<User>) => {
			if (response.success && response.data) {
				setUser(response.data);
			} else {
				setErrMsg("Votre nom d'usager ou votre mot de passe est invalide.");
				setPwd("");
			}
		});
	}, []);

	const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
		e.preventDefault();
		submitCredentials({ username, password: pwd });
	};

	return (
		<>
			<main>
				<div className="flex-column">
					<h1>
						MAGIX
						<br />
						WARS
					</h1>
					<div id="loginContainer" className="container">
						<div className="subContainer">
							<div ref={errRef} className="error" aria-live="assertive">
								{errMsg}
							</div>
							<form id="form" onSubmit={handleSubmit} method="post">
								<div id="usernameWrapper">
									<label htmlFor="username">Username</label>
									<input
										type="text"
										id="username"
										ref={userRef}
										autoComplete="off"
										onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
											setUsername(e.target.value)
										}
										value={username}
										required
									/>
								</div>
								<div id="passwordWrapper">
									<label htmlFor="password">Password</label>
									<input
										type="password"
										id="password"
										onChange={(e: React.ChangeEvent<HTMLInputElement>) => setPwd(e.target.value)}
										value={pwd}
										required
									/>
								</div>
								<div>
									<button className="btn" id="btn-connexion">
										Connection
									</button>
								</div>
							</form>
						</div>
					</div>
				</div>
			</main>
			<canvas id="starfield" />
			<div className="cover"></div>
		</>
	);
};

export default LoginPage;
