using System.Numerics;

namespace MathEngine.Values;

public class SpecialConstant : RatioValue
{
	public static readonly SpecialConstant PI = new(Math.PI, "π");
	public static readonly SpecialConstant E = new(Math.E, "e");

	readonly string Repr;
	readonly double ResolvesTo;

	private SpecialConstant(double resolvesTo, string repr) : base(null!, ConstantValue.One)
	{
		ResolvesTo = resolvesTo;
		Repr = repr;

		Numerator = this;
	}

	public override Complex Approximate()
	{
		return ResolvesTo;
	}

	public override Value Simplify()
	{
		return this;
	}

	public override string ToString()
	{
		return Repr;
	}
}