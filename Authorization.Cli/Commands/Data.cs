using System;
using System.Linq;
using System.Text;
using Authorization.Data;

namespace Authorization.Cli.Commands
{
	public static class Data
	{
		public static string AddUser(string userName)
		{
			var repository = new UserRepository(Library.ConnectionString);

			var user = repository.AddUser(userName);

			var result = String.Format("{0} created. Unique token {1}.", user.Name, user.Token);
			return result;
		}

		public static string GetUser(string userName = "")
		{
			var repository = new UserRepository(Library.ConnectionString);

			var users = repository.GetUsers();
			if (String.IsNullOrWhiteSpace(userName) == false)
			{
				users = users.Where(x => x.Name == userName);
			}

			var result = new StringBuilder();
			foreach (var user in users)
			{
				result.AppendFormat("\t{0}: {1}\n", user.Name, user.Token);
			}

			return result.ToString();
		}
	}
}
