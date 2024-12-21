using System.Collections.Immutable;
using MathEngine.Algebra.Expressions.Terms;

namespace MathEngine.Algebra.Expressions;

public class Expression(ImmutableArray<Term> terms, bool isNegated = false, bool IsReciprocal = false)
{
	public readonly ImmutableArray<Term> Terms = terms;
	public readonly bool IsNegated = isNegated;
	public readonly bool IsReciprocal = IsReciprocal;

	public static implicit operator Expression(Product prod) => new([ (ProductTerm) prod ]);
	public static implicit operator Expression(ImmutableArray<Term> terms) => new(terms);
	public static implicit operator Expression(Term term) => new([term]);

	public Expression Negate() => new(Terms, !IsNegated, IsReciprocal);
	public Expression Reciprocal() => new(Terms, IsNegated, !IsReciprocal);

	public override string ToString()
	{
		return $"({string.Join("+", Terms)})";
	}

	public Expression RemoveNesting()
	{
		Expression self = this;
	
		if (Terms.Length == 1 && Terms[0] is ProductTerm productTerm && productTerm.Inner.Factors.Length == 1)
		{
			self = productTerm.Inner.Factors[0].RemoveNesting();
		}

		return new Expression([..self.Terms.Select(term => term.RemoveNesting())], IsNegated, IsReciprocal);
	}

	public virtual Expression Simplify() => this;

	public PolynomialExpression ToPolynomial()
	{
		return new(Terms);
	}
}