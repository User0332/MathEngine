using MathEngine.Algebra.Equations;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Algebra.Expressions.Polynomial;
using Rationals;

namespace MathEngine.Algebra.Solver.Polynomial;

public static partial class PolynomialEquationSolver
{
	static IEnumerable<Expression> SolveViaFormula(PolynomialEquation eq)
	{
		var expr = (NormalizedPolynomialExpression) eq.LeftSide; // must find the roots of this

		if (expr.Degree == 2) // use quadratic formula & return factors
		{
			// in normalized simplified form, we can assume that the terms follow the normalized pattern

			ProductExpression quadraticTerm = expr.GetTermOfDegree(2);
			ProductExpression linearTerm = expr.GetTermOfDegree(1);
			ProductExpression constantTerm = expr.GetTermOfDegree(0);
			
			var a = quadraticTerm.Left;
			var b = linearTerm.Left;
			var c = constantTerm.Left;

			var discriminant = (b^2)-4*a*c;

			var denominator = 2*a;

			var factorOne = (-b + (discriminant^((Rational) 1/2)))/denominator;
			var factorTwo = (-b - (discriminant^((Rational) 1/2)))/denominator;

			return [factorOne, factorTwo];
		}
		else if (expr.Degree == 1) // return the root of ax+b=0
		{
			ProductExpression linearTerm = expr.GetTermOfDegree(1);
			Expression constantTerm = expr.GetTermOfDegree(0);

			var a = linearTerm.Left;
			var b = constantTerm;

			var root = -b/a;

			return [root];
		}
		else throw new NotImplementedException($"MathEngine.Algebra does not support a formula for a polynomial of degree {expr.Degree}");
	}
}