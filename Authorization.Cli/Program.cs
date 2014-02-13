using System;
using System.Linq;
using Authorization.Cli.Executors;
using Authorization.Cli.Startup;
using log4net;
using StructureMap;

[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "log4net")]
namespace Authorization.Cli
{
	class Program
	{
		static void Main(string[] args)
		{
			StructureMapConfig.Bootstrap();

			var log = LogManager.GetLogger("Authorization.Cli");

			//log.Debug(ObjectFactory.WhatDoIHave());

			args = EnsureArguments(args);

			foreach (var word in args)
			{
				log.Info(word);
			}

			try
			{
				var executor = ObjectFactory.GetInstance<IExecutor>();
				executor.Execute(args, log);
			}
			catch (Exception ex)
			{
				LogException(ex, log);
			}

			Console.ReadLine();
		}

		private static string[] EnsureArguments(string[] args)
		{
			var result = args;
			while (result.Any() == false)
			{
				Console.WriteLine("Type something will you, we're paying for this.");

				var input = Console.ReadLine();

				if (String.IsNullOrWhiteSpace(input) == false)
				{
					result = input.Split(' ');
				}
			}

			return result;
		}

		private static void LogException(Exception ex, ILog log)
		{
			do
			{
				log.Error(ex.Message);
				log.Error(ex.StackTrace);
				ex = ex.InnerException;
			} while (ex != null);
		}
	}
}
