using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Expressions.Operational;

internal static class SimplificationUtils
{
	public static bool GetRationalValue(Expression expr, out RationalValue ratVal)
	{
		var val = (expr as ValueExpression)?.Inner as RationalValue;
		
		if (val is not null)
		{
			ratVal = val;
			return true;
		}

		ratVal = null!;

		return false;
	}

	public static ValueExpression ToExpression(RationalValue val)
	{
		return (ValueExpression) val;
	}
}