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
		if (InnerValue.Denominator == 1) return new IntegerValue(val.Numerator);

		return this;
	}

	public override string ToString()
	{
		throw new NotImplementedException();
	}
}