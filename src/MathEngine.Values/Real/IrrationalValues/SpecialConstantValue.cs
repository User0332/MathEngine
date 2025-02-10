namespace MathEngine.Values.Real.IrrationalValues;

public sealed class SpecialConstantValue(string repr, string latexRepr, Rational resolvesTo) : IrrationalValue
{
	public static readonly SpecialConstantValue PI = new("π", "\\pi", Math.PI);
	public static readonly SpecialConstantValue E = new("e", Math.E);

	readonly string Repr = repr;
	readonly string LatexRepr = latexRepr;
	readonly Rational ResolvesTo = resolvesTo;

	public SpecialConstantValue(string repr, double resolvesTo) : this(repr, repr, resolvesTo) {}
	public SpecialConstantValue(string repr, Rational resolvesTo) : this(repr, repr, resolvesTo) {}
	public SpecialConstantValue(string repr, string latexRepr, double resolvesTo) : this(repr, latexRepr, (Rational) resolvesTo) {}

	public override BigComplex Approximate()
	{
		return new((BigDecimal) (double) ResolvesTo);
	}

	public override string LaTeX()
	{
		return LatexRepr;
	}

	public override string ToString()
	{
		return Repr;
	}
}