using System.Collections.Generic;
using System.Linq;
using Authorization.Cli.Executors.Impl;
using Authorization.Core.Repositories;
using log4net;
using NUnit.Framework;
using Rhino.Mocks;

namespace Authorization.Cli.Tests.Executors
{
	[TestFixture]
	public class RoleExecutorTests
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ExecutorTests));

		[Test]
		public void Execute_EmptyArray_ThrowsArgumentException()
		{
			var repository = MockRepository.GenerateStub<IRoleRepository>();

			var args = new string[0];

			var executor = new RoleExecutor(repository);

			Assert.That(() => executor.Execute(args, Log), Throws.ArgumentException);
		}

		[Test]
		public void Execute_RoleNameNoActions_CallsRepository()
		{
			var repository = MockRepository.GenerateMock<IRoleRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(Enumerable.Empty<Role>())
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<Role>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] {"NewRole"};
			var executor = new RoleExecutor(repository);

			executor.Execute(args, Log);

			repository.VerifyAllExpectations();
		}

		[Test]
		public void Execute_RoleNameNoActions_SavesRoleName()
		{
			var repository = MockRepository.GenerateMock<IRoleRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(Enumerable.Empty<Role>())
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<Role>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewRole" };
			var executor = new RoleExecutor(repository);

			executor.Execute(args, Log);

			var repositoryArgs = repository.GetArgumentsForCallsMadeOn(x => x.Save(Arg<Role>.Is.TypeOf))[0];

			Assert.That(repositoryArgs, Has.Length.EqualTo(1));
			Assert.That(repositoryArgs[0], Is.TypeOf<Role>());

			var role = repositoryArgs[0] as Role;

			Assert.That(role.Name, Is.EqualTo("NewRole"));
		}

		[Test]
		public void Execute_RoleNameNoActions_SavesRoleWithNoActions()
		{
			var repository = MockRepository.GenerateMock<IRoleRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(Enumerable.Empty<Role>())
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<Role>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewRole" };
			var executor = new RoleExecutor(repository);

			executor.Execute(args, Log);

			var repositoryArgs = repository.GetArgumentsForCallsMadeOn(x => x.Save(Arg<Role>.Is.TypeOf))[0];

			Assert.That(repositoryArgs, Has.Length.EqualTo(1));
			Assert.That(repositoryArgs[0], Is.TypeOf<Role>());

			var role = repositoryArgs[0] as Role;

			Assert.That(role.Actions, Is.Empty);
		}

		[Test]
		public void Execute_NewRoleNameHasActions_CallsRepository()
		{
			var repository = MockRepository.GenerateMock<IRoleRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(Enumerable.Empty<Role>())
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<Role>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewRole", "Action1", "Action2" };
			var executor = new RoleExecutor(repository);

			executor.Execute(args, Log);

			repository.VerifyAllExpectations();
		}

		[Test]
		public void Execute_NewRoleNameHasActions_SavesRoleName()
		{
			var repository = MockRepository.GenerateMock<IRoleRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(Enumerable.Empty<Role>())
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<Role>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewRole", "Action1", "Action2" };
			var executor = new RoleExecutor(repository);

			executor.Execute(args, Log);

			var repositoryArgs = repository.GetArgumentsForCallsMadeOn(x => x.Save(Arg<Role>.Is.TypeOf))[0];

			Assert.That(repositoryArgs, Has.Length.EqualTo(1));
			Assert.That(repositoryArgs[0], Is.TypeOf<Role>());

			var role = repositoryArgs[0] as Role;

			Assert.That(role.Name, Is.EqualTo("NewRole"));
		}

		[Test]
		public void Execute_NewRoleNameHasActions_SavesRoleWithActions()
		{
			var repository = MockRepository.GenerateMock<IRoleRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(Enumerable.Empty<Role>())
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<Role>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewRole", "Action1", "Action2" };
			var executor = new RoleExecutor(repository);

			executor.Execute(args, Log);

			var repositoryArgs = repository.GetArgumentsForCallsMadeOn(x => x.Save(Arg<Role>.Is.TypeOf))[0];

			Assert.That(repositoryArgs, Has.Length.EqualTo(1));
			Assert.That(repositoryArgs[0], Is.TypeOf<Role>());

			var role = repositoryArgs[0] as Role;

			Assert.That(role.Actions, Has.Count.EqualTo(2));
		}

		[Test]
		public void Execute_RoleNameHasAdditionalActions_CallsRepository()
		{
			var repository = MockRepository.GenerateMock<IRoleRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(new[] {new Role {Name = "NewRole", Actions = new List<string> {"Action1", "Action2"}}})
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<Role>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewRole", "Action3", "Action4" };
			var executor = new RoleExecutor(repository);

			executor.Execute(args, Log);

			repository.VerifyAllExpectations();
		}

		[Test]
		public void Execute_RoleNameHasAdditionalActions_SavesRoleName()
		{
			var repository = MockRepository.GenerateMock<IRoleRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(new[] { new Role { Name = "NewRole", Actions = new List<string> { "Action1", "Action2" } } })
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<Role>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewRole", "Action3", "Action4" };
			var executor = new RoleExecutor(repository);

			executor.Execute(args, Log);

			var repositoryArgs = repository.GetArgumentsForCallsMadeOn(x => x.Save(Arg<Role>.Is.TypeOf))[0];

			Assert.That(repositoryArgs, Has.Length.EqualTo(1));
			Assert.That(repositoryArgs[0], Is.TypeOf<Role>());

			var role = repositoryArgs[0] as Role;

			Assert.That(role.Name, Is.EqualTo("NewRole"));
		}

		[Test]
		public void Execute_RoleNameHasAdditionalActions_SavesRoleWithAllActions()
		{
			var repository = MockRepository.GenerateMock<IRoleRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(new[] { new Role { Name = "NewRole", Actions = new List<string> { "Action1", "Action2" } } })
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<Role>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewRole", "Action3", "Action4" };
			var executor = new RoleExecutor(repository);

			executor.Execute(args, Log);

			var repositoryArgs = repository.GetArgumentsForCallsMadeOn(x => x.Save(Arg<Role>.Is.TypeOf))[0];

			Assert.That(repositoryArgs, Has.Length.EqualTo(1));
			Assert.That(repositoryArgs[0], Is.TypeOf<Role>());

			var role = repositoryArgs[0] as Role;

			repository.VerifyAllExpectations();
			Assert.That(role.Actions, Has.Count.EqualTo(4));
		}

		[Test]
		public void Execute_RoleDelete_CallsRepository()
		{
			var repository = MockRepository.GenerateMock<IRoleRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(new[] { new Role { Name = "NewRole", Actions = new List<string> { "Action1", "Action2" } } })
				.Repeat.Once();
			repository.Expect(x => x.Delete(Arg<Role>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewRole", "Delete" };
			var executor = new RoleExecutor(repository);

			executor.Execute(args, Log);

			repository.VerifyAllExpectations();
		}
	}
}