using MathEngine.Values.Real;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Values.Real.Arithmetic;

public sealed class RealProductValue(RealValue left, RealValue right) : RealValue
{
	public static readonly RealValue Identity = IntegerValue.One;
	
	public readonly RealValue Left = left.Simplify();
	public readonly RealValue Right = right.Simplify();


	public override BigComplex Approximate()
	{
		return Left.Approximate()*Right.Approximate();
	}

	public override RealValue Simplify()
	{
		return (Left is RationalValue leftRat && Right is RationalValue rightRat) ? new RationalValue(leftRat.InnerValue*rightRat.InnerValue) : this;
	}

	public override string ToString()
	{
		return $"({Left})({Right})";
	}
}