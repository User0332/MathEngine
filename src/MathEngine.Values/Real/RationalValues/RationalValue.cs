namespace MathEngine.Values.Real.RationalValues;

public class RationalValue(Rational val) : RealValue
{
	public readonly Rational InnerValue = val.CanonicalForm;

	public override BigComplex Approximate()
	{
		return new((BigDecimal) (double) InnerValue);
	}

	public override RationalValue Simplify()
	{
		if (InnerValue.Denominator == 1) return new IntegerValue(InnerValue.Numerator);

		return this;
	}

	public override bool Equals(object? obj)
	{
		return Equals(obj as RationalValue);
	}

	public bool Equals(RationalValue? other)
	{
		return other?.InnerValue == InnerValue;
	}

	public override int GetHashCode()
	{
		return InnerValue.GetHashCode();
	}

	public override string ToString()
	{
		return $"({InnerValue})";
	}

	public override string LaTeX()
	{
		return $"\\frac{{ {InnerValue.Numerator} }} {{ {InnerValue.Denominator} }}";;
	}

	public static RationalValue operator +(RationalValue a, RationalValue b)
	{
		return new(a.InnerValue + b.InnerValue);
	}

	public static RationalValue operator -(RationalValue a, RationalValue b)
	{
		return new(a.InnerValue - b.InnerValue);
	}

	public static RationalValue operator *(RationalValue a, RationalValue b)
	{
		return new(a.InnerValue * b.InnerValue);
	}

	public static RationalValue operator /(RationalValue a, RationalValue b)
	{
		return new(a.InnerValue / b.InnerValue);
	}

	public static bool operator ==(RationalValue self, RationalValue other) => self.Equals(other);
	public static bool operator !=(RationalValue self, RationalValue other) => !(self == other);
}