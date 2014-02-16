using System.Collections.Generic;
using Authorization.Cli.Executors;
using Authorization.Cli.Executors.Impl;
using Authorization.Core.Repositories;
using log4net;
using NUnit.Framework;
using Rhino.Mocks;

namespace Authorization.Cli.Tests.Executors
{
	[TestFixture]
	public class ExecutorTests
	{
		private class ExecutorTestHarness : Executor
		{
			public ExecutorTestHarness(IExecutor mockAction) : base(MockRepository.GenerateStub<IRoleRepository>())
			{
				Executors = new Dictionary<string, IExecutor>
				{
					{"validaction", mockAction}
				};
			}
		}
		private static readonly ILog Log = LogManager.GetLogger(typeof(ExecutorTests));

		[Test]
		public void Execute_InvalidAction_ThrowsArgumentException()
		{
			var mockAction = MockRepository.GenerateStub<IExecutor>();

			var executor = new ExecutorTestHarness(mockAction);

			var args = new[]
			{
				"InvalidCommand"
			};

			Assert.That(() => executor.Execute(args, Log), Throws.ArgumentException);
		}

		[Test]
		public void Execute_ValidAction_ExecutesAction()
		{
			var mockAction = MockRepository.GenerateStub<IExecutor>();
			mockAction.Expect(x => x.Execute(null, null))
				.IgnoreArguments()
				.Repeat.Once();

			var executor = new ExecutorTestHarness(mockAction);

			var args = new[]
			{
				"ValidAction"
			};

			executor.Execute(args, Log);

			mockAction.VerifyAllExpectations();
		}
	}
}
