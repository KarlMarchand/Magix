import * as THREE from "three";
import { GLTFLoader } from "three/examples/jsm/loaders/GLTFLoader";
import { EffectComposer } from "three/examples/jsm/postprocessing/EffectComposer.js";
import { RenderPass } from "three/examples/jsm/postprocessing/RenderPass.js";
import { UnrealBloomPass } from "three/examples/jsm/postprocessing/UnrealBloomPass";
import { OrbitControls } from "three/examples/jsm/controls/OrbitControls.js";

export const InitProjection = (scene: THREE.Scene, holoTexture: THREE.Texture, gltfLoader: GLTFLoader): THREE.Group => {
	const dsProjection = new THREE.Group();

	const sphereGeo = new THREE.SphereGeometry(0.7, 25, 25);
	const sphereMat = new THREE.MeshBasicMaterial({
		color: 0x00ffff,
		map: holoTexture,
		transparent: true,
		opacity: 0.2,
	});
	const sphere = new THREE.Mesh(sphereGeo, sphereMat);
	sphere.translateY(2.25);
	dsProjection.add(sphere);

	const cylinderGeo = new THREE.CylinderGeometry(0.4, 0.2, 1.4, 10, 10, true);
	const cylinderMat = new THREE.MeshBasicMaterial({
		color: 0x00ffff,
		map: holoTexture,
		transparent: true,
		opacity: 0.2,
	});
	const cylinder = new THREE.Mesh(cylinderGeo, cylinderMat);
	cylinder.translateY(1);
	dsProjection.add(cylinder);

	gltfLoader.load("assets/threeJsModels/deathStar/scene.gltf", (gltf) => {
		gltf.scene.scale.set(0.0001, 0.0001, 0.0001);
		gltf.scene.translateY(2.25);
		gltf.scene.rotateY(1);
		gltf.scene.name = "DS";
		dsProjection.add(gltf.scene);
	});

	scene.add(dsProjection);

	return dsProjection;
};

export const InitTable = (scene: THREE.Scene, holoTexture: THREE.Texture): THREE.Mesh => {
	const tableGeo = new THREE.CylinderGeometry(1.3, 1.3, 0.1, 100, 1, false);
	const tableMat = new THREE.MeshBasicMaterial({
		color: 0x0055ff,
		map: holoTexture,
		transparent: true,
		opacity: 0.7,
	});
	const table = new THREE.Mesh(tableGeo, tableMat);
	table.translateY(1);
	table.rotateY(4.75);
	table.visible = false;

	scene.add(table);

	return table;
};

export const InitModels = (scene: THREE.Scene, gltfLoader: GLTFLoader): void => {
	gltfLoader.load("assets/threeJsModels/room/scene.gltf", (gltf) => {
		scene.add(gltf.scene);
	});

	gltfLoader.load("assets/threeJsModels/c3PO/scene.gltf", (gltf) => {
		gltf.scene.scale.set(0.01, 0.01, 0.01);
		gltf.scene.rotateY(3.7);
		gltf.scene.translateX(-2.25);
		gltf.scene.translateZ(0.75);
		gltf.scene.name = "C3PO";
		scene.add(gltf.scene);
	});

	gltfLoader.load("assets/threeJsModels/r2/scene.gltf", (gltf) => {
		gltf.scene.scale.set(1.25, 1.25, 1.25);
		gltf.scene.rotateY(2.75);
		gltf.scene.translateZ(1);
		gltf.scene.translateX(2);
		gltf.scene.name = "R2D2";
		scene.add(gltf.scene);
	});
};

export const InitLights = (scene: THREE.Scene): THREE.SpotLight => {
	const pointLight2 = new THREE.PointLight(0xaaaadd, 0.5);
	pointLight2.position.x = 0;
	pointLight2.position.y = 10;
	pointLight2.position.z = -5;
	scene.add(pointLight2);
	const rectLight = new THREE.RectAreaLight(0xffffff, 3, 1000, 1000);
	rectLight.position.x = 20;
	rectLight.position.y = 0;
	rectLight.position.z = 20;
	rectLight.lookAt(-10, 0, -10);
	scene.add(rectLight);
	scene.add(new THREE.AmbientLight(0x404040));
	const blueLight = new THREE.SpotLight(0x0000ff, 20, 3);
	blueLight.position.set(0, 3, 0);
	blueLight.visible = false;
	scene.add(blueLight);

	return blueLight;
};

export const InitCamera = (scene: THREE.Scene, width: number, height: number): THREE.PerspectiveCamera => {
	const camera = new THREE.PerspectiveCamera(75, width / height, 0.1, 100);
	camera.position.x = 0;
	camera.position.y = 2;
	camera.position.z = -4;
	camera.rotateY(3.15);
	camera.lookAt(0, 0, 0);
	scene.add(camera);

	return camera;
};

export const InitRenderer = (canvas: HTMLCanvasElement, width: number, height: number): THREE.WebGLRenderer => {
	const renderer = new THREE.WebGLRenderer({
		canvas: canvas,
	});
	// Set the output color space to sRGB
	renderer.outputColorSpace = THREE.SRGBColorSpace;
	// Set the size of the renderer
	renderer.setSize(width, height);

	return renderer;
};

export const InitComposer = (
	scene: THREE.Scene,
	camera: THREE.PerspectiveCamera,
	renderer: THREE.WebGLRenderer
): EffectComposer => {
	const composer = new EffectComposer(renderer);
	composer.addPass(new RenderPass(scene, camera));

	const bloomPass = new UnrealBloomPass(new THREE.Vector2(window.innerWidth, window.innerHeight), 0.2, 0, 0.95);
	composer.addPass(bloomPass);

	return composer;
};

export const InitControls = (canvas: HTMLCanvasElement, camera: THREE.PerspectiveCamera): OrbitControls => {
	const controls = new OrbitControls(camera, canvas);
	controls.enableDamping = true;

	return controls;
};

export const CleanScene = (
	scene: THREE.Scene,
	renderer?: THREE.WebGLRenderer | null,
	composer?: EffectComposer | null,
	controls?: OrbitControls | null
): void => {
	scene.traverse((object) => {
		if (object instanceof THREE.Mesh) {
			if (object.geometry) object.geometry.dispose();
			if (object.material) {
				const materials = Array.isArray(object.material) ? object.material : [object.material];
				materials.forEach((material) => {
					if (material instanceof THREE.Material) {
						material.dispose();
					}
				});
			}
		}
	});

	// Disposing of controls, renderer, and composer
	if (controls) controls.dispose();
	if (renderer) {
		renderer.forceContextLoss();
		renderer.dispose();
	}
	if (composer) composer.dispose();

	// Clearing the scene
	scene.clear();
};
