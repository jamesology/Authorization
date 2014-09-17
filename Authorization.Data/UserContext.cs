using System.Data.Entity;

namespace Authorization.Data
{
	public class UserContext : DbContext
	{
		public IDbSet<User> Users { get; set; }

		public UserContext(string connectionString) : base(connectionString)
		{
			
		}
	}
}