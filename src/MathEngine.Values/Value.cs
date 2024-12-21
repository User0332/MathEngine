namespace MathEngine.Values;


public abstract class Value
{
	public abstract BigComplex Approximate();
	public abstract override string ToString();
	public virtual Value Simplify() => this;
}