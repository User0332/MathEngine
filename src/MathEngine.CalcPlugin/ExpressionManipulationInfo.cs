using MathEngine.Algebra.Expressions;

namespace MathEngine.CalcPlugin;

public abstract class ExpressionManipulationInfo
{
	public abstract IEnumerable<Expression> GetAlternateForms(Expression expr);
}
