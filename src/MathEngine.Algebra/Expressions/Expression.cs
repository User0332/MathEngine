using System.Collections.Immutable;

namespace MathEngine.Algebra.Expressions;

public class Expression(params ImmutableArray<Term> terms)
{
	public readonly ImmutableArray<Term> Factors = terms;

	public static implicit operator Expression(Term term) => new([ term ]);
	public static implicit operator Expression(Product prod) => new([ prod ]);
}