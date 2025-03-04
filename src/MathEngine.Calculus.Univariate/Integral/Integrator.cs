using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.CalcPlugin;
using MathEngine.Functions;

namespace MathEngine.Calculus.Univariate.Integral;

public sealed class Integrator
{
	public List<UnivariateAntiderivativeInfo> integrators = [];

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
