using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Values.Arithmetic;

public sealed class SumValue(Value left, Value right) : Value
{
	public static readonly Value Identity = IntegerValue.Zero;

	public readonly Value Left = left.Simplify();
	public readonly Value Right = right.Simplify();


	public override BigComplex Approximate()
	{
		return Left.Approximate()+Right.Approximate();
	}

	public override Value Simplify()
	{
		return (Left is RationalValue leftRat && Right is RationalValue rightRat) ? new RationalValue(leftRat.InnerValue+rightRat.InnerValue) : this;
	}

	public override string ToString()
	{
		return $"({Left})+({Right})";
	}
}