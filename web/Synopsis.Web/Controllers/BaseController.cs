using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Synopsis.Library.CustomMVC;

namespace Synopsis.Web.Controllers
{
	public class BaseController : Controller
	{
		public ActionResult JsonContract(object model)
		{
			return new JsonDataContractActionResult(model);
		}

		public ActionResult JsonContractError(object model)
		{
			return new JsonDataContractErrorActionResult(model);
		}
	}
}
