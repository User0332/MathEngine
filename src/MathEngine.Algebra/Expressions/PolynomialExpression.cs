using System.Collections.Immutable;
using MathEngine.Algebra.Expressions.Terms;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Expressions;

public class PolynomialExpression : Expression
{
	internal PolynomialExpression(Expression baseTerm)
	{

	}

	public static PolynomialExpression From(Expression baseTerm)
	{
		return baseTerm as PolynomialExpression ?? new(baseTerm);
	}

	// public readonly int Degree = terms.Select(DegreeOf).Max();

	// public override PolynomialExpression Simplify()
	// {
	// 	// multiply everything out
	// 	var expandedTerms = new List<Term>();

	// 	foreach (var term in Terms)
	// 	{
	// 		expandedTerms.Add(term.Simplify());
	// 	}
		
	// 	// combine like terms
	// 	var combinedTerms = expandedTerms
	// 		.GroupBy(DegreeOf)
	// 		.Select(group => group.Aggregate((term1, term2) => AddLikeTerms(term1, term2)))
	// 		.ToImmutableArray();

	// 	return new PolynomialExpression(combinedTerms);

	// 	static ProductTerm AddLikeTerms(Term term1, Term term2) // we can make some assumptions here since this function will only ever be called internally when the polynomial expression is split up into terms, each with one variable^power factor each
	// 	{
	// 		var defaultSum = new Expression([term1, term2]).AsProduct();

	// 		if (term1 is ValueTerm valueTerm1 && term2 is ValueTerm valueTerm2 && valueTerm1.Inner is RationalValue rat1 && valueTerm2.Inner is RationalValue rat2)
	// 		{
	// 			return new Product([
	// 				new RationalValue(rat1.InnerValue + rat2.InnerValue).AsTerm()
	// 			]);
	// 		}

	// 		if (term1 is ProductTerm productTerm1 && term2 is ProductTerm productTerm2)
	// 		{
	// 			var product1 = productTerm1.Inner;
	// 			var product2 = productTerm2.Inner;

	// 			if (product1.Factors[0].Terms[0] is ValueTerm valTerm1 && product2.Factors[0].Terms[0] is ValueTerm valTerm2 && valTerm1.Inner is RationalValue prodRat1 && valTerm2.Inner is RationalValue prodRat2)
	// 			{
	// 				return new Product([
	// 					new RationalValue(prodRat1.InnerValue * prodRat2.InnerValue).AsExpression(),
	// 					..product1.Factors // in this form the product should only contain one other value
	// 				]).AsTerm();
	// 			}
	// 		}

	// 		return defaultSum;
	// 	}
	// }

	// public PolynomialExpression Normalize() // sort by degree & put into the coefficient*variable^exp form
	// {
	// 	return new PolynomialExpression([..Terms.OrderByDescending(DegreeOf)]);
	// }

	// internal ProductTerm? GetTermOfDegree(int degree) // this should ONLY be used by a normalized, simplified PolynomialExpression, so no need to sum the terms, there should be only one root term per degree
	// {
	// 	return Terms.FirstOrDefault(term => DegreeOf(term) == degree) as ProductTerm;
	// }

	// public static int DegreeOf(Term term)
	// {
	// 	if (term is NthPowerOf power && power.Base is Variable)
	// 	{
	// 		return (int) ((IntegerValue) ((ValueTerm) power.Power).Inner).InnerValue;
	// 	}

	// 	if (term is Variable) return 1;

	// 	if (term is ValueTerm) return 0;

	// 	if (term is ProductTerm productTerm)
	// 	{
	// 		return productTerm.Inner.Factors.Select(expr => {
	// 			if (expr.IsReciprocal) throw new ArgumentException("Reciprocal terms currently unsupported in polynomial expressions"); // Can fix via Expression.Normalize (invert powers and rationals)

	// 			return expr.ToPolynomial().Degree;
	// 		}).Sum();
	// 	}

	// 	throw new ArgumentException("Invalid term type");
	// }

	static ImmutableArray<Term> ValidateTerms(ImmutableArray<Term> terms) // always returns true
	{
		Variable? usingVar = null;

		foreach (var term in terms)
		{
			ValidateTerm(term, ref usingVar);
		}

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
			else if (innerVar != usingVar) throw new ArgumentException("Multiple variables used in polynomial expression");

			if (power.Power is not ValueTerm valueTerm) throw new ArgumentException("Non-constant power used in polynomial expression");

			if (valueTerm.Inner is not IntegerValue) throw new ArgumentException("Non-integral power used in polynomial expression");

			return;
		}

		if (term is Variable varTerm)
		{
			if (usingVar is null) usingVar = varTerm;
			else if (usingVar != varTerm) throw new ArgumentException("Multiple variables used in polynomial expression");

			return;
		}

		if (term is ValueTerm) return;

		if (term is ProductTerm productTerm)
		{
			foreach (var expr in productTerm.Inner.Factors)
			{
				if (expr.IsReciprocal) throw new ArgumentException("Reciprocal terms currently unsupported in polynomial expressions"); // Can fix via Expression.Normalize (invert powers and rationals)

				foreach (var innerTerm in expr.Terms)
				{
					ValidateTerm(innerTerm, ref usingVar);
				}
			}
		}
	}
}