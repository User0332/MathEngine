using MathEngine.Algebra.Expressions;

namespace MathEngine.Algebra.Equations;

public class Equation(Expression lhs, Expression rhs)
{
	public readonly Expression LeftSide = lhs;
	public readonly Expression RightSide = rhs;
}