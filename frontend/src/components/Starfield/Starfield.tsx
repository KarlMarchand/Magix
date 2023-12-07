import { useEffect, useRef } from "react";
import ISprite from "@customTypes/interfaces/Isprite";
import Ship from "@customTypes/Ship";
import Star from "@customTypes/Star";
import "./starfield.scss";

interface StarfieldProps {
	addSpriteToList?: boolean | null;
	onAnimationComplete?: (() => void) | null;
}

const Starfield: React.FC<StarfieldProps> = ({ addSpriteToList, onAnimationComplete }) => {
	const canvasRef = useRef<HTMLCanvasElement>(null);
	const spriteListRef = useRef<ISprite[]>([]);

	// Initialize stars
	useEffect(() => {
		const canvas = canvasRef.current;
		if (canvas) {
			canvas.width = window.innerWidth;
			canvas.height = window.innerHeight;
			const stars = Array.from(
				{ length: calculateNbrStars(canvas) },
				() => new Star(window.innerWidth, window.innerHeight)
			);
			spriteListRef.current = stars;
			const context = canvas.getContext("2d");
			if (context) {
				const animate = () => tick(context);
				requestAnimationFrame(animate);
			}
		}
	}, []);

	// Resize handler
	useEffect(() => {
		const handleResize = () => {
			const canvas = canvasRef.current;
			if (canvas) {
				canvas.width = window.innerWidth;
				canvas.height = window.innerHeight;
				const stars = Array.from(
					{ length: calculateNbrStars(canvas) },
					() => new Star(window.innerWidth, window.innerHeight)
				);
				spriteListRef.current = stars;
			}
		};

		window.addEventListener("resize", handleResize);
		return () => window.removeEventListener("resize", handleResize);
	}, []);

	// Add ship to sprite list
	useEffect(() => {
		if (addSpriteToList && canvasRef.current) {
			const ship = new Ship(canvasRef.current.width, canvasRef.current.height, onAnimationComplete);
			spriteListRef.current = [...spriteListRef.current, ship];
		}
	}, [addSpriteToList]);

	const tick = (context: CanvasRenderingContext2D) => {
		const canvas = canvasRef.current;
		if (canvas && context) {
			context.fillStyle = "black";
			context.fillRect(0, 0, canvas.width, canvas.height);
			spriteListRef.current.forEach((sprite) => {
				sprite.show(context);
				sprite.move();
			});
			requestAnimationFrame(() => tick(context));
		}
	};

	const calculateNbrStars = (canvas: HTMLCanvasElement): number => {
		return (canvas.width * canvas.height) / 2000;
	};

	return <canvas className="starfield" ref={canvasRef} width={window.innerWidth} height={window.innerHeight} />;
};

export default Starfield;
