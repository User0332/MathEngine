// class merely to denote undefined values

using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;

internal class UndefinedExpression : Expression
{
	public override bool Equals(Expression? other) => other is UndefinedExpression;
	public override int GetHashCode() => 917248130;
	public override string LaTeX() => "NaN";
	public override string Repr() => "undefined";

	public override Expression Substitute(Variable var, Expression val)
	{
		return this;
	}
	
	public override bool ContainsVariable()
	{
		return false;
	}

	public override bool ContainsVariable(Variable testFor)
	{
		return false;
	}

	public override string ToString() => "undefined";
}