using MathEngine.Values;

namespace MathEngine.Algebra.Expressions.Terms;

public sealed class ValueTerm(Value inner) : Term
{
	public readonly Value Inner = inner.Simplify();

	public override string ToString()
	{
		return Inner.ToString();
	}

	public static implicit operator ValueTerm(Value val) => new(val);
}