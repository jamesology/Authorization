using System.Linq;
using NUnit.Framework;

namespace Authorization.Data.Tests.UserRepository
{
	[TestFixture]
	public class GetUsers
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
		public void NoUsersReturnsEmptyCollection()
		{
			var users = _repository.GetUsers().ToList();

			Assert.That(users, Is.Empty);
		}

		[Test]
		public void ReturnsUsers()
		{
			_repository.AddUser("User1");
			_repository.AddUser("User2");

			var users = _repository.GetUsers().ToList();
			var user2 = _repository.GetUsers().FirstOrDefault(x => x.Name == "User2");

			Assert.That(users, Has.Count.EqualTo(2));
			Assert.That(user2, Is.Not.Null);
		}
	}
}