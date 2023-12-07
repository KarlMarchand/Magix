import React from "react";
import { useDeckManager } from "@context/deck_manager_context/DeckManagerContext";
import "./filtersbar.scss";
import { useGameOptions } from "@context/GameOptionsProvider";

const FiltersBar: React.FC<React.HTMLProps<HTMLDivElement>> = ({ className, ...props }) => {
	const { setFilter, activeFilter } = useDeckManager();
	const { factionsImages, availableFactionsList } = useGameOptions();

	return (
		<div className={`d-flex justify-content-center align-items-center ${className ? className : ""}`} {...props}>
			<div className={"d-flex justify-content-center filter-bar py-2 px-5"}>
				{availableFactionsList.map((faction) => {
					return (
						<img
							src={factionsImages[faction.name.toLowerCase()].symbol}
							alt={faction.name.toLowerCase() + "-symbol"}
							onClick={() => setFilter(faction === activeFilter ? null : faction)}
							className={`filter mx-4 ${activeFilter === faction ? "selected" : ""}`}
							key={faction.id}
						/>
					);
				})}
			</div>
		</div>
	);
};

export default FiltersBar;
