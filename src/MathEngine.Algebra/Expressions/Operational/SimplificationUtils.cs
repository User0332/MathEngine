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

	public static bool EquivalentVariables(Expression one, Expression two)
	{
		return (one as Variable)?.Equals(two as Variable) ?? false;
	}

	public static bool TryCombineVariableDegree(Expression left, Expression right, out Expression combined)
	{
		if (left is Variable)
		{
			if (right is Variable)
			{
				if (left == right)
				{
					combined = new PowerExpression(left, (ValueExpression) 2);
					
					return true;
				}
			}
			else if (
				right is PowerExpression powExprR &&
				powExprR.Base is Variable baseVar &&
				baseVar == left && 
				powExprR.Exponent is ValueExpression valExpr &&
				valExpr.Inner is RationalValue ratVal
			)
			{
				combined = new PowerExpression(left, (ValueExpression) (ratVal+new RationalValue(1)));

				return true;
			}
		}
		else if (
			left is PowerExpression powExprL &&
			powExprL.Base is Variable baseVar &&
			powExprL.Exponent is ValueExpression valExpr &&
			valExpr.Inner is RationalValue ratVal
		)
		{
			if (right is Variable)
			{
				if (baseVar == right)
				{
					combined = new PowerExpression(left, (ValueExpression) (ratVal+new RationalValue(1)));
					
					return true;
				}
			}
			else if (
				right is PowerExpression powExprR &&
				powExprR.Base is Variable baseVarR &&
				baseVarR == baseVar && 
				powExprR.Exponent is ValueExpression valExprR &&
				valExprR.Inner is RationalValue ratValR
			)
			{
				combined = new PowerExpression(left, (ValueExpression) (ratValR+ratVal));

				return true;
			}
		}

		combined = null!;

		return false;
	}

	public static ValueExpression ToExpression(RationalValue val)
	{
		return (ValueExpression) val;
	}
}