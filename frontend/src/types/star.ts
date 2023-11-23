import ISprite from "./Interfaces/Isprite";

class Star implements ISprite {
	private color: string = "white";
	private position: { x: number; y: number; z: number };
	private canvasCenter: { x: number; y: number };
	private maxWidth: number;
	private speed: number = 5;

	constructor(canvasWidth: number, canvasHeight: number) {
		this.maxWidth = canvasWidth;
		this.position = {
			x: Math.random() * canvasWidth,
			y: Math.random() * canvasHeight,
			z: Math.random() * canvasWidth,
		};
		this.canvasCenter = {
			x: canvasWidth / 2,
			y: canvasHeight / 2,
		};
	}

	show(canvasContext: CanvasRenderingContext2D) {
		const x = (this.position.x - this.canvasCenter.x) * (this.maxWidth / this.position.z) + this.canvasCenter.x;
		const y = (this.position.y - this.canvasCenter.y) * (this.maxWidth / this.position.z) + this.canvasCenter.y;
		const s = this.maxWidth / this.position.z;
		canvasContext.beginPath();
		canvasContext.fillStyle = this.color;
		canvasContext.arc(x, y, s, 0, Math.PI * 2);
		canvasContext.fill();
	}

	move() {
		this.position.z = this.position.z - this.speed;
		if (this.position.z <= 0) {
			this.position.z = this.maxWidth;
		}
	}
}

export default Star;
