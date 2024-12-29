using MathEngine.Values;
using MathEngine.Values.Real.RationalValues;
using Rationals;

namespace MathEngine.Algebra.Expressions;

public sealed class ValueExpression(Value inner) : Expression
{
	public readonly Value Inner = inner.Simplify();

	public override bool Equals(Expression? other)
	{
		return inner.Equals(other); // TODO: this is not actually implemented properly for Value objects, so Value.Equals needs to be a required abstract member
	}

	public override int GetHashCode()
	{
		return inner.GetHashCode();
	}

	public override string ToString()
	{
		return Inner.ToString();
	}

	public static implicit operator ValueExpression(Value val) => new(val);
	public static implicit operator ValueExpression(Rational val) => new(new RationalValue(val));
	public static implicit operator ValueExpression(int val) => new(new IntegerValue(val));
	public static implicit operator ValueExpression(double val) => new(new RationalValue((Rational) val));
}