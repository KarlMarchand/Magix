import React, { useEffect, useState } from "react";
import RequestHandler from "@utils/RequestHandler";
import NewsEntry from "@customTypes/NewsEntry";
import "./news.scss";

const News: React.FC<React.HTMLProps<HTMLDivElement>> = ({ className, ...restProps }) => {
	const [news, setNews] = useState<any[]>();

	useEffect(() => {
		RequestHandler.get<NewsEntry[]>("news").then((response) => {
			if (response.success && response.data) {
				const sortedNews = response.data.sort(
					(a, b) => new Date(b.date).getTime() - new Date(a.date).getTime()
				);
				setNews(sortedNews);
			}
		});
	}, []);

	return (
		<div {...restProps} className={`news-container w-100 h-100 p-3 ${className ? className : ""}`}>
			{news?.map((message: NewsEntry, index) => (
				<div key={index} className="news-entry">
					<h2>{message.title}</h2>
					<p>{message.text}</p>
					<p>
						<small>Posted on {new Date(message.date).toLocaleDateString()}</small>
					</p>
					<hr className="my-5" />
				</div>
			))}
		</div>
	);
};

export default News;
