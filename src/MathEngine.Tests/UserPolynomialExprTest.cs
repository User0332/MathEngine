using MathEngine.Algebra;
using MathEngine.Algebra.Equations;
using MathEngine.Algebra.Expressions;
using MathEngine.Algebra.Expressions.Operational;
using MathEngine.Algebra.Expressions.Polynomial;
using MathEngine.Algebra.Solver;
using MathEngine.Algebra.Solver.Polynomial;

namespace MathEngine.Tests;

public static class UserPolynomialExprTest
{
	public static void Run()
	{
		var x = Variable.X;

		while (true)
		{
			List<Expression> factors = [];

			while (true)
			{
				List<Expression> terms = [];

				while (true)
				{
					
					Console.Write("Enter Coefficient: ");

					if (int.TryParse(Console.ReadLine(), out int coeff))
					{
						Console.Write("Enter Degree: ");

						if (int.TryParse(Console.ReadLine(), out int degree))
						{
							if (degree > 0) terms.Add((x^degree)*coeff);
							else if (degree == 0) terms.Add((ValueExpression) coeff);
							else
							{
								Console.WriteLine("Invalid degree!");

								continue;
							}

							Console.Write("Enter to add another term, anything else to stop adding terms ");

							string contTerm = Console.ReadLine()!;

							if (contTerm == "") continue;

							break;
						}
					}

					Console.WriteLine("Invalid Input!");
				}

				if (terms.Count == 1) factors.Add(terms[0]);
				else factors.Add(SumExpression.FromTerms(terms));

				Console.Write("Enter to add another factor, anything else to stop adding factors ");

				string contFac = Console.ReadLine()!;

				if (contFac == "") continue;

				break;
			}

			PolynomialExpression expr;

			if (factors.Count == 1) expr = factors[0].ToPolynomial();
			else expr = ProductExpression.FromFactors(factors).ToPolynomial();

			Console.WriteLine($"Your Expression: {expr}");
			Console.WriteLine($"LaTeX Form: {expr.LaTeX()}");

			var normalized = expr.Normalize();

			Console.WriteLine($"Normalized Form: {normalized}");
			Console.WriteLine($"LaTeX Form: {normalized.LaTeX()}");

			var eq = new PolynomialEquation(normalized, PolynomialExpression.ZeroExpr());

			var roots = eq.Solve(strategy: PolynomialSolvingStrategy.UseFormula).Select(soln => soln.Simplify()).Distinct();

			Console.WriteLine($"Roots: {string.Join(", ", roots)}");
			Console.WriteLine($"Roots in LaTeX: {string.Join(", ", roots.Select(root => root.LaTeX()))}");


			Console.Write("Enter to solve for the roots of another polynomial, anything else to exit");

			string contPoly = Console.ReadLine()!;

			if (contPoly == "") continue;

			break;
		}
	}
}