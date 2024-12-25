using MathEngine.Values;

namespace MathEngine.Algebra.Expressions;

public sealed class ValueExpression(Value inner) : Expression
{
	public readonly Value Inner = inner.Simplify();
	public override string ToString()
	{
		return Inner.ToString();
	}

	public static implicit operator ValueExpression(Value val) => new(val);
}