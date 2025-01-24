using MathEngine.Algebra.Expressions;

namespace MathEngine.Functions;

public abstract class Function
{
	public enum CalculationPrecision
	{
		Unknown,
		Exact,
		Approximate
	}

	public readonly bool HasExpressibleInverse = false;
	public readonly Function Inverse = null!;

	public virtual CalculationPrecision PrecisionAtPoint(Expression x) => CalculationPrecision.Unknown; 
	public abstract Expression ApplyExact(Expression x);
	public abstract Expression ApplyApproximate(Expression x);

}
