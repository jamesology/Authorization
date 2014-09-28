using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Data
{
	public class UserRepository
	{
		private readonly UserContext _userContext;
		public UserRepository(string connectionString)
		{
			_userContext = new UserContext(connectionString);
		}

		public User AddUser(string name)
		{
			var user =  new User
			{
				Token = Guid.NewGuid().ToString(),
				Name = name
			};

			User result;
			if (_userContext.Users.Any(x => x.Name == name))
			{
				result = null;
			}
			else
			{
				_userContext.Users.Add(user);
				_userContext.SaveChanges();
				result = user;
			}

			return result;
		}

		public IQueryable<User> GetUsers()
		{
			return _userContext.Users;
		}

		public User DeactivateUser(User user)
		{
			throw new NotImplementedException();
		}
	}
}
