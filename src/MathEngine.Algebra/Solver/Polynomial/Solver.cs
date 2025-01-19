using MathEngine.Algebra.Equations;
using MathEngine.Algebra.Expressions;

namespace MathEngine.Algebra.Solver.Polynomial;

public static partial class PolynomialEquationSolver
{
	public static IEnumerable<Expression> Solve(this PolynomialEquation eq, PolynomialSolvingStrategy strategy = PolynomialSolvingStrategy.All)
	{
		if ((strategy & PolynomialSolvingStrategy.Substitute) != 0)
		{
			if ((strategy & PolynomialSolvingStrategy.UseFormula) == 0 && (strategy & PolynomialSolvingStrategy.Factor) == 0)
				throw new ArgumentException("Substitution alone cannot be used as a solving strategy, you must also use either factoring or general formula");

			return SolveViaSubstitution(eq.Normalize(), strategy & ~PolynomialSolvingStrategy.Substitute);
		}
		if ((strategy & PolynomialSolvingStrategy.Factor) != 0) return SolveViaFactoring(eq.Normalize());
		if ((strategy & PolynomialSolvingStrategy.UseFormula) != 0) return SolveViaFormula(eq.Normalize());
		
		throw new ArgumentException("Unknown solving strategy");
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