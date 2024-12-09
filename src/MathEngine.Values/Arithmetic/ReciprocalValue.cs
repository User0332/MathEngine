using MathEngine.Values.Real;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Values.Arithmetic;

public class ReciprocalValue(Value inner) : Value
{
	public readonly Value InnerValue = inner.Simplify();

	public override BigComplex Approximate()
	{
		return 1/InnerValue.Approximate();
	}

	public override Value Simplify()
	{
		if (InnerValue is RealValue real)
		{
			if (real is RationalValue rat)
			{
				return new RationalValue(1/rat.InnerValue);
			}
		}

		if (InnerValue is ReciprocalValue inner) return inner.InnerValue; // the nested reciprocals cancel out

		return this;
	}

	public override string ToString()
	{
		return $"1/({InnerValue})";
	}
}