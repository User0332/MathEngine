using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Algebra.Expressions.Polynomial;

namespace MathEngine.Algebra.Equations;

public sealed class PolynomialEquation : Equation
{
	public readonly new PolynomialExpression LeftSide;
	public readonly new PolynomialExpression RightSide;

	public readonly bool IsNormalized;
	public readonly int Degree;
	public readonly Variable Variable;

	public PolynomialEquation(Expression lhs, Expression rhs)
		: this(lhs.ToPolynomial(), rhs.ToPolynomial()) { }

	public PolynomialEquation(PolynomialExpression lhs, PolynomialExpression rhs) : this(lhs, rhs, false) { }

	private PolynomialEquation(PolynomialExpression lhs, PolynomialExpression rhs, bool isNormalized) : base(lhs, rhs)
	{
		LeftSide = lhs;
		RightSide = rhs;

		IsNormalized = isNormalized;
		Degree = Math.Max(lhs.Degree, rhs.Degree);

		Variable = lhs.Variable;

		if (lhs.Variable != rhs.Variable) throw new ArgumentException("A PolynomialEquation instance may consist of only one variable!");
	}

	/// <summary>
	/// Returns a <see cref="PolynomialEquation"/> mathematically equivalent to the current instance where the left hand side is a <see cref="NormalizedPolynomialExpression"/> and the right hand side is a zero expression
	/// </summary>
	/// <returns>The PolynomialEquation with the right hand side set to 0</returns>
	public PolynomialEquation Normalize()
	{
		if (IsNormalized) return this;

		if (RightSide.Normalize() == PolynomialExpression.ZeroExpr(RightSide.Variable))
		{
			if (LeftSide is NormalizedPolynomialExpression) return new(LeftSide, RightSide, true);

			return new(LeftSide.Normalize(), RightSide, true);
		}

		var negRight = new ProductExpression(Expression.NegativeOne, RightSide.BaseNode); // NOTE: need to use BaseNode because no simplification logic exists for wrapped expressions like PolynomialExpression
		var leftWithRightSubtracted = new SumExpression(LeftSide.BaseNode, negRight);

		return new PolynomialEquation(
			PolynomialExpression.From(leftWithRightSubtracted).Normalize(),
			PolynomialExpression.ZeroExpr(),
			true
		);
	}
}