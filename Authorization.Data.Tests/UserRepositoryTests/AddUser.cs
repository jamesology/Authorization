using NUnit.Framework;

namespace Authorization.Data.Tests.UserRepositoryTests
{

	[TestFixture]
	public class AddUser
	{
		private UserRepository _repository;

		[SetUp]
		public void SetUp()
		{
			const string connectionString = "This should not work";

			_repository = new UserRepository(connectionString);
		}

		[Test]
		public void UniqueUserIsCreated()
		{
			var userName = "TestUser";

			var user = _repository.AddUser(userName);

			Assert.That(user.Name, Is.EqualTo(userName));
			Assert.That(user.Token, Is.Not.Empty);
		}
	}
}
