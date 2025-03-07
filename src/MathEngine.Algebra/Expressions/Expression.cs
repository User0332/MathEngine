using MathEngine.Algebra.Equations;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Algebra.Expressions.Polynomial;
using MathEngine.Values.Real.IrrationalValues;
using MathEngine.Values.Real.RationalValues;
using Rationals;

namespace MathEngine.Algebra.Expressions;

public abstract class Expression : IEquatable<Expression>
{
	public static readonly ValueExpression One = (ValueExpression) IntegerValue.One;
	public static readonly ValueExpression Zero = (ValueExpression) IntegerValue.Zero;
	public static readonly ValueExpression NegativeOne = (ValueExpression) IntegerValue.NegativeOne;
	public static readonly ValueExpression OneHalf = (ValueExpression) ((Rational) 1/2);
	public static readonly ValueExpression PI = new(SpecialConstantValue.PI);
	public static readonly ValueExpression E = new(SpecialConstantValue.E);
	public static readonly Expression Undefined = new UndefinedExpression();

	public abstract override string ToString();
	public abstract string LaTeX();
	public abstract string Repr();
	public virtual Expression Simplify() => this;

	public PowerExpression Square() => new(this, (ValueExpression) 2);
	public PowerExpression Cube() => new(this, (ValueExpression) 3);
	public PowerExpression Sqrt() => new(this, (ValueExpression) ((Rational) 1/2));
	public PowerExpression Cbrt() => new(this, (ValueExpression) ((Rational) 1/3));

	public PolynomialExpression ToPolynomial()
	{
		return PolynomialExpression.From(this);
	}

	public abstract bool ContainsVariable();

	public abstract bool ContainsVariable(Variable testFor);

	public abstract Expression Substitute(Variable var, Expression val);
 
	public abstract bool Equals(Expression? other);
	public abstract override int GetHashCode();

	public override bool Equals(object? obj)
	{
		return Equals(obj as Expression);
	}

	public Equation SetEqualTo(Expression other) => new(this, other);
	
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