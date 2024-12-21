using MathEngine.Values;

namespace MathEngine.Algebra.Expressions;

public class Term
{
	public readonly Product Inner = null!;
	public readonly bool IsNegated = false;
	public readonly bool IsReciprocal = false;
	public readonly bool IsValueTerm = false;
	public readonly Value InnerValue = null!;

	internal Term() {}

	public Term(Value val, bool negate = false, bool invert = false) : this()
	{
		InnerValue = val;
		IsValueTerm = true;
		IsNegated = negate;
		IsReciprocal = invert;
	}

	public Term(Product inner, bool negate = false, bool invert = false) : this()
	{
		Inner = inner;
		IsNegated = negate;
		IsReciprocal = invert;
	}

	public Term Negate() => new(Inner, !IsNegated, IsReciprocal);
	public Term Reciprocal() => new(Inner, IsNegated, !IsReciprocal);

	public static implicit operator Term(Value val) => new(val);
	public static implicit operator Term(Product prod) => new(prod);
}