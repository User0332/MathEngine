using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Solver.Polynomial;

namespace MathEngine.Tests;

public static class HighDegPolyTest
{
	public static void Run()
	{
		var x = Variable.X;

		var eq = ((x^3)+5*x+6).SetEqualTo(Expression.Zero).ToPolynomial();

		var solns = eq.Solve(strategy: PolynomialSolvingStrategy.Factor).Select(soln => soln.Simplify());

		foreach (var soln in solns)
		{
			Console.WriteLine(soln.LaTeX());
		}
	}
}