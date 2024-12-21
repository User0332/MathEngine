using ExtendedNumerics;
using MathEngine.Algebra.Expressions;
using MathEngine.Values;

namespace MathEngine.Algebra;

public sealed class Variable(char ident, string subscript = "") : Term
{
	public readonly string FullName = ident+subscript;
	public readonly char Name = ident;
	public readonly string Subscript = subscript;

	public override string ToString()
	{
		if (Subscript != string.Empty) return $"{Name}_{Subscript}";

		return Name.ToString();
	}
}
