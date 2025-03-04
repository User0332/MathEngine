using MathEngine.Functions;

namespace MathEngine.Trig.Functions;

public static class TrigFunctions
{
	public static readonly UnivariateFunction Sin = new SineFunction();
	public static readonly UnivariateFunction Cos = new CosineFunction();
	public static readonly UnivariateFunction Tan = new TangentFunction();
	public static readonly UnivariateFunction Csc = new CosecantFunction();
	public static readonly UnivariateFunction Sec = new SecantFunction();
	public static readonly UnivariateFunction Cot = new CotangentFunction();
}