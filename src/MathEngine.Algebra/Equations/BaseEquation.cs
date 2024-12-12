using System.Linq.Expressions;

namespace MathEngine.Algebra.Equations;

public abstract class BaseEquation<TExpression>(TExpression lhs, TExpression rhs) where TExpression : Expression
{
	public readonly TExpression LeftSide = lhs;
	public readonly TExpression RightSide = rhs;
}