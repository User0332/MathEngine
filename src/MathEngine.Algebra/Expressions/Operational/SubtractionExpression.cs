namespace MathEngine.Algebra.Expressions.Operational;

public sealed class SubtractionExpression(Expression left, Expression right) : OperationExpression(left, right, '-')
{
	public override Expression Simplify()
	{
		throw new NotImplementedException();
	}
}