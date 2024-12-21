using MathEngine.Values;

namespace MathEngine.Algebra.Expressions.Terms;

public abstract class Term
{
	public abstract override string ToString();

	public Term RemoveNesting()
	{
		if (this is ProductTerm productTerm)
		{
			Term self = productTerm;

			if (productTerm.Inner.Factors.Length == 1 && productTerm.Inner.Factors[0].Terms.Length == 1)
			{
				self = productTerm.Inner.Factors[0].Terms[0].RemoveNesting();
			}

			if (self is ProductTerm selfSimpl)
			{
				return new ProductTerm(selfSimpl.Inner.RemoveNesting());
			}
		}

		return this;
	}

	public virtual Term Simplify() => this;
}