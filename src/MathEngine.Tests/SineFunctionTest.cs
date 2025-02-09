using MathEngine.Trig.Functions;
using static MathEngine.Algebra.Expressions.Expression;

namespace MathEngine.Tests;

public static class SineFunctionTest
{
	public static void Run()
	{
		var sin = TrigFunctions.Sin;

		Console.WriteLine(
			sin.ValueAt(
				3*PI/2
			).LaTeX()
		);
	}
}