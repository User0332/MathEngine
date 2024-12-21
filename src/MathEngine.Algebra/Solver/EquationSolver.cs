using MathEngine.Algebra.Equations;
using MathEngine.Algebra.Expressions;
using MathEngine.Values;
using MathEngine.Values.Arithmetic;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Solver;

public static class EquationSolver
{
	public static Expression SolveFor(this Equation eq, Variable var)
	{
		return new Expression(new Factor(IntegerValue.One));
	}

	public static Value SolveForValue(this Equation eq, Variable var)
	{
		var expr = eq.SolveFor(var);

		if (expr.Terms.Any(fac => fac.Terms.Any(term => term is Variable)))
			throw new InvalidOperationException($"Cannot fully resolve variable {var} to a constant value");

		Value product = ProductValue.Identity;

		foreach (var fac in expr.Terms) 
		{
			Value sum = SumValue.Identity;

			foreach (var term in fac.Terms)
			{
				sum = sum.AddTo(term);
			}

			product = product.MultiplyBy(sum);
		}

		return product;
	}
}