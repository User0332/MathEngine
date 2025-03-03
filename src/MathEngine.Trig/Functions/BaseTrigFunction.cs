using MathEngine.Algebra.Expressions;
using MathEngine.Functions;

namespace MathEngine.Trig.Functions;

internal abstract class BaseTrigFunction : UnivariateFunction
{
	public readonly string Name;
	readonly Dictionary<Expression, Expression> _exactValueCache = [];

	internal BaseTrigFunction(string name) { Name = name; }

	public abstract bool TryCalculateExactValue(Expression x, out Expression y);

	// TODO: FOR ApplyApprox, use power series expansion for trig functions (maybe until 10th degree)
	public override Expression ValueAt(Expression x)
	{
		x = x.Simplify();

		if (_exactValueCache.TryGetValue(x, out var value)) return value;

		if (TryCalculateExactValue(x, out var y))
		{
			return _exactValueCache[x] = y;
		}

		return new FunctionExpression(Name, [ x ]);
	}
}
