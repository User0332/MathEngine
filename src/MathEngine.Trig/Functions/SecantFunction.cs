using ExtendedNumerics;
using MathEngine.Algebra.Expressions;

namespace MathEngine.Trig.Functions;

internal sealed class SecantFunction : BaseTrigFunction
{
	internal SecantFunction() : base("sec") {}
	
	public override BigComplex Approximate(BigComplex x)
	{
		return 1/BigComplex.Cos(x);
	}

	public override bool TryCalculateExactValue(Expression x, out Expression y)
	{
		var internalCos = (BaseTrigFunction) TrigFunctions.Cos;

		if (internalCos.TryCalculateExactValue(x, out var cosValue))
		{
			if (cosValue == Expression.Zero)
			{
				y = Expression.Undefined;
				return true;
			}
			
			y = 1 / cosValue;
			return true;
		}

		y = default!;
		
		return false;
	}
}