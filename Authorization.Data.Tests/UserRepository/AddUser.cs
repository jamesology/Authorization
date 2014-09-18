using NUnit.Framework;

namespace Authorization.Data.Tests.UserRepository
{

	[TestFixture]
	public class AddUser
	{
		const string ConnectionString = "TestConnection";

		private Data.UserRepository _repository;

		[SetUp]
		public void SetUp()
		{
			_repository = new Data.UserRepository(ConnectionString);
		}

		[TearDown]
		public void TearDown()
		{
			var context = new UserContext(ConnectionString);

			foreach (var user in context.Users)
			{
				context.Users.Remove(user);
			}
			context.SaveChanges();
		}

		[Test]
		public void UniqueUserIsCreated()
		{
			const string userName = "TestUser";

			var user = _repository.AddUser(userName);

			Assert.That(user.Name, Is.EqualTo(userName));
			Assert.That(user.Token, Is.Not.Empty);
		}

		[Test]
		public void DuplicateUserReturnsNull()
		{
			const string userName = "TestUser";

			_repository.AddUser(userName);
			var user = _repository.AddUser(userName);

			Assert.That(user, Is.Null);
		}
	}
}
