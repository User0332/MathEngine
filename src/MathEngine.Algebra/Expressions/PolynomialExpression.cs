using System.Collections.Immutable;
using MathEngine.Values;
using MathEngine.Values.Arithmetic;
using MathEngine.Values.Real.IrrationalValues;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Expressions;

public class PolynomialExpression(params ImmutableArray<Factor> factors) : Expression(ValidateFactors(factors))
{
	static ImmutableArray<Factor> ValidateFactors(ImmutableArray<Factor> factors) // always returns true
	{
		Variable? usingVar = null;

		foreach (var factor in factors)
		{
			foreach (var term in factor.Terms)
			{
				ValidateTerm(term, ref usingVar);
			}
		}

		return factors;
	}

	static void ValidateTerm(Value term, ref Variable? usingVar) // always returns true
	{
		if (term is ProductValue product)
		{
			ValidateTerm(product.Left, ref usingVar);
			ValidateTerm(product.Right, ref usingVar);

			return;
		}

		if (term is NthPowerOf power)
		{
			if (power.Base is not Variable var) return; // todo: fix: this would pass a term like (2x+1)**1.42, maybe make a bool UsesVariable() method
			
			if (power.Power is not IntegerValue) throw new ArgumentException("non-integral power used in polynomial expression");

			if (usingVar is null) usingVar = var;
			else if (usingVar != var) throw new ArgumentException("multiple variables used in polynomial expression");
		}
	}
}