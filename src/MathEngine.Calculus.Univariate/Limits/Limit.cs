using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;

namespace MathEngine.Calculus.Univariate.Limits;

public sealed class Limit
{
	public enum SolutionResult
	{
		Found,
		Indeterminate,
		UnboundedNegative,
		UnboundedPositive,
		DoesNotExist,
	}
	
	public sealed class LimitBuilder
	{
		readonly Expression func;
		readonly Variable? var;

		internal LimitBuilder(Expression func)
		{
			this.func = func;
		}

		private LimitBuilder(Expression func, Variable var) : this(func)
		{
			this.var = var;
		}

		public LimitBuilder As(Variable var)
		{
			if (this.var is not null) throw new InvalidOperationException("Cannot take limit with multiple variables");

			return new(func, var);
		}

		public Limit Approaches(Expression appr)
		{
			if (var is null) throw new InvalidOperationException("No variable specified in limit!");

			return new(func, var, appr);
		}
	}

	public readonly Expression Func;
	public readonly Variable Variable;
	public readonly Expression Approaches;

	private Limit(Expression func, Variable var, Expression approaches)
	{
		Func = func;
		Variable = var;
		Approaches = approaches;
	}

	public static LimitBuilder Of(Expression func) => new(func);
}