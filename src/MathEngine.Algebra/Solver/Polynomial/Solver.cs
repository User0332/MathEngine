using MathEngine.Algebra.Equations;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Algebra.Expressions.Polynomial;
using MathEngine.Values.Real.RationalValues;
using Rationals;

namespace MathEngine.Algebra.Solver.Polynomial;

public static partial class PolynomialEquationSolver
{
	public static IEnumerable<Expression> Solve(this PolynomialEquation eq, PolynomialSolvingStrategy strategy = PolynomialSolvingStrategy.All)
	{
		if ((strategy & PolynomialSolvingStrategy.UseFormula) != 0) return SolveViaFormula(eq);
		if ((strategy & PolynomialSolvingStrategy.Factor) != 0) return SolveViaFactoring(eq);
		
		throw new ArgumentException("Invalid solving strategy");
	}
}

[Flags]
public enum PolynomialSolvingStrategy
{
	UseFormula = 1 << 0,
	Factor = 1 << 1,
	Substitute = 1 << 2,
	All = UseFormula | Factor | Substitute
}