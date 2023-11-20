import React, { useRef, useEffect, useState } from "react";
import { Tween, Easing } from "@tweenjs/tween.js";
import * as THREE from "three";
import { GLTFLoader } from "three/examples/jsm/loaders/GLTFLoader";
import { EffectComposer } from "three/examples/jsm/postprocessing/EffectComposer.js";
import { OrbitControls } from "three/examples/jsm/controls/OrbitControls.js";
import {
	InitProjection,
	InitTable,
	InitCamera,
	InitComposer,
	InitControls,
	InitLights,
	InitModels,
	InitRenderer,
	CleanScene,
} from "./ThreeJs/ThreeJsInit";

interface ThreeSceneProps {
	startGameAnimation: boolean;
	onAnimationEndCallback: () => void;
	canControl?: boolean;
}

const ThreeScene: React.FC<ThreeSceneProps> = ({ startGameAnimation, onAnimationEndCallback, canControl = false }) => {
	const canvasRef = useRef<HTMLCanvasElement>(null);
	const [scene, setScene] = useState<THREE.Scene>(new THREE.Scene());
	const [dsProjection, setDsProjection] = useState<THREE.Group | null>(null);
	const [table, setTable] = useState<THREE.Mesh | null>(null);
	const [blueLight, setBlueLight] = useState<THREE.SpotLight | null>(null);
	const [camera, setCamera] = useState<THREE.PerspectiveCamera | null>(null);
	const [renderer, setRenderer] = useState<THREE.WebGLRenderer | null>(null);
	const [composer, setComposer] = useState<EffectComposer | null>(null);
	const [controls, setControls] = useState<OrbitControls | null>(null);

	useEffect(() => {
		// Initialization of the scene and its components
		const canvas = canvasRef.current;
		if (!canvas) return;

		const holoTexture = new THREE.TextureLoader().load("./static/img/hologram.png");
		const gltfLoader = new GLTFLoader();

		setDsProjection(InitProjection(scene, holoTexture, gltfLoader));
		setTable(InitTable(scene, holoTexture));
		InitModels(scene, gltfLoader);
		setBlueLight(InitLights(scene));

		const cameraInstance = InitCamera(scene, window.innerWidth, window.innerHeight);
		setCamera(cameraInstance);

		const rendererInstance = InitRenderer(canvas, window.innerWidth, window.innerHeight);
		setRenderer(renderer);

		const composerInstance = InitComposer(scene, cameraInstance, rendererInstance);
		setComposer(composerInstance);

		if (canControl) {
			setControls(InitControls(canvas, cameraInstance));
		}

		// Resize Listener
		const onResizeHandler = () => {
			cameraInstance.aspect = window.innerWidth / window.innerHeight;
			cameraInstance.updateProjectionMatrix();
			rendererInstance.setSize(window.innerWidth, window.innerHeight);
			rendererInstance.setPixelRatio(Math.min(window.devicePixelRatio, 2));
			composerInstance.setSize(window.innerWidth, window.innerHeight);
		};
		window.addEventListener("resize", onResizeHandler);

		// Animation Loop
		const animate = () => {
			if (controls) {
				controls.update();
			}
			rendererInstance.render(scene, cameraInstance);
			dsProjection?.rotateY(0.01);
			composer?.render();
			requestAnimationFrame(animate);
		};
		animate();

		// Cleanup
		return () => {
			window.removeEventListener("resize", onResizeHandler);
			CleanScene(scene, renderer, composer, controls);
		};
	}, [canControl]);

	useEffect(() => {
		if (startGameAnimation && scene && camera) {
			setTimeout(() => {
				if (dsProjection) dsProjection.visible = false;
				setTimeout(() => {
					if (blueLight) blueLight.visible = true;
					if (table) table.visible = true;
					performAnimation(camera);
				}, 500);
			}, 1000);
		}
	}, [startGameAnimation, scene, camera]);

	const performAnimation = (camera: THREE.PerspectiveCamera) => {
		// Animation logic using tween.js
		const startPosition = camera.position.clone();
		const targetPosition = new THREE.Vector3(/* target coordinates */);
		const tween = new Tween(startPosition)
			.to(targetPosition, 2000) // duration in ms
			.easing(Easing.Quadratic.Out)
			.onUpdate(() => {
				camera.position.copy(startPosition);
				camera.lookAt(scene.position);
			})
			.onComplete(() => {
				canvasRef.current?.classList.add("fade-out");
				setTimeout(() => {
					onAnimationEndCallback(); // Notify parent component
				}, 2000); // match with fade-out duration
			})
			.start();

		const animate = () => {
			tween.update(); // Update tween animations
			scene.getObjectByName("R2D2")?.lookAt(0, 0, camera.position.z);
			scene.getObjectByName("C3PO")?.lookAt(0, 0, camera.position.z);
			requestAnimationFrame(animate);
		};
		animate();
	};

	return <canvas ref={canvasRef} className="webgl" />;
};

export default ThreeScene;
