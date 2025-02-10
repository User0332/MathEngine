namespace MathEngine.Values;


public abstract class Value
{
	public abstract BigComplex Approximate();
	public abstract override string ToString();
	public abstract string LaTeX();
	public virtual Value Simplify() => this;
}