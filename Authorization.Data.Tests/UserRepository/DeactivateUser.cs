using System.Linq;
using NUnit.Framework;

namespace Authorization.Data.Tests.UserRepository
{
	[TestFixture]
	public class DeactivateUser
	{
		private const string ConnectionString = "TestConnection";

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
		public void DeactivatedUserIsDeactivated()
		{
			var user = _repository.AddUser("Test User");

			_repository.DeactivateUser(user);

			var actual = _repository.GetUsers().FirstOrDefault(x => x.Id == user.Id);

			Assert.That(user.Disabled, Is.True);
		}

	}
}