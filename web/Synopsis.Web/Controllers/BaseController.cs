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
