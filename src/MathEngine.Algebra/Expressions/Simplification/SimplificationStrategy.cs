namespace MathEngine.Algebra.Expressions.Simplification;

[Flags]
public enum SimplificationStrategy
{
	/// <summary>
	/// Default simplification method.
	/// </summary>
	Default = 1 << 0,

	/// <summary>
	/// <para> If this simplification method is enabled, expressions will be able to access saved info from other parts of the expression during the simplification process.
	/// This is useful, for example, if a part of an expression is cancelable but cannot be canceled since its prescence might make the expression undefined (e.x. (x-3)/(x-3)).
	/// If another part of the expression still covers that undefined case (e.x. if 1/(x-3) exists somewhere else in the expression), then the simplification
	/// engine would be able to recognize that fact through <see cref="SavedSimplificationInfo"/> and would thus be able to cancel the (x-3)/(x-3) term, resulting in a
	/// more simplified expression with identical semantics.</para>
	/// 
	/// <para>Note that this simplification method causes the engine to perform two passes of the expression, so it will take longer than <see cref="Default"/>.</para>
	/// </summary>
	UseInfo = 1 << 1,
}