using System.Collections.Immutable;
using MathEngine.Algebra.Expressions.Terms;

namespace MathEngine.Algebra.Expressions;

public sealed class Product(ImmutableArray<Expression> factors)
{
	public readonly ImmutableArray<Expression> Factors = factors;

	public static implicit operator Product(Expression expr) => new([ expr ]);
	public static implicit operator Product(ImmutableArray<Expression> factors) => new(factors);
	public static implicit operator Product(Term term) => new([ term ]);

	public Product RemoveNesting()
	{
		Product self = this;

		if (Factors.Length == 1 && Factors[0].Terms.Length == 1 && Factors[0].Terms[0] is ProductTerm productTerm) self = productTerm.Inner.RemoveNesting();

		return new Product([..self.Factors.Select(factor => factor.RemoveNesting())]);
	}

	public override string ToString()
	{
		return $"({string.Join(")(", Factors)})";
	}
}