using ExtendedNumerics;
using MathEngine.Values;

namespace MathEngine.Algebra;

public sealed class Variable(char ident, string subscript = "") : Value
{
	public readonly string FullName = ident+subscript;
	public readonly char Name = ident;
	public readonly string Subscript = subscript;

	public override BigComplex Approximate()
	{
		throw new NotImplementedException();
	}

	public override string ToString()
	{
		if (Subscript != string.Empty) return $"{Name}_{Subscript}";

		return Name.ToString();
	}
}
