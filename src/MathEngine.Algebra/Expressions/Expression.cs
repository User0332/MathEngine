using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Algebra.Expressions.Polynomial;
using MathEngine.Values.Real.RationalValues;
using Rationals;

namespace MathEngine.Algebra.Expressions;

public abstract class Expression : IEquatable<Expression>
{
	public static readonly Expression One = (ValueExpression) IntegerValue.One;
	public static readonly Expression Zero = (ValueExpression) IntegerValue.Zero;
	public static readonly Expression NegativeOne = (ValueExpression) IntegerValue.NegativeOne;

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

	public static SumExpression operator -(Expression self, Expression other)
	{
		return new(self, new ProductExpression(NegativeOne, other));
	}

	public static ProductExpression operator -(Expression self)
	{
		return new ProductExpression(NegativeOne, self);
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

	public static SumExpression operator -(Expression self, Rational other)
	{
		return new(self, new ProductExpression(NegativeOne, (ValueExpression) other));
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

	public static SumExpression operator -(Rational self, Expression other)
	{
		return new((ValueExpression) self, new ProductExpression(NegativeOne, other));
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