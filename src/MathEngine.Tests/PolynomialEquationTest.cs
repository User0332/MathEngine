using MathEngine.Algebra;
using MathEngine.Algebra.Equations;
using MathEngine.Algebra.Expressions.Polynomial;
using MathEngine.Algebra.Solver;
using MathEngine.Algebra.Solver.Polynomial;

namespace MathEngine.Tests;

public static class PolynomialEquationTest
{
	public static void Run()
	{
		var x = Variable.X;

		var eq = ((x^2)+5*x+6).SetEqualTo(2*x+8).ToPolynomial();

		var solns = eq.Solve(strategy: PolynomialSolvingStrategy.UseFormula).Select(soln => soln.Simplify()).Distinct();

		foreach (var soln in solns)
		{
			Console.WriteLine(soln.LaTeX());
		}
	}
}