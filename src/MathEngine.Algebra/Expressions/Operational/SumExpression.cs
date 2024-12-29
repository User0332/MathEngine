using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Expressions.Operational;

public sealed class SumExpression(Expression left, Expression right) : OperationExpression(left, right, '+')
{
	public override Expression Simplify() // combine like terms, but only if the coefficient can be resolved to a non-SumExpression value, (so distributive property and combining like terms do not get infinitely recursively applied)
	{
		if (Left.Equals(Right)) return new ProductExpression((ValueExpression) 2, Left);
		
		if (Left is ValueExpression valExprL && Right is ValueExpression valExprR && valExprL.Inner is RationalValue ratL && valExprR.Inner is RationalValue ratR)
		{
			return new ValueExpression(ratR+ratL);
		}

		var firstTry = InternalTryCombineLikeTerms(Left, Right);

		if (firstTry is not null) return firstTry;

		var secondTry = InternalTryCombineLikeTerms(Right, Left);

		if (secondTry is not null) return secondTry;

		return this;
	}
	
	static ProductExpression? InternalTryCombineLikeTerms(Expression left, Expression right)
	{
		if (left is ProductExpression prodExprL && right is ProductExpression prodExprR)
		{
			if (prodExprR.Left.Equals(prodExprL.Left))
			{
				var coeffOne = prodExprR.Right;
				var coeffTwo = prodExprL.Right;
				var coeffSum = (coeffOne+coeffTwo).Simplify();

				if (coeffSum is not SumExpression) // we can resolve this to lower than a sum!
				{
					return new ProductExpression(coeffSum, prodExprL.Left); // prodExpr*.Left is the value being multiplied by the coefficient
				}
			}
		}

		return null;
	}
}