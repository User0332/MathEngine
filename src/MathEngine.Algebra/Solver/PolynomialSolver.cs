using MathEngine.Algebra.Equations;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Values.Real.RationalValues;
using Rationals;

namespace MathEngine.Algebra.Solver;

public static class EquationSolver
{
	public static IEnumerable<Expression> SolveFor(this Equation eq, Variable var)
	{
		return [ (ValueExpression) IntegerValue.One ];
	}

	static IEnumerable<Expression> SolveViaFormula(PolynomialEquation eq)
	{
		var expr = eq.SetZeroSide().LeftSide.Normalize(); // must find the roots of this

		if (expr.Degree > 2) throw new UnsolvableEquationException("Equation solving for polynomials of degree 3 or greater is currently unsupported");
		else if (expr.Degree == 2) // use quadratic formula & return factors
		{
			// in normalized simplified form, we can assume that the terms follow the normalized pattern

			ProductExpression quadraticTerm = (expr.GetTermOfDegree(2) as ProductExpression)!;
			ProductExpression linearTerm = (expr.GetTermOfDegree(1) as ProductExpression)!;
			Expression constantTerm = expr.GetTermOfDegree(0);
			
			var a = quadraticTerm.Left;
			var b = linearTerm.Left;
			var c = constantTerm;

			var discriminant = (b^2)-4*a*c;

			var denominator = 2*a;

			var factorOne = (-b + (discriminant^((Rational) 1/2)))/denominator;
			var factorTwo = (-b - (discriminant^((Rational) 1/2)))/denominator;

			return [factorOne, factorTwo];
		}
		else if (expr.Degree == 1) // return the root of ax+b=0
		{
			ProductExpression linearTerm = (expr.GetTermOfDegree(1) as ProductExpression)!;
			Expression constantTerm = expr.GetTermOfDegree(0);

			var a = linearTerm.Left;
			var b = constantTerm;

			var root = -b/a;

			return [root];
		}
		else // degree is zero, we do not support this yet
		{
			throw new NotImplementedException("Solving of polynomial equations of degree 0 is currently unsupported");
		}
	}

	public static IEnumerable<Expression> Solve(this PolynomialEquation eq, PolynomialSolvingStrategy strategy)
	{
		switch (strategy)
		{
			case PolynomialSolvingStrategy.ViaFormula: return SolveViaFormula(eq);
			case PolynomialSolvingStrategy.ViaZeroProductProperty: throw new NotImplementedException("solving via factoring and zero product property is currently unsupported");
			default: throw new ArgumentException("Invalid solving strategy");
		}
	}
}

public enum PolynomialSolvingStrategy
{
	ViaFormula,
	ViaZeroProductProperty
}