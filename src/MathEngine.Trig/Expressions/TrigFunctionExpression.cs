using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Simplification;
using MathEngine.Functions;
using MathEngine.Trig.Functions;

namespace MathEngine.Trig.Expressions;

internal sealed class TrigFunctionExpression(string funcName, Expression arg) : FunctionExpression(funcName, [arg])
{
	public override Expression Simplify(SavedSimplificationInfo? info)
	{
		return FuncName switch
		{
			"sin" => TrigFunctions.Sin.ValueAt(Args[0]),
			"cos" => TrigFunctions.Cos.ValueAt(Args[0]),
			"tan" => TrigFunctions.Tan.ValueAt(Args[0]),
			"csc" => TrigFunctions.Csc.ValueAt(Args[0]),
			"sec" => TrigFunctions.Sec.ValueAt(Args[0]),
			"cot" => TrigFunctions.Cot.ValueAt(Args[0]),
			_ => throw new NotImplementedException($"unknown trig function {FuncName}"),
		};
	}

	public override Expression Substitute(Variable var, Expression val)
	{
		return new TrigFunctionExpression(FuncName, Args[0].Substitute(var, val));
	}

	public override string Repr()
	{
		return "Trig"+base.Repr();
	}
}