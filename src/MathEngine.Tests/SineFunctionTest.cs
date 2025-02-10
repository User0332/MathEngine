using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Functions;
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
			).Repr()
		);

		Console.WriteLine(
			new FunctionExpression("sin", [
				new ProductExpression((ValueExpression) 3, new ProductExpression(PI, (ValueExpression) 0.5))
			]).Repr()
		);
	}
}