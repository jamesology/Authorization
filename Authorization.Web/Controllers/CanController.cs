using System;
using System.Net.Http;
using System.Web.Http;

namespace Authorization.Web.Controllers
{
	[RoutePrefix("can")]
	public class CanController : ApiController
	{
		[Route("{uuser}/{aaction}")]
		[HttpGet]
		public HttpResponseMessage Get(string uuser, string aaction)
		{
			var responseMessage = String.Format("User {0} can {1}.", uuser, aaction);

			var result = new HttpResponseMessage
			{
				Content = new StringContent(responseMessage)
			};

			return result;
		}
	}
}
