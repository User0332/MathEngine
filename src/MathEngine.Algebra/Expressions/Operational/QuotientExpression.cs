using MathEngine.Algebra.Expressions.Simplification;

namespace MathEngine.Algebra.Expressions.Operational;

public sealed class QuotientExpression(Expression left, Expression right) : OperationExpression(left, right, '/')
{
	public readonly Expression Numerator = left;
	public readonly Expression Denominator = right;

	public override Expression Simplify(SavedSimplificationInfo? info)
	{
		var (simplNumerator, simplDenominator) = (Numerator.Simplify(info), Denominator.Simplify(info));

		if (simplNumerator == Zero && simplDenominator == Zero) return new IndeterminateExpression(IndeterminateExpression.Form.ZeroOverZero);
		
		if (simplDenominator == Zero || simplDenominator == Undefined || simplNumerator == Undefined) return Undefined;

		if (simplNumerator == Zero) return Zero;
		// TODO: this is the case where we use info
		if (simplNumerator == simplDenominator && !simplDenominator.ContainsVariable()) return One; // this may lead to undefined if the denominator is zero, so we make sure it can never be zero by the above if cases (and checking here that there is no variable in it, which could cause it to become zero)
		if (simplDenominator == One) return simplNumerator;

		Expression? simpl = null;

		if (SimplificationUtils.GetRationalValue(simplNumerator, out var numRat) && SimplificationUtils.GetRationalValue(simplDenominator, out var denomRat))
		{
			return SimplificationUtils.ToExpression(numRat/denomRat);
		}

		// Splitting Numerator (for (x+y)/n)
		if (simplNumerator is SumExpression sumExpr) simpl = new SumExpression(new QuotientExpression(sumExpr.Left, simplDenominator), new QuotientExpression(sumExpr.Right, simplDenominator));
	
		// n/d = rational(1/d)*(n), d E Q
		else if (SimplificationUtils.GetRationalValue(simplDenominator, out var denomVal)) simpl = new ProductExpression(simplNumerator, (ValueExpression) (1/denomVal.InnerValue));

		if (simpl is null) return new QuotientExpression(simplNumerator, simplDenominator);

		return simpl.Simplify(info);
	}

	public override string ToString()
	{
		string leftRepr, rightRepr;

		if (Left is SumExpression) leftRepr = $"({Left})"; // need to parenthesize lower-order operations (DifferenceExpression doesn't exist anymore, so we only need to account for this case)
		else leftRepr = Left.ToString();

		if (Right is SumExpression) rightRepr = $"({Right})";
		else rightRepr = Right.ToString();

		return $"({leftRepr}/{rightRepr})";
	}

	public override string LaTeX()
	{
		return $"\\frac{{ {Left.LaTeX()} }}{{ {Right.LaTeX()} }}";
	}

	public override Expression Substitute(Variable var, Expression val)
	{
		return new QuotientExpression(Left.Substitute(var, val), Right.Substitute(var, val));
	}
}