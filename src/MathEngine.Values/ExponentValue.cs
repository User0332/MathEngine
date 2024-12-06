using System.Numerics;

namespace MathEngine.Values;

public class ExponentValue(Value vBase, Value exp) : Value
{
	public readonly Value Base = vBase.Simplify();
	public readonly Value Exponent = exp.Simplify();
	public override Complex Approximate()
	{
		return Complex.Pow(Base.Approximate(), Exponent.Approximate());
	}

	public override Value Simplify()
	{
		if (
			ConstantValue.Zero.Equals(Exponent) && 
			!ConstantValue.Zero.Equals(Base)
		) return new ConstantValue(1);

		// integral exponent, rational base (no room for precision loss)
		if (Exponent is ConstantValue { IsMadeOfIntegralParts: true }) 
		{
			if (Base is ConstantValue { IsMadeOfIntegralParts: true }) return new ConstantValue(Complex.Pow(Base.Approximate(), Exponent.Approximate()));

			if (Base is RatioValue { ContainsOnlyRationalParts: true } ratio)
			{
				return new RatioValue( // "distribute" the exponent
					new ExponentValue(ratio.Numerator, Exponent),
					new ExponentValue(ratio.Denominator, Exponent)
				).Simplify();
			}
		}
		
		return new ExponentValue(Base, Exponent);
	}

	public override string ToString()
	{
		return $"({Base})^({Exponent})";
	}
}