using System;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace Authorization.Cli.Executors.Impl
{
	public class Executor : IExecutor
	{
		private readonly IDictionary<string, IExecutor> _executors;

		public Executor()
		{
		}

		public Executor(IDictionary<string, IExecutor> executors)
		{
			_executors = executors;
		}

		public void Execute(string[] args, ILog log)
		{
			var command = args.FirstOrDefault();
			if (String.IsNullOrWhiteSpace(command) == false)
			{
				IExecutor executor;
				if (_executors.TryGetValue(command, out executor))
				{
					log.InfoFormat("Executing: {0}", command);
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