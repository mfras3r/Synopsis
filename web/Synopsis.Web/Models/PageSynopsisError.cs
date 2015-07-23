using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

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