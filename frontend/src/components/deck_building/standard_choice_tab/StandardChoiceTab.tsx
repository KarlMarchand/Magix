import React, { useState } from "react";
import "./standardChoiceTab.scss";

export interface choiceObject {
	id: number | string;
	name: string;
	description: string;
	image: string;
}

interface StandardChoiceTabProps extends React.HTMLAttributes<HTMLDivElement> {
	maxHeightInVh?: number;
	cardSize?: "lg" | "md" | "sm";
	content: choiceObject[];
	initialChoice: choiceObject;
	onSelection: (id: number | string) => void;
}

const StandardChoiceTab: React.FC<StandardChoiceTabProps> = ({
	content,
	initialChoice,
	onSelection,
	maxHeightInVh = 64,
	cardSize = "sm",
	...htmlProps
}) => {
	const [activeChoice, setActiveChoice] = useState<number | string>(initialChoice?.id ?? content[0].id);

	const handleSelection = (id: string | number) => {
		setActiveChoice(id);
		onSelection(id);
	};

	const cardSizing = {
		lg: {
			style: {
				width: "20rem",
				height: "20rem",
			},
			col: 6,
		},
		md: {
			style: {
				width: "15rem",
				height: "15rem",
			},
			col: 4,
		},
		sm: {
			style: {
				width: "10rem",
				height: "10rem",
			},
			col: 3,
		},
	}[cardSize];

	return (
		<div {...htmlProps} className="container p-2">
			<div style={{ maxHeight: `${maxHeightInVh}vh` }} className="overflow-y-scroll row">
				{content.map((obj) => {
					return (
						<div className={`col-${cardSizing.col} mb-4`} key={obj.id}>
							<div
								onClick={() => handleSelection(obj.id)}
								className={`${
									activeChoice === obj.id ? "highlight" : ""
								} choice clickable h-100 d-flex flex-column align-items-center px-4`}
							>
								<h2 className="mt-3">{obj.name}</h2>
								<img src={obj.image} className="my-3" style={cardSizing.style} />
								<p>{obj.description}</p>
							</div>
						</div>
					);
				})}
			</div>
		</div>
	);
};

export default StandardChoiceTab;
