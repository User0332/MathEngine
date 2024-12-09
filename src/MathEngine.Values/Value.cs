using MathEngine.Values.Arithmetic;
using MathEngine.Values.Real;

namespace MathEngine.Values;


public abstract class Value
{
	public abstract BigComplex Approximate();
	public abstract override string ToString();
	public virtual Value Simplify() => this;
	public Value Reciprocal() => new ReciprocalValue(this);
	public Value Negated() => new NegatedValue(this);
}