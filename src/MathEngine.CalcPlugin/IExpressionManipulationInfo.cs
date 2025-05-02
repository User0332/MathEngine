using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;

namespace MathEngine.CalcPlugin;

public interface IExpressionManipulationInfo
{
	public abstract IEnumerable<Expression> GetAlternateForms(Expression expr, Variable focus);
}
