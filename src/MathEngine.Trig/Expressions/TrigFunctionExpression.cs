using MathEngine.Algebra.Expressions;
using MathEngine.Functions;
using MathEngine.Trig.Functions;

namespace MathEngine.Trig.Expressions;

internal class TrigFunctionExpression(string funcName, Expression arg) : FunctionExpression(funcName, [arg])
{
	public override Expression Simplify()
	{
		return FuncName switch
		{
			"sin" => TrigFunctions.Sin.ValueAt(Args[0]),
			"cos" => TrigFunctions.Cos.ValueAt(Args[0]),
			_ => throw new NotImplementedException($"unknown trig function {FuncName}"),
		};
	}
}