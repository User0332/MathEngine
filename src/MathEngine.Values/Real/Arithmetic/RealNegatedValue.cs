using MathEngine.Values.Real;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Values.Real.Arithmetic;

public sealed class RealNegatedValue(RealValue inner) : RealValue
{
	public static readonly RealValue Identity = IntegerValue.Zero;

	public readonly RealValue InnerValue = inner.Simplify();

	public override BigComplex Approximate()
	{
		return -InnerValue.Approximate();
	}

	public override RealValue Simplify()
	{
		if (InnerValue is RealValue real)
		{
			if (real is RationalValue rat)
			{
				return new RationalValue(rat.InnerValue*-1);
			}
		}

		if (InnerValue is RealNegatedValue inner) return inner.InnerValue; // the nested negates cancel out

		return this;
	}

	public override string ToString()
	{
		return $"-({InnerValue})";
	}
}