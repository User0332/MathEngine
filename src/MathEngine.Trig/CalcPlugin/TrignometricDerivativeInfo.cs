using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.CalcPlugin;
using MathEngine.Functions;

namespace MathEngine.Trig.CalcPlugin;

public class TrignometricDerivativeInfo : UnivariateDerivativeInfo
{
	protected override bool TryGetDerivative(string funcName, out Expression derivative, Variable wrt)
	{
		if (funcName == "sin")
		{
			derivative = new FunctionExpression("cos", [wrt]);
			return true;
		}

		if (funcName == "cos")
		{
			derivative = new FunctionExpression("sin", [wrt])*-1;
			return true;
		}

		if (funcName == "tan")
		{
			derivative = new FunctionExpression("sec", [wrt])^2;
			return true;
		}

		if (funcName == "csc")
		{
			derivative = new FunctionExpression("csc", [wrt]) * new FunctionExpression("cot", [wrt]) * -1;
			return true;
		}

		if (funcName == "sec")
		{
			derivative = new FunctionExpression("sec", [wrt]) * new FunctionExpression("tan", [wrt]);
			return true;
		}

		if (funcName == "cot")
		{
			derivative = new FunctionExpression("csc", [wrt])^2 * -1;
			return true;
		}

		if (funcName == "asin")
		{
			derivative = 1 / (1 - wrt^2).Sqrt();
			return true;
		}

		if (funcName == "acos")
		{
			derivative = -1 / (1 - wrt^2).Sqrt();
			return true;
		}

		if (funcName == "atan")
		{
			derivative = 1 / (1 + wrt^2);
			return true;
		}

		derivative = default!;

		return false;
	}
}