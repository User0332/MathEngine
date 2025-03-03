using MathEngine.Calculus.Univariate.Differential;
using MathEngine.Trig.CalcPlugin;

namespace MathEngine.Tests;

public static class BasicDerivativeTest
{
	public static void Run()
	{
		Differentiator differentiator = new();
		
		differentiator.AddPlugin<TrignometricDerivativeInfo>();

		
	}
}