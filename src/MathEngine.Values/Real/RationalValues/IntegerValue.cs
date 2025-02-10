using System.Numerics;

namespace MathEngine.Values.Real.RationalValues;

public sealed class IntegerValue(BigInteger val) : RationalValue(val)
{
	public static readonly IntegerValue One = new(1);
	public static readonly IntegerValue Zero = new(0);
	public static readonly IntegerValue NegativeOne = new(-1);
	
	public new readonly BigInteger InnerValue = val;

	public override BigComplex Approximate()
	{
		return new(InnerValue);
	}

	public override string ToString()
	{
		return InnerValue.ToString();
	}

	public override string LaTeX()
	{
		return ToString();
	}
}