namespace MathEngine.Algebra.Expressions.Operational;

public sealed class PowerExpression(Expression left, Expression right) : OperationExpression(left, right, '^')
{
	public readonly Expression Base = left;
	public readonly Expression Exponent = right;

	public override Expression Simplify()
	{
		return this;
	}
}