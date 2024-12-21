using System.Collections.Immutable;
using MathEngine.Algebra;
using MathEngine.Algebra.Equations;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Terms;
using MathEngine.Algebra.Solver;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Tests;

public static class PolynomialExprTest
{
	public static void Run()
	{
		ImmutableArray<Term> terms = [
			new NthPowerOf(Variable.X, new IntegerValue(2).AsTerm()),
			new NthPowerOf(Variable.X, new IntegerValue(3).AsTerm()),
			Variable.X,
			new IntegerValue(28).AsTerm()
		];

		PolynomialExpression expr = new(terms);

		Console.WriteLine(expr);
		Console.WriteLine(expr.Degree);

		PolynomialExpression quadratic = new([
			new NthPowerOf(Variable.X, new IntegerValue(2).AsTerm()),
			new Product([new IntegerValue(2).AsExpression(), new NthPowerOf(Variable.X, new IntegerValue(1).AsTerm()).AsExpression()]).AsTerm(),
			new IntegerValue(1).AsTerm()
		]);

		quadratic = quadratic.Simplify();

		PolynomialEquation quadrEq = new(quadratic, IntegerValue.Zero.AsExpression().ToPolynomial());

		Console.WriteLine(quadratic);
		Console.WriteLine(quadratic.Degree);
		
		var solns = quadrEq.SolveFor(Variable.X);

		foreach (var soln in solns)
		{
			Console.WriteLine(soln);
		}
	}
}