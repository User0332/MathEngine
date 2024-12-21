using MathEngine.Values;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Expressions.Terms;

public sealed class ProductTerm(Product inner) : Term
{
	public readonly Product Inner = inner;

	public override Term Simplify()
	{
		List<Term> result = [..Inner.Factors[0].Terms];
		List<Term> workingResult = [];

		for (int i = 1; i < Inner.Factors.Length; i++)
		{
			foreach (var term1 in Inner.Factors[i].Terms)
			{
				foreach (var term2 in result)
				{
					workingResult.Add(MultiplyTerms(term1, term2));
				}
			}

			result = [..workingResult];
			workingResult.Clear();
		}

		var expr = new Expression([..result]).AsTerm();

		return expr.RemoveNesting();

		static Term MultiplyTerms(Term term1, Term term2)
		{
			var defaultProduct = new Product([term1, term2]).AsTerm();

			if (term1 is ValueTerm valueTerm1 && term2 is ValueTerm valueTerm2)
			{
				return valueTerm1.Inner switch
					{
						RationalValue rat1 => valueTerm2.Inner switch
						{
							RationalValue rat2 => new RationalValue(rat1.InnerValue * rat2.InnerValue).AsTerm(),
							_ => defaultProduct
						},
						_ => defaultProduct
					};
			}

			if (term1 is Variable && term2 is Variable && term1.Equals(term2))
			{
				return new NthPowerOf(term1, new IntegerValue(2).AsTerm());
			}

			if (term2 is Variable var2 && term1 is NthPowerOf powerTerm1) // swap for below case
			{
				(term2, term1) = (term1, term2);
			}

			if (term1 is Variable var1 && term2 is NthPowerOf powerTerm2)
			{
				if (var1.Equals(powerTerm2.Base) && powerTerm2.Power is ValueTerm powerValue && powerValue.Inner is RationalValue ratPwr)
				{
					return new NthPowerOf(var1, new RationalValue(ratPwr.InnerValue + 1).AsTerm());
				}
			}

			if (term1 is NthPowerOf pwrTerm1 && term2 is NthPowerOf pwrTerm2)
			{
				if (pwrTerm1.Base is Variable baseVar && baseVar.Equals(pwrTerm2.Base))
				{
					if (pwrTerm1.Power is ValueTerm power1 && pwrTerm2.Power is ValueTerm power2 && power1.Inner is RationalValue ratPwr1 && power2.Inner is RationalValue ratPwr2)
					{
						return new NthPowerOf(baseVar, new RationalValue(ratPwr1.InnerValue+ratPwr2.InnerValue).AsTerm());
					}
				}
			}

			if (term1 is ProductTerm productTerm1 && term2 is ProductTerm productTerm2)
			{
				return new Product([..productTerm1.Inner.Factors, ..productTerm2.Inner.Factors]).AsTerm();
			}

			return defaultProduct;
		}
	}

	public static implicit operator ProductTerm(Product prod) => new(prod);
	public static implicit operator ProductTerm(Expression expr) => new(expr);

	public override string ToString()
	{
		return Inner.ToString();
	}
}