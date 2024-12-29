using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Expressions.Polynomial;

public class PolynomialExpression : Expression
{
	public readonly Expression BaseNode;
	public readonly Variable Variable;
	public readonly int Degree;

	internal PolynomialExpression(Expression baseTerm)
	{
		(Variable, Degree) = ValidateNode(baseTerm);
		
		BaseNode = baseTerm;
	}

	public static PolynomialExpression From(Expression baseTerm)
	{
		return baseTerm as PolynomialExpression ?? new(baseTerm);
	}

	/// <summary>
	/// Converts the PolynomialExpression to normalized form where the terms are arranged like so for a given polynomial:
	/// <para>ax^n + bx^(n-1) + cx^(n-2) + ...</para>
	/// <code>
	/// SumExpr(
	/// 	ProductExpr(a, PowerExpr(x, n)),
	/// 	SumExpr(
	/// 		ProductExpr(b, PowerExpr(x, n-1)),
	/// 		SumExpr(...)
	/// 	)
	/// )</code>
	/// <para>Any terms whose coefficient (a, b, c, ...) is zero are still included in the normalized polynomial</para>
	/// </summary>
	/// <returns>A mathematically equivalent PolynomialExpression in normalized form</returns>
	public PolynomialExpression Normalize()
	{
		return new(BaseNode.Simplify());
		// var terms = new List<Expression>();
		// CollectTerms(BaseNode, terms);

		// Expression normalizedExpr = terms.Aggregate((acc, term) => new SumExpression(acc, term));
		
		// return new(normalizedExpr);
	}

	static void CollectTerms(Expression expr, List<Expression> terms)
	{
		if (expr is SumExpression sumExpr)
		{
			CollectTerms(sumExpr.Left, terms);
			CollectTerms(sumExpr.Right, terms);
		}
		else if (expr is ProductExpression prodExpr)
		{
			var leftTerms = new List<Expression>();
			var rightTerms = new List<Expression>();

			CollectTerms(prodExpr.Left, leftTerms);
			CollectTerms(prodExpr.Right, rightTerms);

			foreach (var leftTerm in leftTerms)
			{
				foreach (var rightTerm in rightTerms)
				{
					terms.Add(new ProductExpression(leftTerm, rightTerm));
				}
			}
		}
		else
		{
			terms.Add(expr);
		}
	}


	/// <summary>
	/// Validates that the expression is a single-variable polynomial 
	/// </summary>
	/// <param name="baseExpr"></param>
	/// <returns>Variable used in expression and polynomial degree</returns>
	(Variable Var, int Degree) ValidateNode(Expression baseExpr)
	{
		(Variable? usingVar, int deg) = ValidateNodeInternal(baseExpr, 0, null);

		if (usingVar is null) throw new ArgumentException("No variable used in polynomial expression!");

		return (usingVar, deg);
	}

	(Variable?, int) ValidateNodeInternal(Expression baseExpr, int highestDeg, Variable? usingVar = null)
	{
		if (baseExpr is OperationExpression opExpr)
		{
			if (opExpr is SumExpression or DifferenceExpression)
			{
				(usingVar, highestDeg) = ValidateNodeInternal(opExpr.Left, highestDeg, usingVar);
				(usingVar, highestDeg) = ValidateNodeInternal(opExpr.Right, highestDeg, usingVar);
			}
			else if (opExpr is ProductExpression)
			{
				(usingVar, int leftDeg) = ValidateNodeInternal(opExpr.Left, 0, usingVar);
				(usingVar, int rightDeg) = ValidateNodeInternal(opExpr.Right, 0, usingVar);

				int productDeg = leftDeg+rightDeg;

				highestDeg = Math.Max(productDeg, highestDeg);
			}
			else if (opExpr is QuotientExpression divExpr) // check that the denominator does not have a variable
			{
				if (divExpr.Denominator.ContainsVariable()) throw new ArgumentException("Rational expressions are not supported in polynomial expression!");

				(usingVar, highestDeg) = ValidateNodeInternal(divExpr.Numerator, highestDeg, usingVar);
			}
			else if (opExpr is PowerExpression powExpr)
			{
				(usingVar, highestDeg) = ValidatePowerExpr(powExpr, highestDeg, usingVar);
			}
		}
		else if (baseExpr is Variable varExpr)
		{
			if (usingVar is null)
			{
				usingVar = varExpr;
				highestDeg = 1;
			}
			else if (usingVar != varExpr) throw new ArgumentException("Multiple variables used in polynomial expression");
		}

		return (usingVar, highestDeg);
	}

	(Variable?, int) ValidatePowerExpr(PowerExpression powExpr, int highestDeg, Variable? usingVar = null)
	{
		(var innerVar, int innerDeg) = ValidateNodeInternal(powExpr.Base, 0, null); // the inside of a power expression can be a polynomial or constant
	
		if (innerVar is null) return (usingVar, highestDeg);
	
		if (
			powExpr.Exponent is not ValueExpression valExpr ||
			valExpr.Inner is not IntegerValue integerValue ||
			integerValue.InnerValue <= 0
		) throw new ArgumentException("Variable raised to a non-natural power in polynomial expression");

		if (innerVar != usingVar && usingVar is not null) throw new ArgumentException("Multiple variables used in polynomial expression");

		usingVar = innerVar;

		int exprDeg = innerDeg*(int) integerValue.InnerValue;

		highestDeg = Math.Max(highestDeg, exprDeg);

		return (usingVar, highestDeg);
	}

	public override string ToString()
	{
		return BaseNode.ToString();
	}

	public override bool Equals(Expression? other)
	{
		return other is PolynomialExpression otherPoly && otherPoly.BaseNode == BaseNode;
	}

	public override int GetHashCode()
	{
		return BaseNode.GetHashCode();
	}

	public override string Repr()
	{
		return BaseNode.Repr();
	}
}