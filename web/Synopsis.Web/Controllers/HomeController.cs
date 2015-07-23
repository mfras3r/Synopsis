using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Synopsis.Library;
using Synopsis.Library.SynopsisRequest;
using Synopsis.Library.SynopsisRequest.Exceptions;
using Synopsis.Web.Models;

namespace Synopsis.Web.Controllers
{
	public class HomeController : BaseController
	{
		//
		// GET: /Home/

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Request(string url)
		{
			try
			{
				if (!url.StartsWith("http") && !url.StartsWith("//"))
				{
					url = "http://" + url;
				}
				var uri = new Uri(url);
				var request = new DocumentRequest<WordCount>(uri);

				request.GetDocument();

				var model = new PageSynopsis()
				{
					ImageUrls = request.GetImageUrls(),
					TopTenWords = request.GetWordCount()
				};
				return JsonContract(model);
			}
			catch (UriFormatException)
			{
				return GetUriFormatError(url);
			}
			catch (UnknownContentTypeException ucte)
			{
				return GetUnknownContentTypeError(url, ucte.ContentType);
			}
			catch (WebException we)
			{
				if (we.Message.StartsWith("The remote name could not be resolved"))
				{
					return GetHostNotFoundError(url);
				}
				
				return GetUnkonwnError(url);
			}
			catch (Exception)
			{
				return GetUnkonwnError(url);
			}
		}

		#region Error Handlers
		private ActionResult GetUriFormatError(string url)
		{
			var error = new PageSynopsisError
			{
				Message = String.Format("The URL that you requested, '{0}', doesn't appear to be a valid URL.", url)
			};

			return JsonContractError(error);
		}

		private ActionResult GetUnknownContentTypeError(string url, string contentType)
		{
			var error = new PageSynopsisError
			{
				Message = String.Format("Synopsis doesn't know how to handle the content type '{0}'", contentType)
			};

			return JsonContractError(error);
		}

		private ActionResult GetHostNotFoundError(string url)
		{
			var error = new PageSynopsisError()
			{
				Message = String.Format("Could not find the requested URL: '{0}', host not found.", url)
			};
			return JsonContractError(error);
		}

		private ActionResult GetUnkonwnError(string url)
		{
			var error = new PageSynopsisError()
			{
				Message = String.Format("An unknown error occurred while requesting the web page at '{0}'.", url)
			};

			return JsonContractError(error);
		}
		#endregion Error Handlers
	}
}
