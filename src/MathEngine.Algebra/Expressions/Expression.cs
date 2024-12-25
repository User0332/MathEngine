namespace MathEngine.Algebra.Expressions;

public abstract class Expression
{
	public abstract override string ToString();
	public virtual Expression Simplify() => this;

	public PolynomialExpression ToPolynomial()
	{
		return new(this);
	}
}