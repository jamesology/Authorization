using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Authorization
{
	public class Role
	{
		public string Name { get; set; }

		public ICollection<string> Actions { get; set; }

		public Role()
		{
			Actions = new Collection<string>();
		}

		public void AddActions(IEnumerable<string> actions)
		{
			foreach (var action in actions)
			{
				Actions.Add(action);
			}
		}
	}
}
