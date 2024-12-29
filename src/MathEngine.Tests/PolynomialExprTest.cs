using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Polynomial;

namespace MathEngine.Tests;

public static class PolynomialExprTest
{
	public static void Run()
	{
		var x = Variable.X;

		var expr = PolynomialExpression.From(
			(x^2) + 2*x + 1
		);

		var normalized = expr.Normalize();

		Console.WriteLine(expr);
		Console.WriteLine(expr.Degree);
		Console.WriteLine(normalized);


		// PolynomialExpression quadratic = new();

		// Console.WriteLine(quadratic);
		// Console.WriteLine(quadratic.Degree);
	}
}