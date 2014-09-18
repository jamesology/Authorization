using System;
using System.Text;

namespace Authorization.Cli.Commands
{
	public static class DefaultCommands
	{
		public static string Status()
		{
			return "The app is running.";
		}

		public static string Help(string command = "")
		{
			var result = String.Empty;

			if (String.IsNullOrWhiteSpace(command))
			{
				var allCommands = new StringBuilder();
				allCommands.Append("Available commands (help [command name] to see parameter info):\n");

				foreach (var availableNamespace in Library.Commands)
				{
					foreach (var availableCommand in availableNamespace.Value)
					{
						var commandName = availableNamespace.Key == "DefaultCommands"
							? availableCommand.Key
							: String.Format("{0}.{1}", availableNamespace.Key, availableCommand.Key);

						allCommands.AppendFormat("\t{0}\n", commandName);
					}
				}

				result = allCommands.ToString();
			}
			else
			{
				var cmd = new ConsoleCommand(command);
				var validCommand = Library.Commands.ContainsKey(cmd.LibraryClassName);

				var methodDictionary = Library.Commands[cmd.LibraryClassName];
				if (methodDictionary.ContainsKey(cmd.Name) == false)
				{
					validCommand = false;
				}

				if (validCommand)
				{
					var parameterList = methodDictionary[cmd.Name];

					var allParameters = new StringBuilder();
					allParameters.AppendFormat("Available parameters for {0}\n", command);

					foreach (var parameterInfo in parameterList)
					{
						allParameters.AppendFormat("\t{0} {1}\n", parameterInfo.ParameterType, parameterInfo.Name);
					}

					result = allParameters.ToString();
				}
				else
				{
					var badCommandMessage = String.Format("Unrecognized command \'{0}.{1}\'. Please type a valid command.", cmd.LibraryClassName, cmd.Name);
					result = badCommandMessage;
				}
			}

			return result;
		}
	}
}
