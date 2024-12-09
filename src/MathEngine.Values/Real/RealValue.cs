namespace MathEngine.Values.Real;

public abstract class RealValue : Value
{
	public override RealValue Simplify()
	{
		return this;
	}
}