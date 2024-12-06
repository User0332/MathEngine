using System.Numerics;

namespace MathEngine.Values;

public abstract class Value
{
	public abstract Complex Approximate();
	public abstract Value Simplify();
	public abstract override string ToString();
}