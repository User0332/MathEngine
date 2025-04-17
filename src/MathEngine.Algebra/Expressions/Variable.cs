using ExtendedNumerics;
using MathEngine.Algebra.Expressions;
using MathEngine.Values;

namespace MathEngine.Algebra;

public sealed class Variable(char ident, string subscript = "") : Expression, IEquatable<Variable>
{
	public static readonly Variable X = new('x');
	public static readonly Variable Y = new('y');
	public static readonly Variable Z = new('z');
	public static readonly Variable T = new('t');
	public static readonly Variable U = new('u');
	public static readonly Variable V = new('v');

	/// <summary>
	/// Usage of this field (or another Variable object whose IsRestricted attribute is true) in user code will result in undefined behavior
	/// </summary>
	public static readonly Variable InternalRestricted = new('@', "internalrestricted");
	
	public readonly string FullName = ident+subscript;
	public readonly char Name = ident;
	public readonly string Subscript = subscript;

	public readonly bool IsRestricted = ident == '@' && subscript.StartsWith("internalrestricted");

	public override string ToString()
	{
		if (Subscript != string.Empty) return $"{Name}_{Subscript}";

		return Name.ToString();
	}

	public override string LaTeX()
	{
		if (Subscript != string.Empty) return $"{Name}_{Subscript}";

		return Name.ToString();
	}
	
	
	public override int GetHashCode()
	{
		return HashCode.Combine(Name, Subscript);
	}

	public bool Equals(Variable? other)
	{
		return Name == other?.Name && Subscript == other?.Subscript;
	}

	public override bool Equals(object? obj)
	{
		return Equals(obj as Variable);
	}

	public override bool Equals(Expression? other)
	{
		return Equals(other as Variable);
	}

	public override string Repr()
	{
		return ToString();
	}

	public override Expression Substitute(Variable var, Expression val)
	{
		return val;
	}

	public override bool ContainsVariable()
	{
		return true;
	}

	public override bool ContainsVariable(Variable testFor)
	{
		return this == testFor;
	}

	public static bool operator ==(Variable? left, Variable? right)
	{
		return left?.Equals(right) ?? right is null;
	}

	public static bool operator !=(Variable? left, Variable? right)
	{
		return !(left == right);
	}
}
