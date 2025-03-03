using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.CalcPlugin;

namespace MathEngine.Calculus.Univariate.Differential;

public sealed class Differentiator
{
	public List<UnivariateDerivativeInfo> differentiators = [];

	public void AddPlugin<T>() where T: UnivariateDerivativeInfo, new()
	{
		if (IsUsingPlugin<T>()) return;
		differentiators.Add(new T());
	}

	public void RemovePlugin<T>() where T: UnivariateDerivativeInfo
	{
		foreach (var plugin in differentiators)
		{
			if (plugin is T)
			{
				differentiators.Remove(plugin);
				return;
			}
		}
	}

	public bool IsUsingPlugin<T>() where T: UnivariateDerivativeInfo
	{
		return differentiators.Any(x => x is T);
	}


	public Expression Differentiate(Expression expression, Variable wrt)
	{

		return Expression.Undefined;
	}
}
