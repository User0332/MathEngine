using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Polynomial;
using MathEngine.Algebra.Solver.Polynomial;

namespace MathEngine.Tests;

public static class HighDegPolyTest
{
	public static void Run()
	{
		var x = Variable.X;

		var eq = ((x^4)+(x^2)+4).SetEqualTo(PolynomialExpression.ZeroExpr()).ToPolynomial();

		var solns = eq.Solve(strategy: PolynomialSolvingStrategy.Substitute | PolynomialSolvingStrategy.UseFormula).Select(soln => soln.Simplify());

		foreach (var soln in solns)
		{
			Console.WriteLine(soln.LaTeX());
		}
	}
}