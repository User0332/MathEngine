using System.Numerics;

namespace MathEngine.Values.Real;

public class IntegerValue(BigInteger val) : RealValue
{
	public readonly BigInteger InnerValue = val;

	public override bool IsIntegral => true;

	public override bool IsRational => true;

	public override BigComplex Approximate()
	{
		return new(InnerValue, 0);
	}

	public override bool Equals(Value other)
	{
		throw new NotImplementedException();
	}

	public override Value Simplify()
	{
		throw new NotImplementedException();
	}

	public override string ToString()
	{
		throw new NotImplementedException();
	}
}