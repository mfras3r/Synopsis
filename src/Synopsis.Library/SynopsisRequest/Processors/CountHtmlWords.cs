using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Synopsis.Library.SynopsisRequest.Processors
{
	public class CountHtmlWords<WordCount> 
		where WordCount: class, IWordCount, new()
	{
		public readonly Regex WordPattern = new Regex(@"[A-Za-z']+");

		public IEnumerable<WordCount> Count(HtmlNode node)
		{
			var wc = new Dictionary<string, int>();
			ConvertContentTo(node, wc);
			return wc
				.Select(foo => new WordCount(){Word = foo.Key, Count = foo.Value});
		}

		private void ConvertContentTo(HtmlNode node, Dictionary<string, int> wc)
		{
			foreach (HtmlNode subnode in node.ChildNodes)
			{
				ConvertTo(subnode, wc);
			}
		}

		private void ConvertTo(HtmlNode node, Dictionary<string, int> wc)
		{
			string html;
			switch (node.NodeType)
			{
				case HtmlNodeType.Comment:
					// don't output comments
					break;

				case HtmlNodeType.Document:
					ConvertContentTo(node, wc);
					break;

				case HtmlNodeType.Text:
					// script and style must not be output
					string parentName = node.ParentNode.Name;
					if ((parentName == "script") || (parentName == "style"))
						break;

					// get text
					html = ((HtmlTextNode)node).Text;

					// is it in fact a special closing node output as text?
					if (HtmlNode.IsOverlappedClosingElement(html))
						break;

					// check the text is meaningful and not a bunch of whitespaces
					if (html.Trim().Length > 0)
					{
						CountWords(HtmlEntity.DeEntitize(html), wc);
					}
					break;

				case HtmlNodeType.Element:
					if (node.HasChildNodes)
					{
						ConvertContentTo(node, wc);
					}
					break;
			}
		}

		private void CountWords(string text, Dictionary<string, int> wc)
		{
			foreach (Match match in WordPattern.Matches(text))
			{
				if (!wc.ContainsKey(match.Value))
				{
					wc.Add(match.Value, 0);
				}
				wc[match.Value]++;
			}
		}
	}
}
