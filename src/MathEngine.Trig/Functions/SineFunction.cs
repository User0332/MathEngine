using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Values;
using MathEngine.Values.Real.IrrationalValues;
using MathEngine.Values.Real.RationalValues;
using Rationals;

namespace MathEngine.Trig.Functions;

internal sealed class SineFunction : BaseTrigFunction
{
	internal SineFunction() {}
	
	public override Expression ApplyApproximate(Expression x) // todo: use power series
	{
		throw new NotImplementedException();
	}

	public override bool TryCalculateExactValue(Expression x, out Expression y)
	{	
		y = null!;
	
		if (x == Expression.Zero)
		{
			y = Expression.Zero;
			return true;
		}

		if (x is not ProductExpression prodExpr) return false;
		
		if (prodExpr.Left is not ValueExpression valExprL || prodExpr.Right is not ValueExpression valExprR) return false;

		var (valL, valR) = (valExprL.Inner, valExprR.Inner);

		Value notPi;

		if (SpecialConstantValue.PI == valL)
		{
			notPi = valR;
		}
		else if (SpecialConstantValue.PI == valR)
		{
			notPi = valL;
		}
		else
		{
			return false;
		}

		if (notPi is not RationalValue ratVal) return false;

		Rational frac = (ratVal.InnerValue.WholePart % 2) + ratVal.InnerValue.FractionPart; // mod 2 pi since sine is periodic

		if (ratVal.InnerValue == 1) // sin(pi) == 0
		{
			y = Expression.Zero;
			return true;
		}

		var (num, den) = (ratVal.InnerValue.Numerator, ratVal.InnerValue.Denominator);

		// first match our pi/6, pi/4, pi/3 and pi/2 families, then we will move to angle addition formulas and other identities if nothing matches
	}
}