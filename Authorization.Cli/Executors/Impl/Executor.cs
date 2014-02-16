using System;
using System.Collections.Generic;
using System.Linq;
using Authorization.Core.Repositories;
using log4net;

namespace Authorization.Cli.Executors.Impl
{
	public class Executor : IExecutor
	{
		protected IDictionary<string, IExecutor> Executors;

		public Executor(IRoleRepository roleRepository, IUserRepository userRepository)
		{
			Executors = new Dictionary<string, IExecutor>
			{
				{"role", new RoleExecutor(roleRepository)},
				{"user", new UserExecutor(userRepository)}
			};
		}

		public void Execute(string[] args, ILog log)
		{
			var command = args.FirstOrDefault();
			if (String.IsNullOrWhiteSpace(command) == false)
			{
				IExecutor executor;
				if (Executors.TryGetValue(command.ToLower(), out executor))
				{
					log.DebugFormat("Executing: {0}", command);
					executor.Execute(args.Skip(1).ToArray(), log);
				}
				else
				{
					throw new ArgumentException(String.Format("Invalid argument: {0}.", command), "args");
				}
			}
		}
	}
}