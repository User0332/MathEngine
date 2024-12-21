using System.Collections.Immutable;
using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Terms;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Tests;

public static class PolynomialExprTest
{
	public static void Run()
	{
		ImmutableArray<Term> terms = [
			new NthPowerOf(Variable.X, (ValueTerm) new IntegerValue(2)),
			new NthPowerOf(Variable.Y, (ValueTerm) new IntegerValue(3)),
			Variable.Z,
		];

		Console.WriteLine(new Expression(terms));
		Console.WriteLine(new PolynomialExpression(terms));
	}
}