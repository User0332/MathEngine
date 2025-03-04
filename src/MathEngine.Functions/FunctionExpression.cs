using System.Collections.Immutable;
using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;

namespace MathEngine.Functions;

public class FunctionExpression(string funcName, Expression[] args) : Expression, IEquatable<FunctionExpression>
{
	public readonly string FuncName = funcName;
	public readonly ImmutableArray<Expression> Args = ImmutableArray.Create(args);

	public override bool Equals(Expression? other)
	{
		return Equals(other as FunctionExpression);
	}

	public bool Equals(FunctionExpression? other)
	{
		return
			other is not null &&
			FuncName == other.FuncName &&
			Args.SequenceEqual(other.Args);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(FuncName, Args);
	}

	public override string LaTeX()
	{
		return $"\\operatorname{{ {FuncName} }}({string.Join(", ", Args.Select(arg => arg.LaTeX()))})";
	}

	public override string Repr()
	{
		string argsRepr = $"[\n  {string.Join(", ", Args.Select(arg => arg.Repr().Replace("\n", "\n  ")))}\n]";
		return $"FunctionExpression(\n  \"{FuncName}\",\n  {argsRepr.Replace("\n", "\n  ")}\n)";
	}

	public override string ToString()
	{
		return $"{FuncName}({string.Join(", ", Args)})";
	}

	public override bool Equals(object? obj)
	{
		return Equals(obj as FunctionExpression);
	}

	public override Expression Substitute(Variable var, Expression val)
	{
		return new FunctionExpression(FuncName, [..Args.Select(arg => arg.Substitute(var, val))]);
	}

	public override bool ContainsVariable()
	{
		return Args.Any(arg => arg.ContainsVariable());
	}

	public override bool ContainsVariable(Variable testFor)
	{
		return Args.Any(arg => arg.ContainsVariable(testFor));
	}
}