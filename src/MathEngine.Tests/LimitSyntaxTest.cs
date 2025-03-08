using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Calculus.Univariate.Limits;
using MathEngine.Trig.Functions;

namespace MathEngine.Tests;

public static class LimitSyntaxTest
{
	public static void Run()
	{
		var x = Variable.X;
		var f = TrigFunctions.Sin.AsDelegate();
		
		Limit lim = Limit.Of(f(x)).As(x).Approaches(Expression.PI/2);
	}
}