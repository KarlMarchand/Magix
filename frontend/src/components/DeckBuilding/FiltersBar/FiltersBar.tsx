import React, { useEffect, useState } from "react";
import { useDeckManager } from "@context/DeckManagerContext/DeckManagerContext";
import Faction from "@customTypes/Faction";
import "./filtersbar.scss";

const FiltersBar: React.FC<React.HTMLProps<HTMLDivElement>> = ({ className, ...props }) => {
	const { availableFactionsList, filterCardsByFaction, factionsImages } = useDeckManager();
	const [activeFilter, setActiveFilter] = useState<Faction | null>(null);

	useEffect(() => {
		filterCardsByFaction(activeFilter);
	}, [activeFilter]);

	return (
		<div className={`d-flex justify-content-center align-items-center ${className ? className : ""}`} {...props}>
			<div className={"d-flex justify-content-center filter-bar py-2 px-5"}>
				{availableFactionsList.map((faction) => {
					return (
						<img
							src={factionsImages[faction.name.toLowerCase()].symbol}
							alt={faction.name.toLowerCase() + "-symbol"}
							onClick={() => setActiveFilter(faction !== activeFilter ? faction : null)}
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
