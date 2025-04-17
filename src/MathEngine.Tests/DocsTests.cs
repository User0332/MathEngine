using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;

namespace MathEngine.Tests;

public static class DocsTests
{
	static void Run()
	{		
		Variable h = new('h');

		Expression expr = (h^4)+2*h+3;

		Console.WriteLine(expr.Substitute(h, Expression.OneHalf).Simplify());
		Console.WriteLine(expr.Substitute(Variable.T, Expression.OneHalf).Simplify()); /* does nothing because the variable 't' is not in the expression */
	}
}