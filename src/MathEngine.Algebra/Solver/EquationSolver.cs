// using MathEngine.Algebra.Equations;
// using MathEngine.Algebra.Expressions;
// using MathEngine.Algebra.Expressions.Terms;
// using MathEngine.Values;
// using MathEngine.Values.Real.RationalValues;

// namespace MathEngine.Algebra.Solver;

// public static class EquationSolver
// {
// 	public static IEnumerable<Expression> SolveFor(this Equation eq, Variable var)
// 	{
// 		return [ (Expression) IntegerValue.One.AsTerm() ];
// 	}

// 	public static IEnumerable<Expression> SolveFor(this PolynomialEquation eq, Variable var)
// 	{
// 		var expr = eq.SetZeroSide().LeftSide.Simplify().Normalize(); // must find the roots of this

// 		if (expr.Degree > 2) throw new UnsolvableEquationException("Equation solving for polynomials of degree 3 or greater is currently unsupported");
// 		else if (expr.Degree == 2) // use quadratic formula & return factors
// 		{
// 			// in normalized simplified form, we can assume that the terms follow the pattern Term(Product(Expression(ValueTerm(Coefficient)), Expression(NthPowerOf(Variable, ValueTerm(Degree))))
// 			// therefore, for example, the a term of the quadratic in standard form is ((ValueTerm) quadraticTerm.Inner.Factors[0].Terms[0])
	
// 			ProductTerm quadraticTerm = expr.GetTermOfDegree(2)!;
// 			ProductTerm linearTerm = expr.GetTermOfDegree(1) ?? IntegerValue.Zero.AsProduct();
// 			ProductTerm constantTerm = expr.GetTermOfDegree(0) ?? IntegerValue.Zero.AsProduct();
			
// 			var a = (ValueTerm) quadraticTerm.Inner.Factors[0].Terms[0];
// 			var b = (ValueTerm) linearTerm.Inner.Factors[0].Terms[0];
// 			var c = (ValueTerm) constantTerm.Inner.Factors[0].Terms[0];

// 			var discriminant = new Expression([
// 				new NthPowerOf(
// 					b,
// 					new IntegerValue(2).AsTerm()
// 				),
// 				new ProductTerm(
// 					new Product([new IntegerValue(4).AsExpression(), a, c])
// 				).AsExpression().Negate().AsTerm()
// 			]);

// 			var denominator = new Product([
// 				a, new IntegerValue(2).AsExpression()
// 			]).AsExpression();

// 			var factorOne = new Product([
// 				new Expression([
// 					b.AsExpression().Negate().AsTerm(),
// 					discriminant.AsTerm()
// 				]),
// 				denominator.Reciprocal()
// 			]);

// 			var factorTwo = new Product([
// 				new Expression([
// 					b.AsExpression().Negate().AsTerm(),
// 					discriminant.Negate().AsTerm()
// 				]),
// 				denominator.Reciprocal()
// 			]);

// 			return [factorOne, factorTwo];
// 		}
// 		else if (expr.Degree == 1) // return the root of ax+b=0
// 		{
// 			ProductTerm linearTerm = expr.GetTermOfDegree(1)!;
// 			ProductTerm constantTerm = expr.GetTermOfDegree(0) ?? IntegerValue.Zero.AsProduct();

// 			var a = (ValueTerm) linearTerm.Inner.Factors[0].Terms[0];
// 			var b = (ValueTerm) constantTerm.Inner.Factors[0].Terms[0];

// 			var root = new Product([
// 				b,
// 				a.AsExpression().Negate().Reciprocal()
// 			]);

// 			return [root];
// 		}
// 		else // degree is zero, we do not support this yet
// 		{
// 			throw new UnsolvableEquationException("Solving of polynomial equations of degree 0 is currently unsupported");
// 		}
// 	}
// }