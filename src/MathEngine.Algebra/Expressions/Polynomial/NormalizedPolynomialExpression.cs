using System.Collections.Immutable;
using MathEngine.Algebra.Expressions.Operational;

namespace MathEngine.Algebra.Expressions.Polynomial;

public sealed class NormalizedPolynomialExpression : PolynomialExpression
{
	public readonly ImmutableArray<Expression> NormalizedTerms;

	internal NormalizedPolynomialExpression(Expression[] terms) : base(SumExpression.FromTerms(terms))
	{
		NormalizedTerms = ImmutableArray.Create(terms);
	}

	public Expression GetTermOfDegree(int degree)
	{
		return NormalizedTerms[^(degree+1)];
	}

	public static new NormalizedPolynomialExpression From(Expression expr)
	{
		return PolynomialExpression.From(expr).Normalize();
	}
}