namespace MathEngine.Algebra.Expressions.Operational;

public sealed class DifferenceExpression(Expression left, Expression right) : OperationExpression(left, right, '-')
{
	public override Expression Simplify()
	{
		throw new NotImplementedException();
	}
}