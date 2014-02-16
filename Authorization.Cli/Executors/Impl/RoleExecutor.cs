using System;
using System.Linq;
using Authorization.Core.Repositories;
using log4net;

namespace Authorization.Cli.Executors.Impl
{
	public class RoleExecutor : IExecutor
	{
		private readonly IRoleRepository _repository;

		public RoleExecutor(IRoleRepository repository)
		{
			_repository = repository;
		}

		public void Execute(string[] args, ILog log)
		{
			var roleName = args.FirstOrDefault();
			var actions = args.Skip(1);

			if (String.IsNullOrWhiteSpace(roleName))
			{
				throw new ArgumentException("Invalid Role name.", "args");
			}

			log.DebugFormat("Role Name: {0}", roleName);
			foreach (var action in actions)
			{
				log.DebugFormat("\tAction: {0}", action);
			}

			var role = _repository.Get(roleName).FirstOrDefault(x => x.Name == roleName) ?? new Role {Name = roleName};

			if (actions.Any() && actions.FirstOrDefault().ToLower() == "delete")
			{
				_repository.Delete(role);
			}
			else
			{
				role.AddActions(actions);
				_repository.Save(role);
			}
		}
	}
}