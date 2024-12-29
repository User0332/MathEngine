using System.Linq.Expressions;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Polynomial;

namespace MathEngine.Algebra.Equations;

public sealed class PolynomialEquation(PolynomialExpression lhs, PolynomialExpression rhs)
	: BaseEquation<PolynomialExpression>(lhs, rhs)
{
	public PolynomialEquation SetZeroSide() // sets LHS to a PolynomialExpression and RHS to 0
	{
		return this;
	}
}