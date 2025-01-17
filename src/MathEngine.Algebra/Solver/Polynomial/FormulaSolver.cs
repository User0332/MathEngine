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
		
		var expr = (NormalizedPolynomialExpression) eq.Normalize().LeftSide;

		return [];
	}
}