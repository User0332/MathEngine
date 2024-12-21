using MathEngine.Algebra.Expressions;

namespace MathEngine.Algebra.Equations;

public sealed class Equation(Expression lhs, Expression rhs) : BaseEquation<Expression>(lhs, rhs);