using ExtendedNumerics;
using MathEngine.Algebra.Expressions;

namespace MathEngine.Trig.Functions;

internal sealed class CosecantFunction : BaseTrigFunction
{
	internal CosecantFunction() : base("csc") {}
	
	public override BigComplex Approximate(BigComplex x)
	{
		return 1/BigComplex.Sin(x);
	}

	public override bool TryCalculateExactValue(Expression x, out Expression y)
	{
		var internalSin = (BaseTrigFunction) TrigFunctions.Sin;

		if (internalSin.TryCalculateExactValue(x, out var sinValue))
		{
			if (sinValue == Expression.Zero)
			{
				y = Expression.Undefined;
				return true;
			}
			
			y = 1 / sinValue;
			return true;
		}

		y = default!;
		
		return false;
	}
}