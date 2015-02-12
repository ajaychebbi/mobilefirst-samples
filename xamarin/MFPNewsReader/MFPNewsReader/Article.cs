using System;

namespace MFPNewsReader
{
	public class Article
	{

		public string ArticleText { get; private set;}
		public string ArticleTitle { get; private set; }

		public Article(string title, string item)
		{
			ArticleTitle = title;
			ArticleText = item;
		}
	}
}

