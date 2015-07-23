using System.Web.Mvc;

namespace Synopsis.Library.CustomMVC
{
	public class JsonDataContractErrorActionResult : JsonDataContractActionResult
	{
		public JsonDataContractErrorActionResult(object model) : base(model)
		{
			// no op
		}

		public override void ExecuteResult(ControllerContext context)
		{
			base.ExecuteResult(context);
			context.HttpContext.Response.StatusCode = 500;
		}
	}
}