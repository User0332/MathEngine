using ExtendedNumerics;
using MathEngine.Algebra.Expressions;

namespace MathEngine.Trig.Functions;

internal sealed class CotangentFunction : BaseTrigFunction
{
	internal CotangentFunction() : base("cot") {}
	
	public override BigComplex Approximate(BigComplex x)
	{
		return 1/BigComplex.Tan(x);
	}

	public override bool TryCalculateExactValue(Expression x, out Expression y)
	{
		var internalSin = (BaseTrigFunction) TrigFunctions.Sin;
		var internalCos = (BaseTrigFunction) TrigFunctions.Cos;

		if (internalSin.TryCalculateExactValue(x, out var sinValue) && internalCos.TryCalculateExactValue(x, out var cosValue))
		{
			if (sinValue == Expression.Zero)
			{
				y = Expression.Undefined;
				return true;
			}
			
			y = cosValue / sinValue;
			return true;
		}

		y = default!;
		
		return false;
	}
}