using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Functions;

namespace MathEngine.Calculus.Univariate.Integral;

public static class IntegrationApplicationExtensions
{
	public static Expression AreaUnderCurve(this Integrator integrator, Expression func, Expression a, Expression b, Variable wrt)
	{
		var antiDeriv = integrator.Integrate(func, wrt);

		return antiDeriv.Substitute(wrt, b) - antiDeriv.Substitute(wrt, a);
	}

	public static Expression AverageValueOfFunction(this Integrator integrator, Expression func, Expression a, Expression b, Variable wrt)
	{
		return integrator.AreaUnderCurve(func, a, b, wrt) / (b - a);
	}

	public static Expression DiscMethod(this Integrator integrator, Expression func, Expression a, Expression b, Variable wrt)
	{
		return Expression.PI*integrator.AreaUnderCurve(func * func, a, b, wrt);
	}

	
}