// this class becomes useful when finding limits; it is placed in the algebra library so it can be integrated into operator expressions

namespace MathEngine.Algebra.Expressions;

public sealed class IndeterminateExpression : UndefinedExpression
{
	public enum Form
	{
		ZeroOverZero,
		InfinityOverInfinity,
		ZeroTimesInfinity,
		InfinityMinusInfinity,
		ZeroToTheZero,
		OneToTheInfinity,
		InfinityToTheZero,
	}
	
	public readonly Form IndeterminateFormType;

	internal IndeterminateExpression(Form type) { IndeterminateFormType = type; }

	public override bool Equals(Expression? other) => other is IndeterminateExpression;
	public override int GetHashCode() => 1289378176;
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