using MathEngine.Values;

namespace MathEngine.Algebra.Expressions.Terms;

public static class ExpressionConversionExtensions
{
	public static ValueTerm AsTerm(this Value value) => new(value);
	public static Product AsProduct(this Term value) => new([value]);
	public static Expression AsExpression(this Term value) => new([value]);
	public static Product AsProduct(this Value value) => value.AsTerm().AsProduct();
	public static Product AsProduct(this Expression value) => new([value]);
	public static Expression AsExpression(this Value value) => value.AsTerm().AsExpression();
	public static ProductTerm AsTerm(this Expression value) => new(new([value]));
	public static Expression AsExpression(this Product value) => new([value.AsTerm()]);
	public static ProductTerm AsTerm(this Product value) => value;
}
