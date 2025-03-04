using System.Runtime.ConstrainedExecution;

namespace MathEngine.Algebra.Expressions.Operational;

public sealed class ProductExpression(Expression left, Expression right) : OperationExpression(left, right, '*')
{
	public List<Expression> ToFactors()
	{	
		List<Expression> factors = [];

		if (Left is ProductExpression prodL) factors.AddRange(prodL.ToFactors());
		else factors.Add(Left);
	
		if (Right is ProductExpression prodR) factors.AddRange(prodR.ToFactors());
		else factors.Add(Right);

		return factors;
	}

	public static ProductExpression FromFactors(IList<Expression> factors)
	{
		return FromFactorsInternal(factors, 0);
	}

	static ProductExpression FromFactorsInternal(IList<Expression> factors, int startAt)
	{
		var count = factors.Count-startAt;

		if (count < 2) throw new ArgumentException("Cannot create ProductExpression of less than two factors");
		if (count == 2) return new ProductExpression(factors[startAt], factors[startAt+1]);

		// after our base case, use recursion to complete the task
		return new ProductExpression(factors[startAt], FromFactorsInternal(factors, startAt+1));
	}

	public override Expression Simplify() // TODO: simplification for rationals, etc. [see TODO file]
	{
		var (simplLeft, simplRight) = (Left.Simplify(), Right.Simplify());

		if (simplLeft == Undefined || simplRight == Undefined) return Undefined;

		if (simplLeft == Zero || simplRight == Zero) return Zero;
		if (simplLeft == simplRight) return new PowerExpression(simplLeft, (ValueExpression) 2).Simplify();
		if (simplLeft == One) return simplRight;
		if (simplRight == One) return simplLeft;


		var leftIsRational = SimplificationUtils.GetRationalValue(simplLeft, out var leftRat);
		var rightIsRational = SimplificationUtils.GetRationalValue(simplRight, out var rightRat);

		if (leftIsRational && leftRat.InnerValue == 0) return Zero; // TODO: need to check if other side is defined
		if (rightIsRational && rightRat.InnerValue == 0) return  Zero;
		
		if (leftIsRational && rightIsRational)
		{
			return SimplificationUtils.ToExpression(leftRat*rightRat);
		}

		
		if (SimplificationUtils.TryCombineVariableDegree(simplLeft, simplRight, out var combinedDegree)) return combinedDegree;

		// Distributive Property
		if (simplLeft is SumExpression sumExpr) return new SumExpression(new ProductExpression(sumExpr.Left, simplRight).Simplify(), new ProductExpression(sumExpr.Right, simplRight).Simplify()).Simplify();
		else if (simplRight is SumExpression sumExprR) return new SumExpression(new ProductExpression(simplLeft, sumExprR.Left).Simplify(), new ProductExpression(simplLeft, sumExprR.Right).Simplify()).Simplify();

		// commutative property, try to multiply out constants and square variables
		var factors = new ProductExpression(simplLeft, simplRight).ToFactors();

		if (factors.Count == 2) return new ProductExpression(simplLeft, simplRight); // this is just the single product expr instance we are in, we can just return ourselves


		HashSet<int> combinedAlready = [];
		List<Expression> combinedFactors = [];
		
		for (int i = 0; i < factors.Count; i++) // we will use indicies to identify SumExpression elements since both reference & default equality will not work - a user could pass the same term to both sides of a SumExpression
		{
			var facL = factors[i];

			if (combinedAlready.Contains(i)) continue;

			for (int j = 0; j < factors.Count; j++)
			{
				var facR = factors[j];

				if (i == j || combinedAlready.Contains(j)) continue;

				var assoc = new ProductExpression(facL, facR);

				var combineTry = assoc.Simplify();

				if (combineTry != assoc) // something was actually simplified
				{
					combinedAlready.Add(i);
					combinedAlready.Add(j);
					combinedFactors.Add(combineTry);
				}
			}
		}

		for (int i = 0; i < factors.Count; i++)
		{
			if (combinedAlready.Contains(i)) continue;

			combinedFactors.Add(factors[i]);
		}

		if (combinedFactors.Count == 1) return combinedFactors[0];

		return FromFactors(combinedFactors);
	}

	public override string ToString()
	{
		string leftRepr, rightRepr;

		if (Left.Equals(One)) return Right.ToString();
		if (Right.Equals(One)) return Left.ToString();

		if (Left.Equals(NegativeOne)) return $"-{Right}";
		if (Right.Equals(NegativeOne)) return $"-{Left}";

		leftRepr = $"({Left})";
		rightRepr = $"({Right})";

		if (Left is SumExpression or Variable) return $"{rightRepr}{leftRepr}";

		return $"{leftRepr}{rightRepr}";
	}

	public override string LaTeX() // TODO: modularize
	{
		string leftRepr, rightRepr;

		if (Left.Equals(One)) return Right.LaTeX();
		if (Right.Equals(One)) return Left.LaTeX();

		if (Left.Equals(NegativeOne)) return $"-{Right.LaTeX()}";
		if (Right.Equals(NegativeOne)) return $"-{Left.LaTeX()}";

		if (Left is SumExpression) leftRepr = $"({Left.LaTeX()})"; // need to parenthesize lower-order operations (DifferenceExpression doesn't exist anymore, so we only need to account for this case)
		else leftRepr = Left.LaTeX();

		if (Right is SumExpression) rightRepr = $"({Right.LaTeX()})";
		else rightRepr = Right.LaTeX();

		if (Left is SumExpression or Variable) return $"{rightRepr} {leftRepr}";

		return $"{leftRepr} {rightRepr}";
	}

	public override Expression SubstituteVariable(Variable var, Expression val)
	{
		return new ProductExpression(Left.SubstituteVariable(var, val), Right.SubstituteVariable(var, val));
	}
}