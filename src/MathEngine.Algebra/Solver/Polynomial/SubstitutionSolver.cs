using MathEngine.Algebra.Equations;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Algebra.Expressions.Polynomial;
using Rationals;

namespace MathEngine.Algebra.Solver.Polynomial;

public static partial class PolynomialEquationSolver
{
	static IEnumerable<Expression> SolveViaSubstitution(PolynomialEquation eq, PolynomialSolvingStrategy strategy)
	{
		if (eq.Degree <= 2) return SolveViaFormula(eq); // no matter what the other strategy is, this type of polynomial will always be dispatched to SolveViaFormula so we can skip the conditional logic and dispatch it directly
		
		var expr = (NormalizedPolynomialExpression) eq.LeftSide; // need to find roots of expr

		var terms = expr.NormalizedTerms.Where(term => term.Left != Expression.Zero);

		// if there are any terms with odd exponents... we are unable to substitute here, dispatch to secondary strategy [TODO: need to cover the case where factoring and THEN substitution works]
		if (terms.Any(term => PolynomialExpression.DegreeOf(term) % 2 != 0)) return eq.Solve(strategy);

		// substitute the even exponents and divide degrees by 2 (perform one even division substitution)
		var substTerms = terms.Select(
			term => new ProductExpression(
				term.Left,
				new PowerExpression(
					expr.Variable,
					(ValueExpression) (
						NormalizedPolynomialExpression.DegreeOfNormalizedTerm(term)/2
					)
				)
			)
		).ToArray(); // these should be normalized, so we should be able to pass them directly to NormalizedPolynomialExpression

		PolynomialEquation substEq = new(
			new NormalizedPolynomialExpression(substTerms),
			PolynomialExpression.ZeroExpr()
		);

		// use recursion in case another round of subst can be performed, otherwise, this call will dispatch to the secondary solving method
		IEnumerable<Expression> substSolns = SolveViaSubstitution(substEq, strategy);

		// since we used a substitution, (think u = x^2), we need to take both the positive and negative roots of all solutions returned to return the full list of solns

		IEnumerable<Expression> fullSolns = substSolns.SelectMany<Expression, Expression>(
			soln => {
				var posRoot = new PowerExpression(soln, Expression.OneHalf);
				
				return [
					posRoot,
					new ProductExpression(Expression.NegativeOne, posRoot)
				];
			}
		);

		return fullSolns;
	}
}