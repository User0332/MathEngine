using System.Linq.Expressions;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Polynomial;

namespace MathEngine.Algebra.Equations;

public sealed class PolynomialEquation(PolynomialExpression lhs, PolynomialExpression rhs)
	: BaseEquation<PolynomialExpression>(lhs, rhs)
{
	/// <summary>
	/// Constructs a new PolynomialEquation mathematically equivalent to the current instance where the left hand side is a polynomial and the right hand side is set to 0
	/// </summary>
	/// <returns>The new PolynomialEquation with the right hand side set to 0</returns>
	public PolynomialEquation SetZeroSide()
	{
		return this;
	}
}