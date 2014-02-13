using log4net;

namespace Authorization.Cli.Executors
{
	public interface IExecutor
	{
		void Execute(string[] args, ILog log);
	}
}
