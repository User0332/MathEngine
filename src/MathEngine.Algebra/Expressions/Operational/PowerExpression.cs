using System.Numerics;
using MathEngine.Values.Real.RationalValues;
using Rationals;

namespace MathEngine.Algebra.Expressions.Operational;

public sealed class PowerExpression(Expression left, Expression right) : OperationExpression(left, right, '^')
{
	public readonly Expression Base = left;
	public readonly Expression Exponent = right;

	public override Expression Simplify()
	{
		var (simplBase, simplExp) = (Base.Simplify(), Exponent.Simplify());

		if (SimplificationUtils.GetRationalValue(simplBase, out var baseRat) && SimplificationUtils.GetRationalValue(simplExp, out var expRat))
		{
			var exp = expRat.InnerValue.CanonicalForm;

			var powed = Rational.Pow(baseRat.InnerValue, (int) exp.Numerator);

			var rootNum = ExactNthRoot(powed.Numerator, (int) exp.Denominator);

			if (rootNum is null) return new PowerExpression((ValueExpression) powed, (ValueExpression) (Rational.One/exp.Denominator));

			var rootDenom = ExactNthRoot(powed.Denominator, (int) exp.Denominator);

			if (rootDenom is null) return new PowerExpression((ValueExpression) powed, (ValueExpression) (Rational.One/exp.Denominator));

			return (ValueExpression) ((Rational) rootNum/rootDenom);
		}

		return new PowerExpression(simplBase, simplExp);
	}

	public static BigInteger? ExactNthRoot(BigInteger value, int n)
	{
		if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n), "Root must be a positive integer.");
		
		if (value < 0 && n % 2 == 0) return null;
		
		if (value == 0) return 0;
		
		if (value == 1 || n == 0) return 1;

		if (n == 1) return value;

		BigInteger low = 1;
		BigInteger high = value;
		BigInteger mid;

		while (low <= high)
		{
			mid = (low + high) / 2;
			BigInteger midPow = BigInteger.Pow(mid, n);

			if (midPow == value) return mid;
			else if (midPow < value) low = mid + 1;
			else high = mid - 1;
		}

		return null;
	}

	public override string ToString()
	{
		string baseRepr, expRepr;

		if (Base is ValueExpression or Variable) baseRepr = Base.ToString(); // only single-char/individually distinguishable values do not need enclosing parentheses
		else baseRepr = $"({Base})";

		if (SimplificationUtils.GetRationalValue(Exponent, out var expRat) && expRat == IntegerValue.One) return baseRepr;

		if (Exponent is ValueExpression or Variable) expRepr = Exponent.ToString();
		else expRepr = $"({Exponent})";

		return $"({baseRepr}^{expRepr})";
	}
}