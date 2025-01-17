using MathEngine.Algebra.Equations;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Algebra.Expressions.Polynomial;
using Rationals;

namespace MathEngine.Algebra.Solver.Polynomial;

public static partial class PolynomialEquationSolver
{
	static IEnumerable<Expression> SolveViaFactoring(PolynomialEquation eq)
	{
		if (eq.Degree <= 2) return SolveViaFormula(eq); 
		
		var expr = (NormalizedPolynomialExpression) eq.LeftSide; // need to find roots of expr

		var rest = expr;
		List<Expression> accumRoots = [];

		while (rest.Degree > 2)
		{
			var terms = rest.NormalizedTerms.Select(term => term.Simplify()).Where(term => term != Expression.Zero);

			Console.WriteLine(terms);
		}

		return accumRoots.Concat(
			SolveViaFormula(
				new PolynomialEquation(rest, PolynomialExpression.ZeroExpr(rest.Variable))
			)
		);
	}
}