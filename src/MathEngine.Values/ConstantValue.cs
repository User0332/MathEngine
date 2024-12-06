using System.Numerics;

namespace MathEngine.Values;

public class ConstantValue : RatioValue
{
	public static readonly ConstantValue Zero = new(Complex.Zero);
	public static readonly ConstantValue One = new(Complex.One);
	public static readonly ConstantValue ImaginaryOne = new(Complex.ImaginaryOne);

	public readonly Complex Value;
	public readonly bool IsMadeOfIntegralParts;
	public readonly bool IsReal;

	public ConstantValue(Complex c) : base(null!, One)
	{
		Value = c;
		IsMadeOfIntegralParts = IsIntegral(c.Imaginary) && IsIntegral(c.Real);
		IsReal = c.Imaginary == 0;

		Numerator = this;
	}

	public override Complex Approximate()
	{
		return Value;
	}

	public override Value Simplify()
	{
		return this;
	}

	public override string ToString()
	{
		return Value.ToString();
	}

	public bool Equals(Value? other)
	{
		return Equals(other?.Simplify() as ConstantValue);
	}

	public bool Equals(ConstantValue? other)
	{
		return other is not null && Value == other.Value;
	}

	static bool IsIntegral(double val) => val % 1 == 0;
}