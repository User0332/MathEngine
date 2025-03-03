using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.Calculus.Univariate.Differential;
using MathEngine.Functions;
using MathEngine.Trig.CalcPlugin;
using MathEngine.Trig.Functions;

namespace MathEngine.Tests;

public static class BasicDerivativeTest
{
	public static void Run()
	{
		Variable x = Variable.X;

		Differentiator differentiator = new();
		
		differentiator.AddPlugin<TrignometricDerivativeInfo>();

		var func = TrigFunctions.Sin.ValueAt(x)^2;

		var derivative = differentiator.Differentiate(func, x).Simplify();

		var fPrime = UnivariateFunction.FromExpression(derivative, x);

		Console.WriteLine(derivative);
		Console.WriteLine(fPrime.ValueAt(Expression.PI/2));
		Console.WriteLine(fPrime.ValueAt(Expression.PI/2).Simplify());
	}
}