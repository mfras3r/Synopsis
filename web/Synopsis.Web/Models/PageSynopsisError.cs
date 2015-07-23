using System.Runtime.Serialization;

namespace Synopsis.Web.Models
{
	
	[DataContract]
	public class PageSynopsisError
	{
		public PageSynopsisError()
		{
			// no op
		}

		public PageSynopsisError(string message)
		{
			Message = message;
		}

		[DataMember(Name="message")]
		public string Message { get; set; }
	}
}