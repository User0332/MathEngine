using MathEngine.Algebra.Expressions;
using MathEngine.Functions;

namespace MathEngine.Trig.Functions;

internal abstract class BaseTrigFunction : SingleVariableFunction
{
	readonly Dictionary<Expression, Expression> exactValueCache = [];

	public abstract bool TryCalculateExactValue(Expression x, out Expression y);

	// TODO: FOR ApplyApprox, use power series expansion for trig functions (maybe until 10th degree)
	public override Expression ValueAt(Expression x)
	{
		x = x.Simplify();

		if (exactValueCache.TryGetValue(x, out var value)) return value;

		if (TryCalculateExactValue(x, out var y))
		{
			return exactValueCache[x] = y;
		}
		
		throw new ArgumentException($"Cannot calculate exact value of trignometric function at point {x}");
	}
}
