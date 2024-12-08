using System.Numerics;

namespace MathEngine.Values;


// TODO: maybe make everything real-valued for now?
public abstract class Value
{
	public abstract Complex Approximate();
	public abstract Value Simplify();
	public abstract override string ToString();
	public abstract bool Equals(Value other);
}