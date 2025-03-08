using System.Collections.Immutable;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Expressions.Polynomial;

public sealed class NormalizedPolynomialExpression : PolynomialExpression
{
	public readonly ImmutableArray<ProductExpression> NormalizedTerms;

	internal NormalizedPolynomialExpression(ProductExpression[] terms) : base(terms.Length > 1 ? SumExpression.FromTerms(terms) : terms[0])
	{
		NormalizedTerms = ImmutableArray.Create(terms);
	}

	public ProductExpression GetTermOfDegree(int degree)
	{
		return NormalizedTerms[^(degree+1)];
	}

	/// <summary>
	/// NOTE: This function must ONLY be called with a term from a <see cref="NormalizedPolynomialExpression"/>
	/// </summary>
	/// <param name="term"></param>
	/// <returns></returns>

	public static int DegreeOfNormalizedTerm(ProductExpression term)
	{
		return (int) (
			(IntegerValue) (
				(ValueExpression) (
					(PowerExpression) term.Right
				).Right
			).Inner
		).InnerValue;
	}

	/// <summary>
	/// NOTE: This function must ONLY be called with a term from a <see cref="NormalizedPolynomialExpression"/>
	/// </summary>
	/// <param name="term"></param>
	/// <returns></returns>

	public static Expression CoefficientOfNormalizedTerm(ProductExpression term)
	{
		return term.Left;
	}

	public static new NormalizedPolynomialExpression From(Expression expr)
	{
		return PolynomialExpression.From(expr).Normalize();
	}

	public override NormalizedPolynomialExpression Normalize() => this;
}