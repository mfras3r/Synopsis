using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Synopsis.Library.SynopsisRequest.Processors
{
	public class HtmlSynopsisProcessor<TWordCount> : ISynopsisProcessor<TWordCount>
		where TWordCount : class, IWordCount, new()
	{
		public HtmlSynopsisProcessor(string content, Uri uri)
		{
			Uri = uri;
			Document = new HtmlDocument();
			Document.LoadHtml(content);
		}

		public Uri Uri { get; set; }
		public HtmlDocument Document { get; set; }

		public List<string> GetImageUrls()
		{
			return Document.DocumentNode
				.Descendants("img")
				.Select(GetRelativeUri)
				.Distinct()
				.ToList();
		}

		private string GetRelativeUri(HtmlNode tag)
		{
			var relativeUri = tag.Attributes["src"].Value;
			if (relativeUri.StartsWith("http") || relativeUri.StartsWith("//"))
			{
				return relativeUri;
			}
			return new Uri(Uri, relativeUri).AbsoluteUri;
		}

		public IEnumerable<TWordCount> GetWordCount()
		{
			var counter = new CountHtmlWords<TWordCount>();
			var bodyNode = Document.DocumentNode
				.Element("html")
				.Element("body");

			return counter.Count(bodyNode);
		}
	}
}
