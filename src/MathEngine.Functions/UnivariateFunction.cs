using ExtendedNumerics;
using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;

namespace MathEngine.Functions;

public abstract class UnivariateFunction : Function
{
	public override sealed Expression ValueAt(Expression[] args)
	{
		if (args.Length != 1) throw new ArgumentException("Single variable function expects one argument");

		return ValueAt(args[0]);
	}

	
	public override sealed BigComplex Approximate(BigComplex[] args)
	{
		if (args.Length != 1) throw new ArgumentException("Single variable function expects one argument");

		return Approximate(args[0]);
	}


	public abstract Expression ValueAt(Expression args);
	public abstract BigComplex Approximate(BigComplex args);

	public static UnivariateFunction FromExpression(Expression expr, Variable var)
	{
		return new UnivariateExprFunc(expr, var);
	}
}

class UnivariateExprFunc(Expression funcExpr, Variable var) : UnivariateFunction
{
	readonly Expression expr = funcExpr;
	readonly Variable var = var;

	public override BigComplex Approximate(BigComplex arg)
	{
		throw new Exception("Approximation is currently unsupported for expression functions");
	}

	public override Expression ValueAt(Expression arg)
	{
		return expr.SubstituteVariable(var, arg);
	}
}