using MathEngine.CalcPlugin;

namespace MathEngine.Calculus.Univariate.Limits;

public class LimitSolver
{
	public List<IExpressionManipulationInfo> exprManipulators = [];

	public void AddPlugin<T>() where T: IExpressionManipulationInfo, new()
	{
		if (IsUsingPlugin<T>()) return;
		exprManipulators.Add(new T());
	}

	public void RemovePlugin<T>() where T: IExpressionManipulationInfo
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

	public bool IsUsingPlugin<T>() where T: IExpressionManipulationInfo
	{
		return exprManipulators.Any(x => x is T);
	}
}