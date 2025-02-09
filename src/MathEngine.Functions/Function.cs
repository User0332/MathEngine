using ExtendedNumerics;
using MathEngine.Algebra.Expressions;

namespace MathEngine.Functions;

public abstract class Function
{
	public abstract Expression ValueAt(Expression[] args);
	public abstract BigComplex Approximate(Expression[] args);
}
