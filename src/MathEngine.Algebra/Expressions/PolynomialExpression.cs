using System.Collections.Immutable;
using MathEngine.Algebra.Expressions.Terms;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Expressions;

public class PolynomialExpression(ImmutableArray<Term> terms) : Expression(ValidateTerms(terms))
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
		if (term is NthPowerOf power)
		{
			Variable? innerVar = null;

			ValidateTerm(power.Base, ref innerVar);

			if (innerVar is null) return; // no variable used in base

			if (usingVar is null) usingVar = innerVar;
			else if (innerVar != usingVar) throw new ArgumentException("multiple variables used in polynomial expression");

			if (power.Power is not ValueTerm valueTerm) throw new ArgumentException("non-constant power used in polynomial expression");

			if (valueTerm.Inner is not IntegerValue) throw new ArgumentException("non-integral power used in polynomial expression");

			return;
		}

		if (term is Variable varTerm)
		{
			if (usingVar is null) usingVar = varTerm;
			else if (usingVar != varTerm) throw new ArgumentException("multiple variables used in polynomial expression");

			return;
		}

		if (term is ValueTerm) return;

		if (term is ProductTerm productTerm)
		{
			foreach (var expr in productTerm.Inner.Factors)
			{
				foreach (var innerTerm in expr.Terms)
				{
					ValidateTerm(innerTerm, ref usingVar);
				}
			}
		}
	}
}