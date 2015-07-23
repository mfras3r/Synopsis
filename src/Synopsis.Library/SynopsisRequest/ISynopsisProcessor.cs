using System.Collections.Generic;

namespace Synopsis.Library.SynopsisRequest
{
	public interface ISynopsisProcessor<TWordCount> where TWordCount : class, IWordCount, new()
	{
		List<string> GetImageUrls();
		IEnumerable<TWordCount> GetWordCount();
	}
}