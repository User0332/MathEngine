using ExtendedNumerics;
using MathEngine.Algebra.Expressions;

namespace MathEngine.Functions;

public abstract class SingleVariableFunction : Function
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
}