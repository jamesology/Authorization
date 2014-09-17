using System;

namespace Authorization.Data
{
	public class UserRepository
	{
		private readonly string _connectionString;

		public UserRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public User AddUser(string name)
		{
			var result =  new User
			{
				Token = Guid.NewGuid().ToString(),
				Name = name
			};

			return result;
		}
	}
}
