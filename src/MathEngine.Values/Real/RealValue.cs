using System.Numerics;

namespace MathEngine.Values.Real;


// TODO: maybe make everything real-valued for now?
public abstract class RealValue : Value
{
	public abstract bool IsIntegral { get; }
	public abstract bool IsRational { get; }
}