using System.Runtime.Serialization;
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