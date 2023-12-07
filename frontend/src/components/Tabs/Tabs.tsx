import React, { useState, ReactElement } from "react";
import "./tab.scss";

interface TabProps {
	name: string;
	children: React.ReactNode;
}

const Tab: React.FC<TabProps> = ({ children }) => {
	return <>{children}</>;
};

interface TabsProps {
	children: ReactElement[];
}

const Tabs: React.FC<TabsProps> = ({ children }) => {
	const [activeTab, setActiveTab] = useState(0);

	const tabs = React.Children.toArray(children).filter(
		(child) => React.isValidElement(child) && child.type === Tab
	) as ReactElement[];

	return (
		<div className="h-100 d-flex flex-column">
			<div className="d-flex w-100">
				{tabs.map((tab, index) => (
					<button
						key={index}
						className={`tabs p-2 flex-fill ${activeTab === index ? "active-tabs" : ""}`}
						onClick={() => setActiveTab(index)}
					>
						{tab.props.name}
					</button>
				))}
			</div>
			<div className="tab-content p-3 flex-fill overflow-hidden d-flex flex-column justify-centent-center">
				{tabs[activeTab]}
			</div>
		</div>
	);
};

export { Tabs, Tab };
