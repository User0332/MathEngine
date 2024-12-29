namespace MathEngine.Algebra.Expressions.Operational;

public abstract class OperationExpression : Expression
{
	public readonly Expression Left;
	public readonly Expression Right;
	public readonly string Operator;

	public override bool Equals(Expression? other)
	{
		return other is OperationExpression opExpr && Operator == opExpr.Operator && opExpr.Left == Left && opExpr.Right == Right;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Left, Right, Operator);
	}

	internal OperationExpression(Expression left, Expression right, string op)
	{
		Left = left.Simplify();
		Right = right.Simplify();
		Operator = op;
	}

	internal OperationExpression(Expression left, Expression right, char op) : this(left, right, op.ToString()) { }

	public abstract override Expression Simplify();

	public override string ToString()
	{
		return $"({Left}){Operator}({Right})";
	}

	public override string Repr() // string.Replace() used to preserve cascading indentation
	{
		var name = GetType().Name;

		return @$"{name}(
  {Left.Repr().Replace("\n", "\n  ")},
  {Right.Repr().Replace("\n", "\n  ")}
)";
	}
}