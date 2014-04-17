using System.Collections.Generic;

namespace Authorization.Web.Objects
{
	public class User
	{
		public string Name { get; set; }
		public IEnumerable<string> Actions;
	}
}