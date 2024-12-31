namespace MathEngine.Algebra.Expressions.Operational;

public sealed class ProductExpression(Expression left, Expression right) : OperationExpression(left, right, '*')
{
	public override Expression Simplify() // TODO: simplification for rationals, etc. [see TODO file]
	{
		var (simplLeft, simplRight) = (Left.Simplify(), Right.Simplify());
		
		Expression? simpl = null;

		if (SimplificationUtils.GetRationalValue(simplLeft, out var leftRat) && SimplificationUtils.GetRationalValue(simplRight, out var rightRat))
		{
			return SimplificationUtils.ToExpression(leftRat*rightRat);
		}

		// Distributive Property
		if (simplLeft is SumExpression sumExpr) simpl = new SumExpression(new ProductExpression(sumExpr.Left, Right), new ProductExpression(sumExpr.Right, Right));
		else if (simplRight is SumExpression sumExprR) simpl = new SumExpression(new ProductExpression(sumExprR.Left, Left), new ProductExpression(sumExprR.Right, Left));

		if (simpl is null) return new ProductExpression(simplLeft, simplRight);

		return simpl.Simplify();
	}
}