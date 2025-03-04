using ExtendedNumerics;
using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;

namespace MathEngine.Functions;

public abstract class Function
{
	public abstract Expression ValueAt(Expression[] args);
	public abstract BigComplex Approximate(BigComplex[] args);

	public static Function FromExpression(Expression expr, Variable[] vars)
	{
		return new ExprFunc(expr, vars);
	}
}

class ExprFunc(Expression funcExpr, Variable[] vars) : Function
{
	readonly Expression expr = funcExpr;
	readonly Variable[] vars = vars;

	public override BigComplex Approximate(BigComplex[] args)
	{
		throw new Exception("Approximation is currently unsupported for expression functions");
	}

	public override Expression ValueAt(Expression[] args)
	{
		if (vars.Length != args.Length)
		{
			throw new ArgumentException("Number of arguments does not match number of variables");
		}

		var retExpr = expr;

		foreach (var (var, arg) in vars.Zip(args))
		{
			retExpr = retExpr.Substitute(var, arg);
		}

		return retExpr;
	}
}