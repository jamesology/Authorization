using System;
using System.Linq;
using log4net;

namespace Authorization.Cli.Executors.Impl
{
	public class RoleExecutor : IExecutor
	{
		public void Execute(string[] args, ILog log)
		{
			var roleName = args.FirstOrDefault();
			var actions = args.Skip(1);

			if(String.IsNullOrWhiteSpace(roleName))
			{
				throw new ArgumentException("Invalid Role name.", "args");
			}

			log.DebugFormat("Role Name: {0}", roleName);
		}
	}
}