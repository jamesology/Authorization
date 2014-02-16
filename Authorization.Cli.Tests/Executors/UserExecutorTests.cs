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
	public class UserExecutorTests
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(UserExecutorTests));

		[Test]
		public void Execute_EmptyArray_ThrowsArgumentException()
		{
			var repository = MockRepository.GenerateStub<IRoleRepository>();

			var args = new string[0];

			var executor = new RoleExecutor(repository);

			Assert.That(() => executor.Execute(args, Log), Throws.ArgumentException);
		}

		[Test]
		public void Execute_UserNameNoRoles_CallsRepository()
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

			repository.VerifyAllExpectations();
		}

		[Test]
		public void Execute_UserNameNoRoles_SavesRoleName()
		{
			var repository = MockRepository.GenerateMock<IUserRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(Enumerable.Empty<User>())
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<User>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewUser" };
			var executor = new UserExecutor(repository);

			executor.Execute(args, Log);

			var repositoryArgs = repository.GetArgumentsForCallsMadeOn(x => x.Save(Arg<User>.Is.TypeOf))[0];

			Assert.That(repositoryArgs, Has.Length.EqualTo(1));
			Assert.That(repositoryArgs[0], Is.TypeOf<User>());

			var user = repositoryArgs[0] as User;

			Assert.That(user.Name, Is.EqualTo("NewUser"));
		}

		[Test]
		public void Execute_UserNameNoRoles_SavesUserWithNoRoles()
		{
			var repository = MockRepository.GenerateMock<IUserRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(Enumerable.Empty<User>())
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<User>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewUser" };
			var executor = new UserExecutor(repository);

			executor.Execute(args, Log);

			var repositoryArgs = repository.GetArgumentsForCallsMadeOn(x => x.Save(Arg<User>.Is.TypeOf))[0];

			Assert.That(repositoryArgs, Has.Length.EqualTo(1));
			Assert.That(repositoryArgs[0], Is.TypeOf<User>());

			var user = repositoryArgs[0] as User;

			Assert.That(user.Roles, Is.Empty);
		}

		[Test]
		public void Execute_NewUserNameHasRoles_CallsRepository()
		{
			var repository = MockRepository.GenerateMock<IUserRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(Enumerable.Empty<User>())
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<User>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewUser", "Role1", "Role2" };
			var executor = new UserExecutor(repository);

			executor.Execute(args, Log);

			repository.VerifyAllExpectations();
		}

		[Test]
		public void Execute_NewUserNameHasRoles_SavesUserName()
		{
			var repository = MockRepository.GenerateMock<IUserRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(Enumerable.Empty<User>())
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<User>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewUser", "Role1", "Role2" };
			var executor = new UserExecutor(repository);

			executor.Execute(args, Log);

			var repositoryArgs = repository.GetArgumentsForCallsMadeOn(x => x.Save(Arg<User>.Is.TypeOf))[0];

			Assert.That(repositoryArgs, Has.Length.EqualTo(1));
			Assert.That(repositoryArgs[0], Is.TypeOf<User>());

			var user = repositoryArgs[0] as User;

			Assert.That(user.Name, Is.EqualTo("NewUser"));
		}

		[Test]
		public void Execute_NewUserNameHasRoles_SavesUserWithRoles()
		{
			var repository = MockRepository.GenerateMock<IUserRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(Enumerable.Empty<User>())
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<User>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewUser", "Role1", "Role2" };
			var executor = new UserExecutor(repository);

			executor.Execute(args, Log);

			var repositoryArgs = repository.GetArgumentsForCallsMadeOn(x => x.Save(Arg<User>.Is.TypeOf))[0];

			Assert.That(repositoryArgs, Has.Length.EqualTo(1));
			Assert.That(repositoryArgs[0], Is.TypeOf<User>());

			var user = repositoryArgs[0] as User;

			Assert.That(user.Roles, Has.Count.EqualTo(2));
		}

		[Test]
		public void Execute_UserNameHasAdditionalRoles_CallsRepository()
		{
			var repository = MockRepository.GenerateMock<IUserRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(new[] { new User { Name = "NewUser", Roles = new List<string> { "Role1", "Role2" } } })
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<User>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewUser", "Role3", "Role4" };
			var executor = new UserExecutor(repository);

			executor.Execute(args, Log);

			repository.VerifyAllExpectations();
		}

		[Test]
		public void Execute_UserNameHasAdditionalRoles_SavesUserName()
		{
			var repository = MockRepository.GenerateMock<IUserRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(new[] { new User { Name = "NewUser", Roles = new List<string> { "Role1", "Role2" } } })
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<User>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewUser", "Role3", "Role4" };
			var executor = new UserExecutor(repository);

			executor.Execute(args, Log);

			var repositoryArgs = repository.GetArgumentsForCallsMadeOn(x => x.Save(Arg<User>.Is.TypeOf))[0];

			Assert.That(repositoryArgs, Has.Length.EqualTo(1));
			Assert.That(repositoryArgs[0], Is.TypeOf<User>());

			var user = repositoryArgs[0] as User;

			Assert.That(user.Name, Is.EqualTo("NewUser"));
		}

		[Test]
		public void Execute_UserNameHasAdditionalRoles_SavesUserWithAllRoles()
		{
			var repository = MockRepository.GenerateMock<IUserRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(new[] { new User { Name = "NewUser", Roles = new List<string> { "Role1", "Role2" } } })
				.Repeat.Once();
			repository.Expect(x => x.Save(Arg<User>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewUser", "Role3", "Role4" };
			var executor = new UserExecutor(repository);

			executor.Execute(args, Log);

			var repositoryArgs = repository.GetArgumentsForCallsMadeOn(x => x.Save(Arg<User>.Is.TypeOf))[0];

			Assert.That(repositoryArgs, Has.Length.EqualTo(1));
			Assert.That(repositoryArgs[0], Is.TypeOf<User>());

			var user = repositoryArgs[0] as User;

			repository.VerifyAllExpectations();
			Assert.That(user.Roles, Has.Count.EqualTo(4));
		}

		[Test]
		public void Execute_UserDelete_CallsRepository()
		{
			var repository = MockRepository.GenerateMock<IUserRepository>();
			repository.Expect(x => x.Get(Arg<string>.Is.TypeOf))
				.Return(new[] { new User { Name = "NewUser", Roles = new List<string> { "Role1", "Role2" } } })
				.Repeat.Once();
			repository.Expect(x => x.Delete(Arg<User>.Is.TypeOf))
				.Repeat.Once();

			var args = new[] { "NewUser", "Delete" };
			var executor = new UserExecutor(repository);

			executor.Execute(args, Log);

			repository.VerifyAllExpectations();
		}
	}
}