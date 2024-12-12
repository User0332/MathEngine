using System.Collections.Immutable;
using MathEngine.Values;

namespace MathEngine.Algebra.Expressions;

public sealed class Factor(params Value[] terms)
{
	public readonly IEnumerable<Value> Terms = terms.Select(term => term.Simplify());
}