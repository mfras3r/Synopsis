using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;

namespace Synopsis.Library.CustomMVC
{
	public class JsonDataContractActionResult : ActionResult
	{
		public JsonDataContractActionResult(object model)
		{
			Model = model;
		}

		public object Model { get; set; }

		public override void ExecuteResult(ControllerContext context)
		{
			var serializer = new DataContractJsonSerializer(Model.GetType());
			string output;

			using (var ms = new MemoryStream())
			{
				serializer.WriteObject(ms, Model);
				output = Encoding.Default.GetString(ms.ToArray());
			}

			context.HttpContext.Response.ContentType = "application/json";
			context.HttpContext.Response.Write(output);
		}
	}
}