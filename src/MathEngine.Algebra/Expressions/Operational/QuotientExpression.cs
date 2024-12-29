namespace MathEngine.Algebra.Expressions.Operational;

public sealed class QuotientExpression(Expression left, Expression right) : OperationExpression(left, right, '/')
{
	public readonly Expression Numerator = left;
	public readonly Expression Denominator = right;

	public override Expression Simplify()
	{
		Expression? simpl = null;

		// Distributive Property (for 1/n)
		if (Numerator is SumExpression sumExpr) simpl = new SumExpression(new QuotientExpression(sumExpr.Left, Denominator), new QuotientExpression(sumExpr.Right, Denominator));
		else if (Numerator is DifferenceExpression diffExpr) simpl = new DifferenceExpression(new QuotientExpression(diffExpr.Left, Denominator), new QuotientExpression(diffExpr.Right, Denominator));
		
		if (simpl is null) return this;

		return simpl.Simplify();
	}
}