using System.Reflection;

namespace MathEngine.Tests;

public static class Program
{
	public static int Main(string[] args)
	{
		if (args.Length == 1)
		{
			var className = args[0];

			var currAsm = Assembly.GetExecutingAssembly();

			var type = currAsm.GetType($"MathEngine.Tests.{className}");

			var runMethod = type?.GetMethod("Run", BindingFlags.Public | BindingFlags.Static);

			if (runMethod is null)
			{
				Console.WriteLine("Invalid test specified!");

				return 1;
			}

			runMethod.Invoke(null, []);

			return 0;
		}
		else
		{
			Console.WriteLine("No test specified to run!");
			
			return 1;
		}
	}
}