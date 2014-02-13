using System;
using System.Linq;

namespace Authorization.Cli
{
	class Program
	{
		static void Main(string[] args)
		{
			//TODO: fire up structuremap

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
				Console.Write("{0} ", word);
			}
			Console.ReadLine();
		}
	}
}
