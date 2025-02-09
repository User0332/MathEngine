using ExtendedNumerics;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Functions;
using MathEngine.Values;
using MathEngine.Values.Real.IrrationalValues;
using MathEngine.Values.Real.RationalValues;
using Rationals;

namespace MathEngine.Trig.Functions;

internal sealed class SineFunction : BaseTrigFunction
{
	internal SineFunction() : base("sin") {}
	
	public override BigComplex Approximate(BigComplex x) // todo: use power series
	{
		return BigComplex.Sin(x);
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

		if (frac == 1 || frac == 0) // sin(pi) == 0, sin(0) == 0
		{
			y = Expression.Zero;
			return true;
		}

		var (num, den) = (frac.Numerator, frac.Denominator);

		// first match our pi/6, pi/4, pi/3 and pi/2 families

		if (den == 2)
		{
			if (num == 1)
			{
				y = Expression.One;
				return true;
			}

			if (num == 3)
			{
				y = Expression.NegativeOne;
				return true;
			}
		}

		if (den == 6 && (num == 1 || num == 5))
		{
			y = (ValueExpression) 0.5;
			return true;
		}

		if (den == 4 && (num == 1 || num == 3))
		{
			y = ((ValueExpression) 2).Sqrt()/2;
			return true;
		}

		if (den == 3 && (num == 1 || num == 2))
		{
			y = ((ValueExpression) 3).Sqrt()/2;
			return true;
		}

		if (den == 6 && (num == 7 || num == 11))
		{
			y = (ValueExpression) (-0.5);
			return true;
		}
		
		if (den == 4 && (num == 5 || num == 7))
		{
			y = ((ValueExpression) (-2)).Sqrt()/2;
			return true;
		}

		if (den == 3 && (num == 4 || num == 5))
		{
			y = -((ValueExpression) 3).Sqrt()/2;
			return true;
		}

		// now try to use angle addition identities

		return false;
	}
}