using Authorization.Cli.Executors.Impl;
using log4net;
using NUnit.Framework;

namespace Authorization.Cli.Tests.Executors
{
	[TestFixture]
	public class RoleExecutorTests
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ExecutorTests));

		[Test]
		public void Execute_EmptyArray_ThrowsArgumentException()
		{
			var args = new string[0];

			var executor = new RoleExecutor();

			Assert.That(() => executor.Execute(args, Log), Throws.ArgumentException);
		}
	}
}