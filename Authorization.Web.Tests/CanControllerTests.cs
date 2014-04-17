using System;
using System.Collections.Generic;
using System.Net;
using Authorization.Web.Cache;
using Authorization.Web.Controllers;
using Authorization.Web.Objects;
using NUnit.Framework;
using Rhino.Mocks;

namespace Authorization.Web.Tests
{
	[TestFixture]
	public class CanControllerTests
	{
		[Test]
		public void Get_UserNotFound_ReturnsUnauthorized()
		{
			var mockUserCache = MockRepository.GenerateStub<IUserCache>();
			mockUserCache.Expect(x => x.GetUsers())
				.Return(new Dictionary<string, User>());

			var controller = new CanController(mockUserCache);
			var user = "testUser";
			var action = "testAction";

			var actual = controller.Get(user, action);

			Assert.That(actual.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
		}

		[Test]
		public void Get_UserNotAuthorized_ReturnsUnauthorized()
		{
			var mockUserCache = MockRepository.GenerateStub<IUserCache>();
			mockUserCache.Expect(x => x.GetUsers())
				.Return(new Dictionary<string, User>
				{
					{
						"testUser", new User
						{
							Name = "testUser",
							Actions = new[] {"otherAction"}
						}
					}
				});

			var controller = new CanController(mockUserCache);
			var user = "testUser";
			var action = "testAction";

			var actual = controller.Get(user, action);

			Assert.That(actual.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
		}

		[Test]
		public void Get_UserAuthorized_ReturnsOk()
		{
			var mockUserCache = MockRepository.GenerateStub<IUserCache>();
			mockUserCache.Expect(x => x.GetUsers())
				.Return(new Dictionary<string, User>
				{
					{
						"testUser", new User
						{
							Name = "testUser",
							Actions = new[] {"testAction"}
						}
					}
				});

			var controller = new CanController(mockUserCache);
			var user = "testUser";
			var action = "testAction";

			var actual = controller.Get(user, action);

			Assert.That(actual.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}
	}
}
