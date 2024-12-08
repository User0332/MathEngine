using System.Numerics;

namespace MathEngine.Values;

public class BigComplex(BigInteger real, ushort negRealExp, BigInteger imag, ushort negImagExp)
{
	public readonly BigInteger RealMantissa = real;
	public readonly BigInteger ImaginaryMantissa = imag;

	public readonly ushort NegRealExp = negRealExp;
	public readonly ushort NegImaginaryExp = negImagExp;

	public readonly ulong Div10Real = 
	public readonly ulong Div10Imag = 

	public double ApproximateRealValue => 1/BigInteger.Pow(RealMantissa, NegRealExp);
	public double ApproximateImaginaryValue => Math.Pow(ImaginaryMantissa, ImaginaryExp);

	public BigComplex(BigInteger real, short realExp, BigInteger imag, short imagExp)
	{
		var realPlaces = (int) BigInteger.Log10(real);
		var places = (int) BigInteger.Log10(real);
	}

	public byte GetMantissa(BigInteger integer, short realExp)
	{

	}
}