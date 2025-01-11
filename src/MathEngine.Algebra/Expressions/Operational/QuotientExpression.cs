namespace MathEngine.Algebra.Expressions.Operational;

public sealed class QuotientExpression(Expression left, Expression right) : OperationExpression(left, right, '/')
{
	public readonly Expression Numerator = left;
	public readonly Expression Denominator = right;

	public override Expression Simplify()
	{
		var (simplNumerator, simplDenominator) = (Numerator.Simplify(), Denominator.Simplify());

		Expression? simpl = null;

		if (SimplificationUtils.GetRationalValue(simplNumerator, out var numRat) && SimplificationUtils.GetRationalValue(simplDenominator, out var denomRat))
		{
			return SimplificationUtils.ToExpression(numRat/denomRat);
		}

		// Splitting Numerator (for (x+y)/n)
		if (simplNumerator is SumExpression sumExpr) simpl = new SumExpression(new QuotientExpression(sumExpr.Left, simplDenominator), new QuotientExpression(sumExpr.Right, simplDenominator));
		
		if (simpl is null) return new QuotientExpression(simplNumerator, simplDenominator);

		return simpl.Simplify();
	}

	public override string ToString()
	{
		string leftRepr, rightRepr;

		if (Left is SumExpression) leftRepr = $"({Left})"; // need to parenthesize lower-order operations (DifferenceExpression doesn't exist anymore, so we only need to account for this case)
		else leftRepr = Left.ToString();

		if (Right is SumExpression) rightRepr = $"({Right})";
		else rightRepr = Right.ToString();

		return $"{leftRepr}/{rightRepr}";
	}

	public override string LaTeX()
	{
		return $"\\frac{{ {Left.LaTeX()} }}{{ {Right.LaTeX()} }}";
	}

}