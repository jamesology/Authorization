using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Authorization
{
	public class User
	{
		public string Name { get; set; }

		public ICollection<string> Roles { get; set; }

		public User()
		{
			Roles = new Collection<string>();
		}

		public void AddRoles(IEnumerable<string> roles)
		{
			foreach (var role in roles)
			{
				Roles.Add(role);
			}
		}
	}
}
