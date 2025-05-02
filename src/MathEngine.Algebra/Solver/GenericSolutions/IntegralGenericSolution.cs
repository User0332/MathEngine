
using System.Collections.Immutable;
using MathEngine.Algebra.Expressions;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Solver.GenericSolutions;

/// <summary>
/// Represents a generic solution set to an equation that can be mathematically expressed by an expression of a single integer parameter.
/// </summary>
public abstract class IntegralGenericSolution
{
	/// <summary>
	/// The solution to the equation represented as an expression containing the generic parameter variable <see cref="GenericParameter"/>
	/// </summary>
	public required Expression Solution { get; init; }
	
	/// <summary>
	/// The <see cref="Variable"/> object representing the generic parameter in <see cref="Solution"/>
	/// </summary>
	public required Variable GenericParameter { get; init; }

	/// <summary>
	/// Intervals of Z which contain possible values for the generic parameter
	/// </summary>
	internal ImmutableArray<IntervalOnZ> Intervals { get; init; }

	// TODO: need a way to represent & restrict the generic parameter (e.g. n for all n in Z)

	/// <summary>
	/// Represents a single <em>arbitrary</em> solution to the equation, where the generic parameter has been substituted for an arbitrary valid value. This property is constant.
	/// </summary>
	public required Expression ConcreteSolution { get; init; }

	/// <summary>
	/// Returns true if this <see cref="IntegralGenericSolution"/> actually represents one single solution. This solution can then be accessed from <see cref="ConcreteSolution"/>.
	/// </summary>
	public bool IsSingleSolution() // we make this a method to make it explicit that this may not be as cheap an operation as a property might imply
	{

	}
}