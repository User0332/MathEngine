using Rationals;

namespace MathEngine.Values.Real.IrrationalValues;

public class SpecialConstantValue(string repr, Rational resolvesTo) : IrrationalValue
{
	public static readonly SpecialConstantValue PI = new("π", Math.PI);
	public static readonly SpecialConstantValue E = new("e", Math.E);

	readonly string Repr = repr;
	readonly Rational ResolvesTo = resolvesTo;

	public SpecialConstantValue(string repr, double resolvesTo) : this(repr, Rational.Approximate(resolvesTo)) {}

	public override BigComplex Approximate()
	{
		return new((BigDecimal) (double) ResolvesTo);
	}

	public override string ToString()
	{
		return Repr;
	}
}