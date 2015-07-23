using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Synopsis.Library;
using Synopsis.Library.SynopsisRequest;

namespace Synopsis.Web.Models
{
	[DataContract]
	public class WordCount : IWordCount
	{
		[DataMember(Name = "word")]
		public string Word { get; set; }

		[DataMember(Name = "count")]
		public int Count { get; set; }
	}
}