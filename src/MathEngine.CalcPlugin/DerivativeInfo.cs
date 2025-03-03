using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Functions;

namespace MathEngine.CalcPlugin;

public abstract class UnivariateDerivativeInfo
{
	public bool TryGetWrtLinearArgument(FunctionExpression function, Variable wrt, out Expression derivative)
	{
		if (function.Args.Length != 1)
		{
			derivative = default!;
			return false;
		}
		
		var arg = function.Args[0];

		if (arg != wrt)
		{
			derivative = default!;
			return false;
		}

		return TryGetDerivative(function.FuncName, wrt, out derivative);
	}

	protected abstract bool TryGetDerivative(string funcName, Variable wrt, out Expression derivative);
}
