using System;
using System.Linq;
using Authorization.Cli.Startup;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "log4net")]
namespace Authorization.Cli
{
	class Program
	{
		static void Main(string[] args)
		{
			StructureMapConfig.Bootstrap();

			var log = LogManager.GetLogger("Authorization.Cli");

			while (args.Any() == false)
			{
				Console.WriteLine("Type something will you, we're paying for this.");

				var input = Console.ReadLine();

				if (String.IsNullOrWhiteSpace(input) == false)
				{
					args = input.Split(' ');
				}
			}

			//TODO: process command
			foreach (var word in args)
			{
				log.Info(word);
				Console.Write("{0} ", word);
			}
			Console.ReadLine();
		}
	}
}
