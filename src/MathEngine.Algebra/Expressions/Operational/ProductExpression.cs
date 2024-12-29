namespace MathEngine.Algebra.Expressions.Operational;

public sealed class ProductExpression(Expression left, Expression right) : OperationExpression(left, right, '*')
{
	public override Expression Simplify() // TODO: simplification for rationals, etc. [see TODO file]
	{
		Expression? simpl = null;

		// Distributive Property
		if (Left is SumExpression sumExpr) simpl = new SumExpression(new ProductExpression(sumExpr.Left, Right), new ProductExpression(sumExpr.Right, Right));
		else if (Left is DifferenceExpression diffExpr) simpl = new DifferenceExpression(new ProductExpression(diffExpr.Left, Right), new ProductExpression(diffExpr.Right, Right));
		else if (Right is SumExpression sumExprR) simpl = new SumExpression(new ProductExpression(sumExprR.Left, Left), new ProductExpression(sumExprR.Right, Left));
		else if (Right is DifferenceExpression diffExprR) simpl = new DifferenceExpression(new ProductExpression(diffExprR.Left, Left), new ProductExpression(diffExprR.Right, Left));

		if (simpl is null) return this;

		return simpl.Simplify();
	}
}