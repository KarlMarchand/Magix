import { useState, useRef, useCallback, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthProvider";
import User from "../types/UserProfile";
import UserLogin from "../types/UserLogin";
import ServerResponse from "../types/ServerResponse";
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
				<div id="login-page" className="d-flex flex-column align-items-center">
					<h1 className="yellow-title">
						MAGIX
						<br />
						WARS
					</h1>
					<div id="loginContainer" className="blue-container">
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
									className="text-left pl-4"
									autoComplete="off"
									onChange={(e: React.ChangeEvent<HTMLInputElement>) => setUsername(e.target.value)}
									value={username}
									required
								/>
							</div>
							<div id="passwordWrapper">
								<label htmlFor="password">Password</label>
								<input
									type="password"
									id="password"
									className="text-left"
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
