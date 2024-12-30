using MathEngine.Algebra;
using MathEngine.Algebra.Equations;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Algebra.Expressions.Polynomial;
using MathEngine.Algebra.Solver;

namespace MathEngine.Tests;

public static class PolynomialExprTest
{
	public static void Run()
	{
		var x = Variable.X;

		var expr = PolynomialExpression.From(
			(x^2) + 2*x + 1 + (-4*x)
		);

		var normalized = expr.Normalize();

		Console.WriteLine(expr);
		Console.WriteLine(expr.Degree);
		Console.WriteLine(expr.Repr());
		Console.WriteLine(normalized);
		Console.WriteLine(normalized.Repr());

		var zeroSide = PolynomialExpression.From(new ProductExpression((ValueExpression) 0, Variable.X));

		PolynomialEquation eq = new(expr, zeroSide); // TODO: actually implement SetZeroSide

		var solns = eq.Solve(PolynomialSolvingStrategy.ViaFormula);

		foreach (var soln in solns)
		{
			Console.WriteLine(soln);
		}


		// PolynomialExpression quadratic = new();

		// Console.WriteLine(quadratic);
		// Console.WriteLine(quadratic.Degree);
	}
}