import { useState, useRef, useCallback, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthProvider";
import User from "../types/UserProfile";
import UserLogin from "../types/UserLogin";
import ServerResponse from "../types/ServerResponse";

const LoginPage: React.FC = () => {
	const navigate = useNavigate();
	const { setUser, login } = useAuth();
	const userRef = useRef<HTMLInputElement>(null);
	const [credentialsAreValid, setCredentialsAreValid] = useState<boolean>(false);
	const [username, setUsername] = useState<string>(localStorage.getItem("username") ?? "");
	const [pwd, setPwd] = useState<string>("");
	const [errorMessage, setErrorMessage] = useState<string>("");

	useEffect(() => {
		userRef.current?.focus();
	}, []);

	const submitCredentials = useCallback(async (logins: UserLogin) => {
		login(logins).then((response: ServerResponse<User>) => {
			if (response.success && response.data) {
				setUser(response.data);
				setCredentialsAreValid(true);
				setTimeout(() => {
					navigate("/lobby");
				}, 2000);
			} else {
				setErrorMessage("Votre nom d'usager ou votre mot de passe est invalide.");
				setPwd("");
			}
		});
	}, []);

	const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
		e.preventDefault();
		submitCredentials({ username, password: pwd });
	};

	return (
		<div
			id="login-page"
			className={`d-flex flex-column align-items-center ${credentialsAreValid ? "fade-out" : ""}`}
		>
			<h1 className="yellow-title">
				MAGIX
				<br />
				WARS
			</h1>
			<div id="loginContainer" className="blue-container">
				<form id="form" onSubmit={handleSubmit} method="post" className="d-flex flex-column align-items-center">
					<div className="d-flex align-items-center mb-2">
						<label htmlFor="username">Username</label>
						<input
							type="text"
							id="username"
							ref={userRef}
							className="custom-input m-2"
							autoComplete="off"
							onChange={(e: React.ChangeEvent<HTMLInputElement>) => setUsername(e.target.value)}
							value={username}
							required
						/>
					</div>
					<div className="d-flex align-items-center">
						<label htmlFor="password">Password</label>
						<input
							type="password"
							id="password"
							className="custom-input m-2"
							onChange={(e: React.ChangeEvent<HTMLInputElement>) => setPwd(e.target.value)}
							value={pwd}
							required
						/>
					</div>
					<div>
						<button className="custom-btn mt-3" id="custom-btn-connexion">
							Connection
						</button>
					</div>
				</form>
			</div>
			<div className="message-container mt-5">
				{errorMessage !== "" && (
					<div className="alert-message alert-danger" onClick={() => setErrorMessage("")}>
						<span>Error</span>
						{errorMessage}
					</div>
				)}
			</div>
		</div>
	);
};

export default LoginPage;
