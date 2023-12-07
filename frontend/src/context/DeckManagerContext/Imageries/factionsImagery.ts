import separatistSymbol from "@assets/factions/separatist-symbol.png";
import republicSymbol from "@assets/factions/republic-symbol.png";
import criminalSymbol from "@assets/factions/criminal-symbol.png";
import empireSymbol from "@assets/factions/empire-symbol.png";
import rebelSymbol from "@assets/factions/rebel-symbol.png";
import separatistImage from "@assets/factions/separatist.webp";
import republicImage from "@assets/factions/republic.webp";
import criminalImage from "@assets/factions/criminal.webp";
import empireImage from "@assets/factions/empire.webp";
import rebelImage from "@assets/factions/rebel.webp";

const factionsImagery: Record<string, { symbol: string; image: string }> = {
	criminal: {
		symbol: criminalSymbol,
		image: criminalImage,
	},
	empire: {
		symbol: empireSymbol,
		image: empireImage,
	},
	rebel: {
		symbol: rebelSymbol,
		image: rebelImage,
	},
	republic: {
		symbol: republicSymbol,
		image: republicImage,
	},
	separatist: {
		symbol: separatistSymbol,
		image: separatistImage,
	},
};

export default factionsImagery;
