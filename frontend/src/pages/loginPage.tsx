import { useState, useRef, useCallback, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthProvider";
import "../sass/loginStyle.scss";
import { User } from "../types/userProfile";
import { UserLogin } from "../types/userLogin";
import { ServerResponse } from "../types/serverResponse";
import Starfield from "../components/Starfield";

const LoginPage: React.FC = () => {
	const navigate = useNavigate();
	const { setUser, login } = useAuth();
	const userRef = useRef<HTMLInputElement>(null);
	const errRef = useRef<HTMLDivElement>(null);
	const [credentialsAreValid, setCredentialsAreValid] = useState<boolean>(false);
	const [isAnimationComplete, setIsAnimationComplete] = useState<boolean>(false);

	const [username, setUsername] = useState<string>(localStorage.getItem("username") ?? "");
	const [pwd, setPwd] = useState<string>("");
	const [errMsg, setErrMsg] = useState<string>("");

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
				setCredentialsAreValid(true);
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
			{!credentialsAreValid && (
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
											onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
												setPwd(e.target.value)
											}
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
			)}
			<Starfield
				addSpriteToList={credentialsAreValid}
				onAnimationComplete={function (): void {
					setIsAnimationComplete(true);
					setTimeout(() => {
						navigate("/lobby");
					}, 4000);
				}}
			/>
			{isAnimationComplete && <div className="cover closing"></div>}
		</>
	);
};

export default LoginPage;
