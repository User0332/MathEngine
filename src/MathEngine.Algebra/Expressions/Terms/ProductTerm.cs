using MathEngine.Values;

namespace MathEngine.Algebra.Expressions.Terms;

public sealed class ProductTerm(Product inner) : Term
{
	public readonly Product Inner = inner;


	public static implicit operator ProductTerm(Product prod) => new(prod);

	public override string ToString()
	{
		return Inner.ToString();
	}
}