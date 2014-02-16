using System;
using System.Linq;
using Authorization.Core.Repositories;
using log4net;

namespace Authorization.Cli.Executors.Impl
{
	public class UserExecutor : IExecutor
	{
		private readonly IUserRepository _repository;

		public UserExecutor(IUserRepository repository)
		{
			_repository = repository;
		}

		public void Execute(string[] args, ILog log)
		{
			var userName = args.FirstOrDefault();
			var roles = args.Skip(1);

			if (String.IsNullOrWhiteSpace(userName))
			{
				throw new ArgumentException("Invalid User name.", "args");
			}

			log.DebugFormat("User Name: {0}", userName);
			foreach (var role in roles)
			{
				log.DebugFormat("\tRole: {0}", role);
			}

			var user = _repository.Get(userName).FirstOrDefault(x => x.Name == userName) ?? new User {Name = userName};

			if (roles.Any() && (roles.FirstOrDefault().ToLower() == "delete"))
			{
				_repository.Delete(user);
			}
			else
			{
				user.AddRoles(roles);
				_repository.Save(user);
			}
		}
	}
}
