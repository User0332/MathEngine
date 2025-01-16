// class merely to denote undefined values

using MathEngine.Algebra.Expressions;

internal class UndefinedExpression : Expression
{
	public override bool Equals(Expression? other) => other is UndefinedExpression;
	public override int GetHashCode() => 917248130;
	public override string LaTeX() => "NaN";
	public override string Repr() => "undefined";
	public override string ToString() => "undefined";
}