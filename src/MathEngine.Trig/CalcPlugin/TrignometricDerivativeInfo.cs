using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.CalcPlugin;
using MathEngine.Functions;
using MathEngine.Trig.Expressions;

namespace MathEngine.Trig.CalcPlugin;

public class TrignometricDerivativeInfo : UnivariateDerivativeInfo
{
	protected override bool TryGetDerivative(string funcName, Variable wrt, out Expression derivative)
	{
		if (funcName == "sin")
		{
			derivative = new TrigFunctionExpression("cos", wrt);
			return true;
		}

		if (funcName == "cos")
		{
			derivative = new TrigFunctionExpression("sin", wrt)*-1;
			return true;
		}

		if (funcName == "tan")
		{
			derivative = new TrigFunctionExpression("sec", wrt)^2;
			return true;
		}

		if (funcName == "csc")
		{
			derivative = new TrigFunctionExpression("csc", wrt) * new TrigFunctionExpression("cot", wrt) * -1;
			return true;
		}

		if (funcName == "sec")
		{
			derivative = new TrigFunctionExpression("sec", wrt) * new TrigFunctionExpression("tan", wrt);
			return true;
		}

		if (funcName == "cot")
		{
			derivative = new TrigFunctionExpression("csc", wrt)^2 * -1;
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