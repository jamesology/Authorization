using System.Collections.Generic;
using Authorization.Web.Objects;

namespace Authorization.Web.Cache
{
	public interface IUserCache
	{
		IDictionary<string, User> GetUsers();
	}
}
