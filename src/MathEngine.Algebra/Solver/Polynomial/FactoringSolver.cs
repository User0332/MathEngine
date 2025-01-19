using MathEngine.Algebra.Equations;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Algebra.Expressions.Polynomial;

namespace MathEngine.Algebra.Solver.Polynomial;

public static partial class PolynomialEquationSolver
{
	static IEnumerable<Expression> SolveViaFactoring(PolynomialEquation eq)
	{
		if (eq.Degree <= 2) return SolveViaFormula(eq); 
		
		var expr = (NormalizedPolynomialExpression) eq.LeftSide; // need to find roots of expr

		var rest = expr;
		List<Expression> accumRoots = [];


		var terms = rest.NormalizedTerms.Where(term => term.Left != Expression.Zero);

		// first method: find GCF
		// NOTE: we just want to reduce the degree of the polynomial to two or less, so here we will just try to reduce using powers of x (to factor out x^n)

		int lowestDegree = terms.DefaultIfEmpty(
			(ProductExpression) PolynomialExpression.ZeroExpr(eq.Variable).BaseNode
		).Select(NormalizedPolynomialExpression.DegreeOfNormalizedTerm).Min();

		if (lowestDegree != 0) // we can factor out some Xs
		{
			// factoring out x^n yields zero as a root
			// don't multi-count zero as a root
			if (!accumRoots.Contains(Expression.Zero)) accumRoots.Add(Expression.Zero);

			// reduce degrees and set rest

			rest = SumExpression.FromTerms(
				[.. terms.Select(
					term => new ProductExpression(
						term.Left,
						new PowerExpression(
							expr.Variable,
							(ValueExpression) (
								NormalizedPolynomialExpression.DegreeOfNormalizedTerm(term)-lowestDegree
							)
						)
					)
				)]
			).ToPolynomial().Normalize();
		}

		if (rest.Degree > 2)
		{
			terms = rest.NormalizedTerms.Where(term => term.Left != Expression.Zero);

			// second method: factor by grouping

			// cannot factor by grouping with an odd number of terms
			if (terms.Count() % 2 != 0) throw new UnsolvableEquationException("Unable to factor the polynomial");
		
			// TODO: impl (use algebra to determine techniques for determining whether or not factoring by grouping can be used by examining coefficients)
		}
		
		return accumRoots.Concat(
			SolveViaFormula(
				new PolynomialEquation(rest, PolynomialExpression.ZeroExpr(rest.Variable))
			)
		);
	}
}