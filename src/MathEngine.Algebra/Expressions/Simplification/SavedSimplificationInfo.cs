namespace MathEngine.Algebra.Expressions.Simplification;

public class SavedSimplificationInfo
{
	/// <summary>
	/// A database of where the current expression is already known to be undefined
	/// </summary>
	public readonly Dictionary<Variable, Expression> AlreadyUndefinedWhen = [];
}