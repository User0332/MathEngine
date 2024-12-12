using MathEngine.Values.Real;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Values.Real.Arithmetic;

public sealed class RealReciprocalValue(RealValue inner) : RealValue
{
	public static readonly RealValue Identity = IntegerValue.One;

	public readonly RealValue InnerValue = inner.Simplify();

	public override BigComplex Approximate()
	{
		return 1/InnerValue.Approximate();
	}

	public override RealValue Simplify()
	{
		if (InnerValue is RealValue real)
		{
			if (real is RationalValue rat)
			{
				return new RationalValue(1/rat.InnerValue);
			}
		}

		if (InnerValue is RealReciprocalValue inner) return inner.InnerValue; // the nested reciprocals cancel out

		return this;
	}

	public override string ToString()
	{
		return $"1/({InnerValue})";
	}
}