using MathEngine.Algebra.Solver.GenericSolutions;

namespace MathEngine.Algebra.ZeroFinding;

public abstract class ZeroInfo
{
	public required IEnumerable<string> KnownFunctions { get; init; }
	
	/// <summary>
	/// circular dependency issues... should we move powerful simplification & zero finding somewhere else? (like MathEngine.AlgebraExtensions)
	/// </summary>
	/// <returns></returns>
	public IntegralGenericSolution SolveForZeros()
	{
		
	}
}