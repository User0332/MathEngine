namespace MathEngine.Algebra.Expressions.Operational;

public abstract class OperationExpression : Expression
{
	public readonly Expression Left;
	public readonly Expression Right;
	public readonly string Operator;

	internal OperationExpression(Expression left, Expression right, string op)
	{
		Left = left;
		Right = right;
		Operator = op;
	}

	internal OperationExpression(Expression left, Expression right, char op) : this(left, right, op.ToString()) { }

	public abstract override Expression Simplify();

	public override string ToString()
	{
		return $"({Left}){Operator}({Right})";
	}
}