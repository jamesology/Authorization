using System.Collections.Generic;
using Authorization.Cli.Executors;
using Authorization.Cli.Executors.Impl;
using log4net;
using NUnit.Framework;
using Rhino.Mocks;

namespace Authorization.Cli.Tests
{
	[TestFixture]
	public class ExecutorTests
	{
		private static ILog _log = LogManager.GetLogger(typeof(ExecutorTests));

		[Test]
		public void Execute_InvalidAction_ThrowsArgumentException()
		{
			var mockAction = MockRepository.GenerateStub<IExecutor>();

			var executors = new Dictionary<string, IExecutor>
			{
				{"ValidAction", mockAction}
			};

			var executor = new Executor(executors);

			var args = new[]
			{
				"InvalidCommand"
			};

			Assert.That(() => executor.Execute(args, _log), Throws.ArgumentException);
		}

		[Test]
		public void Execute_ValidAction_ExecutesAction()
		{
			var mockAction = MockRepository.GenerateStub<IExecutor>();
			mockAction.Expect(x => x.Execute(null, null))
				.IgnoreArguments()
				.Repeat.Once();

			var executors = new Dictionary<string, IExecutor>
			{
				{"ValidAction", mockAction}
			};

			var executor = new Executor(executors);

			var args = new[]
			{
				"ValidAction"
			};

			executor.Execute(args, _log);

			mockAction.VerifyAllExpectations();
		}
	}
}
