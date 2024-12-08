using System.Numerics;

namespace MathEngine.Values;

public class RatioValue : Value
{
	public Value Numerator { get; protected set; } // made a property solely to support ConstantValue & SpecialConstant Functionality
	public readonly Value Denominator;

	public readonly bool ContainsOnlyRationalParts;

	public RatioValue(Value numer, Value denom)
	{
		Numerator = numer.Simplify();
		Denominator = denom.Simplify();

		if (ConstantValue.Zero.Equals(Denominator)) throw new DivideByZeroException();

		ContainsOnlyRationalParts = (
			(
				Numerator is ConstantValue { IsMadeOfIntegralParts: true } ||
				Numerator is RatioValue { ContainsOnlyRationalParts: true }
			) &&
			(
				Denominator is ConstantValue { IsMadeOfIntegralParts: true } || 
				Denominator is RatioValue { ContainsOnlyRationalParts: true }
			)	
		);
	}

	public override Complex Approximate()
	{
		return Numerator.Approximate()/Denominator.Approximate();
	}

	public override Value Simplify()
	{
		// edge cases
		if (ConstantValue.One.Equals(Denominator)) return Numerator;
		if (Denominator.Equals(Numerator)) return ConstantValue.One;

		// remove complex fractions [check for denom also]
		if (Numerator is RatioValue && Numerator is not ConstantValue && Numerator is not SpecialConstant)
		{
		}

		// try to find a common denominator if both are integral (yes, common denominators can be found otherwise, but focus on integers for this case)

		if (Numerator is ConstantValue { IsMadeOfIntegralParts: true } numer &&
			Denominator is ConstantValue { IsMadeOfIntegralParts: true } denom)
		{
		}

		return this;
	}

	public override string ToString()
	{
		return $"({Numerator})/({Denominator})";
	}
}