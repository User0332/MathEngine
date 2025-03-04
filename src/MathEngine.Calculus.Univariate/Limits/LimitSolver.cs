using MathEngine.CalcPlugin;

namespace MathEngine.Calculus.Univariate.Limits;

public class LimitSolver
{
	public List<ExpressionManipulationInfo> exprManipulators = [];

	public void AddPlugin<T>() where T: ExpressionManipulationInfo, new()
	{
		if (IsUsingPlugin<T>()) return;
		exprManipulators.Add(new T());
	}

	public void RemovePlugin<T>() where T: ExpressionManipulationInfo
	{
		foreach (var plugin in exprManipulators)
		{
			if (plugin is T)
			{
				exprManipulators.Remove(plugin);
				return;
			}
		}
	}

	public bool IsUsingPlugin<T>() where T: ExpressionManipulationInfo
	{
		return exprManipulators.Any(x => x is T);
	}
}