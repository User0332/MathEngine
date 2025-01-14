using MathEngine.Algebra;
using MathEngine.Algebra.Equations;
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

		PolynomialEquation eq = new(expr, PolynomialExpression.ZeroExpr()); // TODO: actually implement SetZeroSide

		var solns = eq.Solve(strategy: PolynomialSolvingStrategy.UseFormula).Select(soln => soln.Simplify()).Distinct();

		foreach (var soln in solns)
		{
			Console.WriteLine(soln);
		}

		var secondExpr = PolynomialExpression.From(
			(3*x+10)*(4*x-15)
		);

		Console.WriteLine(secondExpr.Normalize());

		PolynomialEquation eq2 = new(secondExpr, PolynomialExpression.ZeroExpr());

		var solns2 = eq2.Solve(strategy: PolynomialSolvingStrategy.UseFormula).Select(soln => soln.Simplify()).Distinct();

		foreach (var soln in solns2)
		{
			Console.WriteLine(soln);
		}


		var thirdExpr = PolynomialExpression.From(
			(x^2) + 2*x + 3*x
		);

		Console.WriteLine(thirdExpr.Simplify());

		PolynomialEquation eq3 = new(thirdExpr, PolynomialExpression.ZeroExpr());

		// PolynomialEquation::Normalize currently broken
		
		var solns3 = eq3.Solve(strategy: PolynomialSolvingStrategy.UseFormula).Select(soln => soln.Simplify()).Distinct();

		foreach (var soln in solns3)
		{
			Console.WriteLine(soln);
		}
	}
}