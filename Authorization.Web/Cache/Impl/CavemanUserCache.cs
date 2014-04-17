using System.Collections.Generic;
using Authorization.Web.Objects;

namespace Authorization.Web.Cache.Impl
{
	public class CavemanUserCache : IUserCache
	{
		private static readonly IDictionary<string, User> _users = new Dictionary<string, User>();

		public IDictionary<string, User> GetUsers()
		{
			return _users;
		} 
	}
}