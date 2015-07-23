using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using Synopsis.Library.SynopsisRequest.Exceptions;
using Synopsis.Library.SynopsisRequest.Processors;

namespace Synopsis.Library.SynopsisRequest
{
	public class DocumentRequest<TWordCount> 
		where TWordCount : class, IWordCount, new()
	{
		public DocumentRequest(Uri uri)
		{
			Uri = uri;
		}

		public Uri Uri { get; set; }
		public HtmlDocument Document { get; set; }
		public ISynopsisProcessor<TWordCount> Processor { get; set; }

		public void GetDocument()
		{
			var request = WebRequest.Create(Uri);
			var response = (HttpWebResponse)request.GetResponse();

			var responseStream = response.GetResponseStream();
			var sr = new StreamReader(responseStream);
			var content = sr.ReadToEnd();

			var contentType = response.ContentType.Split(';').First();
			if (!Processors.ContainsKey(contentType))
			{
				throw new UnknownContentTypeException(contentType);
			}
			Processor = Processors[contentType](content, Uri);
		}

		public List<string> GetImageUrls()
		{
			return Processor.GetImageUrls();
		}

		public List<TWordCount> GetWordCount(int count = 10)
		{
			return Processor.GetWordCount()
				.OrderByDescending(c => c.Count)
				.Take(count)
				.ToList();
		}

		private static readonly Dictionary<string, Func<string, Uri, ISynopsisProcessor<TWordCount>>>  Processors =
			new Dictionary<string, Func<string, Uri, ISynopsisProcessor<TWordCount>>>
			{
				{"text/html", (content, uri) => new HtmlSynopsisProcessor<TWordCount>(content, uri)}
			};
	}
}
