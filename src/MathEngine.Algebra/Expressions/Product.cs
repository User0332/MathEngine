using System.Collections.Immutable;

namespace MathEngine.Algebra.Expressions;

public sealed class Product(params ImmutableArray<Expression> factors)
{
	public readonly ImmutableArray<Expression> Factors = factors;

	public static implicit operator Product(Term term) => new([ term ]);
	public static implicit operator Product(Expression expr) => new([ expr ]);
}