using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Solver.GenericSolutions;

internal readonly struct IntervalOnZ
{
	public readonly IntegerValue? LowerBound { get; init; }
	public readonly IntegerValue? UpperBound { get; init; }
}