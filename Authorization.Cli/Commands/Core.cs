using System;
using Authorization.Data;

namespace Authorization.Cli.Commands
{
	public static class Core
	{
		public static string AddUser(string userName)
		{
			var repository = new UserRepository(String.Empty);

			var user = repository.AddUser(userName);

			var result = String.Format("{0} created. Unique token {1}.", user.Name, user.Token);
			return result;
		}
	}
}
