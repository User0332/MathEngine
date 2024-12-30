namespace MathEngine.Algebra.Expressions.Operational;

public sealed class DifferenceExpression(Expression left, Expression right) : OperationExpression(left, right, '-')
{
	public override Expression Simplify()
	{
		return this; // TODO: implement similar to SumExpression
	}
}