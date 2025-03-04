using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.CalcPlugin;
using MathEngine.Functions;

namespace MathEngine.Calculus.Univariate.Integral;

public sealed class Integrator
{
	public List<UnivariateAntiderivativeInfo> integrators = [];
	public List<ExpressionManipulationInfo> exprManipulators = [];

	public void AddPlugin<T>() where T: UnivariateAntiderivativeInfo, new()
	{
		if (IsUsingPlugin<T>()) return;
		integrators.Add(new T());
	}

	public void RemovePlugin<T>() where T: UnivariateAntiderivativeInfo
	{
		foreach (var plugin in integrators)
		{
			if (plugin is T)
			{
				integrators.Remove(plugin);
				return;
			}
		}
	}

	public bool IsUsingPlugin<T>() where T: UnivariateAntiderivativeInfo
	{
		return integrators.Any(x => x is T);
	}

	public void AddManipulationPlugin<T>() where T: ExpressionManipulationInfo, new()
	{
		if (IsUsingManipulationPlugin<T>()) return;
		exprManipulators.Add(new T());
	}

	public void RemoveManipulationPlugin<T>() where T: ExpressionManipulationInfo
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

	public bool IsUsingManipulationPlugin<T>() where T: ExpressionManipulationInfo
	{
		return exprManipulators.Any(x => x is T);
	}

	public Expression Integrate(Expression expression, Variable wrt)
	{
		expression = expression.Simplify();

		throw new ArgumentException($"unable to take antiderivative of {expression} wrt {wrt}");
	}
	

	Expression IntegrateProduct(ProductExpression expression, Variable wrt)
	{
		return Expression.Undefined;
	}
}
