using MathEngine.Values.Real;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Values.Arithmetic;

public class NegatedValue(Value inner) : Value
{
	public readonly Value InnerValue = inner.Simplify();

	public override BigComplex Approximate()
	{
		return -InnerValue.Approximate();
	}

	public override Value Simplify()
	{
		if (InnerValue is RealValue real)
		{
			if (real is RationalValue rat)
			{
				return new RationalValue(rat.InnerValue*-1);
			}
		}

		if (InnerValue is NegatedValue inner) return inner.InnerValue; // the nested negates cancel out

		return this;
	}

	public override string ToString()
	{
		return $"-({InnerValue})";
	}
}