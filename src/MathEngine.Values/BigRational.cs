using System.Numerics;

namespace MathEngine.Values;

public class BigRational(BigInteger mantissa, ushort negExp)
{
	public readonly BigInteger Mantissa = mantissa;
	public readonly ushort RealExp = negExp;
	public readonly double Div10 = Math.Pow(10, negExp);

	public double ApproximateValue => Mantissa/Div10;

	public BigComplex(BigInteger real, short realExp, BigInteger imag, short imagExp)
	{
		var realPlaces = (int) BigInteger.Log10(real);
		var places = (int) BigInteger.Log10(real);
	}

	public byte GetMantissa(BigInteger integer, short realExp)
	{

	}
}