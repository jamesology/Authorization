using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Authorization.Cli
{
	internal static class Library
	{
		private static Dictionary<string, Dictionary<string, IEnumerable<ParameterInfo>>> _commandLibraries;

		public const string CommandNamespace = "Authorization.Cli.Commands";

		public static Dictionary<string, Dictionary<string, IEnumerable<ParameterInfo>>> Commands
		{
			get
			{
				if (_commandLibraries == null)
				{
					LoadCommands();
				}

				return _commandLibraries;
			}
		}
		private static void LoadCommands()
		{
			_commandLibraries = new Dictionary<string, Dictionary<string, IEnumerable<ParameterInfo>>>();

			var q = from t in Assembly.GetExecutingAssembly().GetTypes()
				where t.IsClass && t.Namespace == CommandNamespace
				select t;
			var commandClasses = q.ToList();

			foreach (var commandClass in commandClasses)
			{
				var methods = commandClass.GetMethods(BindingFlags.Static | BindingFlags.Public);
				var methodDictionary = new Dictionary<string, IEnumerable<ParameterInfo>>();
				foreach (var method in methods)
				{
					var commandName = method.Name;
					methodDictionary.Add(commandName, method.GetParameters());
				}

				_commandLibraries.Add(commandClass.Name, methodDictionary);
			}
		}
	}
}
