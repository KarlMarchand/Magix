import { ISprite } from "./Interfaces/isprite";
import shipImg from "../assets/img/Ship.png";

export class Ship implements ISprite {
	private img: HTMLImageElement = new Image();
	private position: { x: number; y: number } = { x: 0, y: 0 };
	private originalWidth: number = 2000;
	private originalHeight: number = 1058;
	private currentWidth: number = 0;
	private currentHeight: number = 0;
	private currentSizeRatio: number = 0;
	private speed: number = 0.02;
	private canvasCenter: { x: number; y: number };
	private animationFinished: boolean = false;
	private animationEndCallBack: (() => void) | null;

	/**
	 *
	 * @param canvasWidth
	 * @param canvasHeight
	 * @param animationEndCallback
	 */
	constructor(canvasWidth: number, canvasHeight: number, animationEndCallback?: (() => void) | null) {
		this.canvasCenter = { x: canvasWidth / 2, y: canvasHeight / 2 };
		this.img.src = shipImg;
		this.animationEndCallBack = animationEndCallback ?? null;
	}

	show(canvasContext: CanvasRenderingContext2D) {
		canvasContext.drawImage(this.img, this.position.x, this.position.y, this.currentWidth, this.currentHeight);
	}

	move() {
		if (this.currentSizeRatio < 1) {
			this.currentSizeRatio += this.speed;
			this.currentWidth = this.originalWidth * this.currentSizeRatio;
			this.currentHeight = this.originalHeight * this.currentSizeRatio;
			this.position.x = this.canvasCenter.x - 0.5 * this.currentWidth;
			this.position.y = this.canvasCenter.y - 0.5 * this.currentHeight;
		} else {
			if (!this.animationFinished) {
				this.animationFinished = true;
				if (this.animationEndCallBack != null) {
					this.animationEndCallBack();
				}
			}
		}
	}
}
