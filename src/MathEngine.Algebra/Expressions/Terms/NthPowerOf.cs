using MathEngine.Algebra.Expressions.Terms;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Expressions.Terms;

public class NthPowerOf(Term baseNum, Term n) : Term
{
	public readonly Term Base = baseNum;
	public readonly Term Power = n;

	// bool CanBeSimplifiedViaRationals =>
	// 	(Power is IntegerValue pwr) &&
	// 	(
	// 		Base is RationalValue ||
	// 		(
	// 			Base is IntegerValue baseInt &&
	// 			baseInt.InnerValue != 0 &&
	// 			(pwr.InnerValue == 0)
	// 		)
	// 	);

	// public override BigComplex Approximate()
	// {
	// 	return new(
	// 		new BigDecimal(
	// 			Math.Pow(
	// 				(double) Base.Approximate().Real,
	// 				(double) Power.Approximate().Real
	// 			)
	// 		)
	// 	);
	// }

	// public override RealValue Simplify()
	// {
	// 	if (!CanBeSimplifiedViaRationals) return this;

	// 	var baseRational = (RationalValue) Base;
	// 	var powerIntegral = (IntegerValue) Power;

	// 	return new RationalValue(Rational.Pow(baseRational.InnerValue, (int) powerIntegral.InnerValue)).Simplify();
	// }

	public override string ToString()
	{
		return $"({Base})^({Power})";
	}
}