namespace MathEngine.Algebra.Solver;

public class UnsolvableEquationException : Exception
{
	public UnsolvableEquationException() { }

	public UnsolvableEquationException(string message) : base(message) { }

	public UnsolvableEquationException(string message, Exception inner) : base(message, inner) { }
}