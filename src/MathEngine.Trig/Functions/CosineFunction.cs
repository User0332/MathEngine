using ExtendedNumerics;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Values;
using MathEngine.Values.Real.IrrationalValues;
using MathEngine.Values.Real.RationalValues;
using Rationals;

namespace MathEngine.Trig.Functions;

internal sealed class CosineFunction : BaseTrigFunction
{
	internal CosineFunction() : base("cos") {}
	
	public override BigComplex Approximate(BigComplex x)
	{
		return BigComplex.Cos(x);
	}

	public override bool TryCalculateExactValue(Expression x, out Expression y)
	{

		y = null!;
	
		if (x == Expression.Zero)
		{
			y = Expression.One;
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

		Rational frac = ((ratVal.InnerValue.WholePart % 2) + ratVal.InnerValue.FractionPart).CanonicalForm; // mod 2 pi since sine is periodic

		if (frac == 1) // cos(pi) == -1
		{
			y = Expression.NegativeOne;
			return true;
		}

		var (num, den) = (frac.Numerator, frac.Denominator);

		// first match our pi/6, pi/4, pi/3 and pi/2 families

		if (den == 2)
		{
			y = Expression.Zero;
			return true;
		}

		int adjRes = 1;

		if (frac > (Rational.One/2) && frac < (3*Rational.One/2)) adjRes*=-1;

		if (den == 6)
		{
			y =  adjRes*((ValueExpression) 3).Sqrt()/2;
			return true;
		}

		if (den == 4)
		{
			y = adjRes*((ValueExpression) 2).Sqrt()/2;
			return true;
		}

		if (den == 3)
		{
			y = (ValueExpression) 0.5*adjRes;
			return true;
		}

		return false;
	}
}