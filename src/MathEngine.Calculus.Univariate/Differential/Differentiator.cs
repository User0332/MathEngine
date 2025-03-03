using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Algebra.Expressions.Polynomial;
using MathEngine.CalcPlugin;
using MathEngine.Functions;

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
		expression = expression.Simplify();

		if (expression is ProductExpression prodExpr) return DifferentiateProduct(prodExpr, wrt);
		if (expression is QuotientExpression quotExpr) return DifferentiateQuotient(quotExpr, wrt);

		if (expression is SumExpression sumExpr)
		{
			return SumExpression.FromTerms(
				[..sumExpr.ToTerms().Select(term => Differentiate(term, wrt))]
			);
		}

		if (expression == wrt) return Expression.One; // slope

		if (expression is PowerExpression powerExpr)
		{
			var f = powerExpr.Base;
			var n = powerExpr.Exponent;

			var fIsFunc = f.ContainsVariable(wrt);
			var nIsFunc = n.ContainsVariable(wrt);

			if (fIsFunc && !nIsFunc) // power rule
			{
				if (n.Simplify() == Expression.Zero) return Expression.Zero;

				return n * (f ^ (n - 1)) * Differentiate(f, wrt);
			}
			else if (!fIsFunc && nIsFunc)
			{
				// derivative of exponential function, need library with natural log, exp func, etc. [unrelated to this specific case, but we also need a library that contains the DerivativeInfo for these functions]
			}
			else if (fIsFunc && nIsFunc)
			{
				// TODO
			}
			else // constant rule
			{
				return Expression.Zero;
			}
		}

		if (expression is FunctionExpression funcExpr)
		{
			Expression? fPrime = null;

			if (funcExpr.Args.Length != 1) throw new ArgumentException("all functions must be univariate");

			foreach (var differentiator in differentiators)
			{
				if (
					differentiator.TryGetWrtLinearArgument(
						new FunctionExpression(funcExpr.FuncName, [wrt]),
						wrt,
						out fPrime
					)
				) break;
			}

			if (fPrime is null) throw new ArgumentException($"function {funcExpr.FuncName} either unknown or non-differentiable");


			return fPrime*Differentiate(funcExpr.Args[0], wrt); // chain rule
		}

		throw new ArgumentException($"unable to take derivative of {expression} wrt {wrt}");
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
