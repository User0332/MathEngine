using System.Collections.Immutable;

namespace MathEngine.Algebra.Expressions;

public sealed class Product(ImmutableArray<Expression> factors)
{
	public readonly ImmutableArray<Expression> Factors = factors;

	public static implicit operator Product(Expression expr) => new([ expr ]);
	public static implicit operator Product(ImmutableArray<Expression> factors) => new(factors);

	public override string ToString()
	{
		return $"({string.Join(")(", Factors)})";
	}
}