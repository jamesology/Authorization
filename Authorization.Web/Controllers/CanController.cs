using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Authorization.Web.Cache;

namespace Authorization.Web.Controllers
{
	[RoutePrefix("can")]
	public class CanController : ApiController
	{
		private IUserCache _users;

		public CanController(IUserCache users)
		{
			_users = users;
		}

		[Route("{uuser}/{aaction}")]
		[HttpGet]
		public HttpResponseMessage Get(string uuser, string aaction)
		{
			string responseMessage;
			HttpStatusCode responseStatus;
			var users = _users.GetUsers();

			var user = users.FirstOrDefault(x => x.Key == uuser).Value;

			if (user == null)
			{
				responseMessage = String.Format("User {0} cannot {1}.", uuser, aaction);
				responseStatus = HttpStatusCode.Unauthorized;
			}
			else
			{
				var canAction = user.Actions.Any(x => x == aaction);

				if (canAction)
				{
					responseMessage = String.Format("User {0} can {1}.", uuser, aaction);
					responseStatus = HttpStatusCode.OK;
				}
				else
				{
					responseMessage = String.Format("User {0} cannot {1}.", uuser, aaction);
					responseStatus = HttpStatusCode.Unauthorized;
				}
			}

			var result = new HttpResponseMessage
			{
				Content = new StringContent(responseMessage),
				StatusCode = responseStatus
			};

			return result;
		}
	}
}
