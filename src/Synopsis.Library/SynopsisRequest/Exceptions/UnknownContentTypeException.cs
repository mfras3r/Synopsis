using System;

namespace Synopsis.Library.SynopsisRequest.Exceptions
{
	public class UnknownContentTypeException : ApplicationException
	{
		public UnknownContentTypeException(string contentType)
			: base(String.Format("The requested URL contains content of an unknown type, '{0}'.", contentType))
		{
			ContentType = contentType;
		}

		public string ContentType { get; private set; }
	}
}
