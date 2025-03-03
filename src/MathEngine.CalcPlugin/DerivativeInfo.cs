using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Functions;

namespace MathEngine.CalcPlugin;

public abstract class UnivariateDerivativeInfo
{
	public bool TryGetWrtLinearArgument(FunctionExpression function, out Expression derivative, Variable wrt)
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

		return TryGetDerivative(function.FuncName, out derivative, wrt);
	}

	protected abstract bool TryGetDerivative(string funcName, out Expression derivative, Variable wrt);
}
