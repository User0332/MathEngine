using MathEngine.Values;
using MathEngine.Values.Real.RationalValues;
using Rationals;

namespace MathEngine.Algebra.Expressions;

public sealed class ValueExpression(Value inner) : Expression
{
	public readonly Value Inner = inner.Simplify();

	public override bool Equals(Expression? other)
	{
		return Equals(other as ValueExpression); // TODO: this is not actually implemented properly for Value objects, so Value.Equals needs to be a required abstract member
	}

	public bool Equals(ValueExpression? other)
	{
		return Inner.Equals(other?.Inner);
	}

	public override int GetHashCode()
	{
		return Inner.GetHashCode();
	}

	public override string LaTeX()
	{
		if (Inner is RationalValue ratVal && ratVal is not IntegerValue)
		{
			return $"\\frac{{ {ratVal.InnerValue.Numerator} }} {{ {ratVal.InnerValue.Denominator} }}";
		}

		return Inner.ToString();
	}

	public override string Repr()
	{
		return Inner.ToString();
	}

	public override string ToString()
	{
		return Inner.ToString();
	}

	public static implicit operator ValueExpression(Value val) => new(val);
	public static implicit operator ValueExpression(Rational val) => new RationalValue(val);
	public static implicit operator ValueExpression(int val) => new IntegerValue(val);
	public static implicit operator ValueExpression(double val) => (Rational) val;
}