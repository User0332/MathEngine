using MathEngine.Values.Real.RationalValues;

namespace MathEngine.Algebra.Expressions.Operational;

public sealed class SumExpression(Expression left, Expression right) : OperationExpression(left, right, '+')
{
	public List<Expression> ToTerms()
	{	
		List<Expression> terms = [];

		if (Left is SumExpression sumL) terms.AddRange(sumL.ToTerms());
		else terms.Add(Left);
	
		if (Right is SumExpression sumR) terms.AddRange(sumR.ToTerms());
		else terms.Add(Right);

		return terms;
	}

	public static SumExpression FromTerms(IList<Expression> terms)
	{
		return FromTermsInternal(terms, 0);
	}

	static SumExpression FromTermsInternal(IList<Expression> terms, int startAt)
	{
		var count = terms.Count-startAt;

		if (count < 2) throw new ArgumentException("Cannot create SumExpression of less than two terms");
		if (count == 2) return new SumExpression(terms[startAt], terms[startAt+1]);

		// after our base case, use recursion to complete the task
		return new SumExpression(terms[startAt], FromTermsInternal(terms, startAt+1));
	}

	public override Expression Simplify()
	{
		if (Left.Equals(Right)) return new ProductExpression((ValueExpression) 2, Left).Simplify();

		var (simplLeft, simplRight) = (Left.Simplify(), Right.Simplify());
		
		if (SimplificationUtils.GetRationalValue(simplLeft, out var leftRat) && SimplificationUtils.GetRationalValue(simplRight, out var rightRat))
		{
			return SimplificationUtils.ToExpression(leftRat+rightRat);
		}

		// break the sum down into a list of summed exprs
		var terms = new SumExpression(simplLeft, simplRight).ToTerms();

		// Console.WriteLine(string.Join(", ", terms.Select(term => term.GetType().Name)));

		// check each term against every other term to attempt to combine as many like terms as possible, then add everything together at the end

		HashSet<int> combinedAlready = [];
		List<Expression> combinedTerms = [];
		
		for (int i = 0; i < terms.Count; i++) // we will use indicies to identify SumExpression elements since both reference & default equality will not work - a user could pass the same term to both sides of a SumExpression
		{
			var termL = terms[i];

			if (combinedAlready.Contains(i)) continue;

			for (int j = 0; j < terms.Count; j++)
			{
				var termR = terms[j];

				if (i == j || combinedAlready.Contains(j)) continue;

				var combineTry = InternalTryCombineLikeTerms(termL, termR);

				if (combineTry is not null) // combining like terms was successful, register these terms as used
				{
					combinedAlready.Add(i);
					combinedAlready.Add(j);
					combinedTerms.Add(combineTry);
				}
			}
		}

		for (int i = 0; i < terms.Count; i++)
		{
			if (combinedAlready.Contains(i)) continue;

			combinedTerms.Add(terms[i]); // add on all of our terms that we did not combine/simplify
		}

		var simpl = FromTerms(combinedTerms); // convert terms back to SumExpression and the simplified expression
	
		return simpl;
	}
	
	// TODO: right now, this only searches for ProductExpressions, but this won't work for terms where the coefficient is 1 and there is no ProductExpression
	static Expression? InternalTryCombineLikeTerms(Expression left, Expression right) // combine like terms, do not simplify the returned ProductExpression so distributive property and combining like terms do not get infinitely recursively applied
	{
		if (left is ProductExpression prodExprL && right is ProductExpression prodExprR)
		{
			if (prodExprR.Left.Equals(prodExprL.Left))
			{
				var coeffOne = prodExprR.Right;
				var coeffTwo = prodExprL.Right;
				var coeffSum = (coeffOne+coeffTwo).Simplify();

				var combined = new ProductExpression(coeffSum, prodExprL.Left); // prodExpr*.Left is the "like" part of the like terms

				if (coeffSum is not SumExpression) return combined.Simplify(); // do not simplify if the coeffSum is a SumExpression (this will cause infinite distributive property)

				return combined;
			}
		
			// TODO: maybe modularize this to minimize repeat code later
			if (prodExprR.Right.Equals(prodExprL.Right))
			{
				var coeffOne = prodExprR.Left;
				var coeffTwo = prodExprL.Left;
				var coeffSum = (coeffOne+coeffTwo).Simplify();

				var combined = new ProductExpression(coeffSum, prodExprL.Right); // prodExpr*.Right is the "like" part of the like terms

				if (coeffSum is not SumExpression) return combined.Simplify(); // do not simplify if the coeffSum is a SumExpression (this will cause infinite distributive property)

				return combined;
			}
		}

		return null;
	}
}