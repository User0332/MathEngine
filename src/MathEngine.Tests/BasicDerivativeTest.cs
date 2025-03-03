using MathEngine.Algebra;
using MathEngine.Calculus.Univariate.Differential;
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

		var func = TrigFunctions.Sin.ValueAt(x^2);

		var derivative = differentiator.Differentiate(func, x).Simplify();

		Console.WriteLine(derivative);
	}
}