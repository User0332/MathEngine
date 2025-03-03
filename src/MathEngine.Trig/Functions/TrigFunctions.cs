using MathEngine.Functions;

namespace MathEngine.Trig.Functions;

public static class TrigFunctions
{
	public static readonly UnivariateFunction Sin = new SineFunction();
	public static readonly UnivariateFunction Cos = new CosineFunction();
}