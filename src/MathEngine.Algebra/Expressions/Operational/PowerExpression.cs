namespace MathEngine.Algebra.Expressions.Operational;

public sealed class PowerExpression(Expression left, Expression right) : OperationExpression(left, right, '^')
{
	public override Expression Simplify()
	{
		throw new NotImplementedException();
	}
}