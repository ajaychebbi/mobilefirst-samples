using System;

namespace MFPNewsReader
{
	public class Article
	{
		//2 things in the article entry
		public string ArticleText { get; private set;}
		public string ArticleTitle { get; private set; }

		public Article(string title, string item)
		{
			ArticleTitle = title;
			ArticleText = item;
		}
	}
}

