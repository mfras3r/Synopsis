using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Synopsis.Web.Models
{
	[DataContract]
	public class PageSynopsis
	{
		public PageSynopsis()
		{
			TopTenWords = new List<WordCount>();
			ImageUrls = new List<string>();
		}

		[DataMember(Name="words")]
		public List<WordCount> TopTenWords { get; set; }

		[DataMember(Name="images")]
		public List<string> ImageUrls { get; set; }
	}
}