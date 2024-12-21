using System.Collections.Immutable;
using MathEngine.Algebra.Expressions.Terms;

namespace MathEngine.Algebra.Expressions;

public class Expression(ImmutableArray<Term> terms, bool isNegated = false, bool IsReciprocal = false)
{
	public readonly ImmutableArray<Term> Terms = terms;
	public readonly bool IsNegated = isNegated;
	public readonly bool IsReciprocal = IsReciprocal;

	public static implicit operator Expression(Product prod) => new([ (ProductTerm) prod ]);

	public Expression Negate() => new(Terms, !IsNegated, IsReciprocal);
	public Expression Reciprocal() => new(Terms, IsNegated, !IsReciprocal);

	public override string ToString()
	{
		return $"({string.Join(")(", Terms)})";
	}
}