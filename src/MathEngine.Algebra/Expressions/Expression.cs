using System.Collections.Immutable;
using MathEngine.Values;

namespace MathEngine.Algebra.Expressions;

public class Expression(params ImmutableArray<Factor> factors)
{
	public readonly ImmutableArray<Factor> Factors = factors;
}