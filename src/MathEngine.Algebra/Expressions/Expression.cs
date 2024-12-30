using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Algebra.Expressions.Polynomial;
using Rationals;

namespace MathEngine.Algebra.Expressions;

public abstract class Expression : IEquatable<Expression>
{
	internal Expression() { } // only allow internal inheritance
	
	public abstract override string ToString();
	public abstract string Repr();
	public virtual Expression Simplify() => this;

	public PolynomialExpression ToPolynomial()
	{
		return PolynomialExpression.From(this);
	}

	public bool ContainsVariable()
	{
		if (this is Variable) return true;

		if (this is OperationExpression opExpr) return opExpr.Left.ContainsVariable() || opExpr.Right.ContainsVariable();

		return false;
	}

	public bool ContainsVariable(Variable testFor)
	{
		if (this is Variable innerVar && innerVar == testFor) return true;

		if (this is OperationExpression opExpr) return opExpr.Left.ContainsVariable(testFor) || opExpr.Right.ContainsVariable(testFor);

		return false;
	}
 
	public abstract bool Equals(Expression? other);
	public abstract override int GetHashCode();

	public override bool Equals(object? obj)
	{
		return Equals(obj as Expression);
	}
	
	public static bool operator ==(Expression self, Expression other) => self.Equals(other);
	public static bool operator !=(Expression self, Expression other) => !self.Equals(other);


	public static SumExpression operator +(Expression self, Expression other)
	{
		return new(self, other);
	}

	public static DifferenceExpression operator -(Expression self, Expression other)
	{
		return new(self, other);
	}

	public static DifferenceExpression operator -(Expression self)
	{
		return new((ValueExpression) 0, self);
	}

	public static ProductExpression operator *(Expression self, Expression other)
	{
		return new(self, other);
	}

	public static QuotientExpression operator /(Expression self, Expression other)
	{
		return new(self, other);
	}

	public static PowerExpression operator ^(Expression self, Expression other)
	{
		return new(self, other);
	}

	// For Rational support

	public static SumExpression operator +(Expression self, Rational other)
	{
		return new(self, (ValueExpression) other);
	}

	public static DifferenceExpression operator -(Expression self, Rational other)
	{
		return new(self, (ValueExpression) other);
	}

	public static ProductExpression operator *(Expression self, Rational other)
	{
		return new(self, (ValueExpression) other);
	}

	public static QuotientExpression operator /(Expression self, Rational other)
	{
		return new(self, (ValueExpression) other);
	}

	public static PowerExpression operator ^(Expression self, Rational other)
	{
		return new(self, (ValueExpression) other);
	}


	public static SumExpression operator +(Rational self, Expression other)
	{
		return new((ValueExpression) self, other);
	}

	public static DifferenceExpression operator -(Rational self, Expression other)
	{
		return new((ValueExpression) self, other);
	}

	public static ProductExpression operator *(Rational self, Expression other)
	{
		return new((ValueExpression) self, other);
	}

	public static QuotientExpression operator /(Rational self, Expression other)
	{
		return new((ValueExpression) self, other);
	}

	public static PowerExpression operator ^(Rational self, Expression other)
	{
		return new((ValueExpression) self, other);
	}
}