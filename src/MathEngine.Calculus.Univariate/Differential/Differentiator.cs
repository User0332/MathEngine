using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
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
		if (expression is ProductExpression prodExpr) return DifferentiateProduct(prodExpr, wrt);
		if (expression is QuotientExpression quotExpr) return DifferentiateQuotient(quotExpr, wrt);

		if (expression is SumExpression sumExpr)
		{
			return SumExpression.FromTerms(
				[..sumExpr.ToTerms().Select(term => Differentiate(term, wrt))]
			);
		}

		return Expression.Undefined;
	}
	

	Expression DifferentiateProduct(ProductExpression expression, Variable wrt)
	{
		var f = expression.Left;
		var g = expression.Right;

		bool fIsFunc = f.ContainsVariable(wrt);
		bool gIsFunc = g.ContainsVariable(wrt);

		if (fIsFunc && gIsFunc) // product rule
		{
			return Differentiate(f, wrt) * g + f * Differentiate(g, wrt);
		}
		else if (fIsFunc) // const. mult. rule [g*f(x)]
		{
			return g * Differentiate(f, wrt);
		}
		else // const. mult rule [f*g(x)]
		{
			return f * Differentiate(g, wrt);
		}
	}

	Expression DifferentiateQuotient(QuotientExpression expression, Variable wrt)
	{
		var f = expression.Numerator;
		var g = expression.Denominator;

		bool fIsFunc = f.ContainsVariable(wrt);
		bool gIsFunc = g.ContainsVariable(wrt);

		if (fIsFunc && gIsFunc) // quotient rule
		{
			return (Differentiate(f, wrt) * g - f * Differentiate(g, wrt)) / (g ^ 2);
		}
		else if (fIsFunc) // const. mult. rule [1/g * f(x)]
		{
			return 1/g * Differentiate(f, wrt);
		}
		else // const. mult rule [f/g(x))]
		{
			return f * Differentiate(1/g, wrt);
		}
	}
}
