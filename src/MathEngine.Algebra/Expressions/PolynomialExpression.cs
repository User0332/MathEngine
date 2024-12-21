using System.Collections.Immutable;
using MathEngine.Values;
using MathEngine.Values.Real.IrrationalValues;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Expressions;

public class PolynomialExpression(params ImmutableArray<Term> terms) : Expression(ValidateTerms(terms))
{
	static ImmutableArray<Term> ValidateTerms(ImmutableArray<Term> terms) // always returns true
	{
		Variable? usingVar = null;

		foreach (var term in terms)
		{
			ValidateTerm(term, ref usingVar);
		}

		if (usingVar is null) throw new ArgumentException("no variable used in polynomial expression");

		return terms;
	}

	static void ValidateTerm(Term term, ref Variable? usingVar) // always returns true
	{
		if (term.IsValueTerm)
		{
			return ;
		}
	

		foreach (var expr in term.Inner.Factors)
		{
			foreach (var innerTerm in expr.Terms)
			{
				ValidateTerm(innerTerm, ref usingVar);
			}
		}



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