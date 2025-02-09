using System.Collections.Immutable;
using MathEngine.Algebra.Expressions;

namespace MathEngine.Functions;

public sealed class FunctionExpression(string funcName, Expression[] args) : Expression, IEquatable<FunctionExpression>
{
	readonly string funcName = funcName;
	readonly ImmutableArray<Expression> args = ImmutableArray.Create(args);

	public override bool Equals(Expression? other)
	{
		return Equals(other as FunctionExpression);
	}

	public bool Equals(FunctionExpression? other)
	{
		return
			other is not null &&
			funcName == other.funcName &&
			args.SequenceEqual(other.args);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(funcName, args);
	}

	public override string LaTeX()
	{
		return $"\\operatorname{{ {funcName} }}({string.Join(", ", args.Select(arg => arg.LaTeX()))})";
	}

	public override string Repr()
	{
		return $"{funcName}({string.Join(", ", args.Select(arg => arg.Repr()))})";
	}

	public override string ToString()
	{
		return $"{funcName}({string.Join(", ", args)})";
	}

	public override bool Equals(object? obj)
	{
		return Equals(obj as FunctionExpression);
	}
}