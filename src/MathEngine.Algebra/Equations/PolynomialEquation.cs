using System.Linq.Expressions;

namespace MathEngine.Algebra.Equations;

public sealed class PolynomialEquation(Expression rhs, Expression lhs)
	: BaseEquation<Expression>(rhs, lhs);