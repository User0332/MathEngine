using ExtendedNumerics;
using MathEngine.Algebra.Expressions;

namespace MathEngine.Trig.Functions;

internal sealed class TangentFunction : BaseTrigFunction
{
	internal TangentFunction() : base("tan") {}
	
	public override BigComplex Approximate(BigComplex x)
	{
		return BigComplex.Tan(x);
	}

	public override bool TryCalculateExactValue(Expression x, out Expression y)
	{
		var internalSin = (BaseTrigFunction) TrigFunctions.Sin;
		var internalCos = (BaseTrigFunction) TrigFunctions.Cos;

		if (internalSin.TryCalculateExactValue(x, out var sinValue) && internalCos.TryCalculateExactValue(x, out var cosValue))
		{
			if (cosValue == Expression.Zero)
			{
				y = Expression.Undefined;
				return true;
			}

			y = sinValue / cosValue;
			return true;
		}

		y = default!;

		return false;
	}
}