using MathEngine.Values.Arithmetic;

namespace MathEngine.Values;


public abstract class Value
{
	public abstract BigComplex Approximate();
	public abstract override string ToString();
	public virtual Value Simplify() => this;
	public Value Reciprocal() => new ReciprocalValue(this);
	public Value Negated() => new NegatedValue(this);
	public Value MultiplyBy(Value other) => new ProductValue(this, other);
	public Value AddTo(Value other) => new SumValue(this, other);
}