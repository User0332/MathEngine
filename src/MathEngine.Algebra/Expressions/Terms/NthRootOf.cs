// NOTE: CODE IS KEPT HERE SOLELY FOR INTEGRAL ROOT SIMPLICFICATION LATER ON
// -------------------------------------------------------------------------
// -------------------------------------------------------------------------
// -------------------------------------------------------------------------
// -------------------------------------------------------------------------


// using MathEngine.Values.Real.RationalValues;
// using Rationals;

// namespace MathEngine.Algebra.Expressions.Terms;

// // TODO: fix 
// public class NthRootOf(Term baseNum, Term n)
// 	: NthPowerOf(baseNum, n.Reciprocal())
// {
// 	public readonly RealValue Root = n.Simplify();

// 	public override BigComplex Approximate()
// 	{
// 		return new(
// 			new BigDecimal(
// 				Math.Pow(
// 					(double) Base.Approximate().Real,
// 					1/((double) Root.Approximate().Real)
// 				)
// 			)
// 		);
// 	}

// 	public override RealValue Simplify()
// 	{
// 		if (Root is not IntegerValue || Base is not RationalValue) return this;

// 		int rootNum = (int) ((IntegerValue) Root).InnerValue;
// 		var rationalBase = (RationalValue) Base;

// 		var topRoot = GetNthRoot(rationalBase.InnerValue.Numerator, rootNum);

// 		if (!topRoot.Success) return this;

// 		var botRoot = GetNthRoot(rationalBase.InnerValue.Denominator, rootNum);

// 		if (!botRoot.Success) return this;

// 		return new RationalValue(new(topRoot.Root, botRoot.Root)).Simplify();
// 	}

// 	public override string ToString()
// 	{
// 		return $"({Base})^(1/({Root}))";
// 	}

// 	static (bool Success, BigInteger Root) GetNthRoot(BigInteger baseNum, int n) // bsearch perfect squared-ness
// 	{
// 		if (baseNum < 0) return (false, 0);

// 		BigInteger left = 0, right = baseNum;

// 		while (left <= right)
// 		{
// 			BigInteger mid = (right+left)/2;
// 			BigInteger raised = BigInteger.Pow(mid, n);

// 			if (raised == baseNum) return (true, mid);
// 			else if (raised < baseNum) left = mid+1;
// 			else right = mid-1;
// 		}

// 		return (false, 0);
// 	}
// }