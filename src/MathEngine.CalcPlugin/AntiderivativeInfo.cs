using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Functions;

namespace MathEngine.CalcPlugin;

public abstract class UnivariateAntiderivativeInfo
{
	public bool TryGetWrtLinearArgument(FunctionExpression function, Variable wrt, out Expression antiderivative)
	{
		if (function.Args.Length != 1)
		{
			antiderivative = default!;
			return false;
		}
		
		var arg = function.Args[0];

		if (arg != wrt)
		{
			antiderivative = default!;
			return false;
		}

		return TryGetAntiderivative(function.FuncName, wrt, out antiderivative);
	}

	protected abstract bool TryGetAntiderivative(string funcName, Variable wrt, out Expression antiderivative);
}
