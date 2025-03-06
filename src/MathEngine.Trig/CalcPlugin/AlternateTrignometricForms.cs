using MathEngine.Algebra;
using MathEngine.Algebra.Expressions;
using MathEngine.CalcPlugin;
using MathEngine.Trig.Functions;

namespace MathEngine.Trig.CalcPlugin;

public class AlternateTrignometricForms : ExpressionManipulationInfo
{
	static readonly HashSet<Expression>[] alternateFormData;
	static readonly Variable internalVar = new('@', "internalrestricted_AlternateTrignometricForms");

	static AlternateTrignometricForms() // initialize trig identity data
	{
		var x = internalVar;

		var sin = TrigFunctions.Sin.AsDelegate();
		var cos = TrigFunctions.Cos.AsDelegate();
		var tan = TrigFunctions.Tan.AsDelegate();
		var sec = TrigFunctions.Sec.AsDelegate();
		var csc = TrigFunctions.Csc.AsDelegate();
		var cot = TrigFunctions.Cot.AsDelegate();
		var pi = Expression.PI;

		alternateFormData = [
			[
				sin(x), 
				2*cos(x/2)*sin(x/2),
				cos(pi/2 - x),
				1/csc(x),
				-sin(-x)
			],
			[
				cos(x),
				-1+2*(cos(x/2)^2),
				1-2*(sin(x/2)^2),
				sin(pi/2 - x),
				1/sec(x),
				cos(-x),
				(cos(x/2)^2)-(sin(x/2)^2)
			],
			[
				tan(x),
				sin(x)/cos(x),
				cot(pi/2 - x),
				1/cot(x),
				-cot(2*x)+csc(2*x),
				sin(2*x)/(1+cos(2*x)),
				2*tan(x/2)/(1-(tan(x/2)^2)),
				-tan(-x)
			],
			[
				csc(x),
				sec(pi/2 - x),
				1/sin(x),
				Expression.OneHalf*csc(x/2)*sec(x/2),
				-csc(-x)
			],
			[
				sec(x),
				csc(pi/2 - x),
				1/cos(x),
				(sec(x/2)^2)/(2-(sec(x/2)^2)),
				sec(-x)
			],
			[
				cot(x),
				tan(pi/2 - x),
				1/tan(x),
				cot(2*x)+csc(2*x),
				csc(x)*sec(x)-tan(x),
				sin(2*x)/(1-cos(2*x)),
				Expression.OneHalf*(cot(x/2)-tan(x/2))
				-cot(-x)
			],
			[
				sin(x)^2,
				1-(cos(x)^2),
				Expression.OneHalf*(1-cos(2*x)),
			],
			[
				cos(x)^2,
				1-(sin(x)^2),
				Expression.OneHalf*(1 + cos(2*x)),
			],
			[
				tan(x)^2,
				(sin(2*x)^2)/((cos(2*x)+1)^2),
				(sec(x)^2)-1
			],
			[
				sec(x)^2,
				(tan(x)^2)+1
			],
			[
				csc(x)^2,
				(cot(x)^2)+1
			],
			[
				cot(x)^2,
				(csc(x)^2)-1
			]
			// TODO: need to add trig, half angle, double angle, angle addition, power reducing, etc. identities
		];
		
	}

	public override IEnumerable<Expression> GetAlternateForms(Expression expr, Variable focus)
	{
		var searchExpr = expr.Substitute(focus, internalVar);

		var matchingSet = alternateFormData.FirstOrDefault(altExprs => altExprs.Contains(searchExpr)) ?? [];

		var finalSet = matchingSet;

		return finalSet
			.Where(form => form != searchExpr)
			.Select(altExpr => altExpr.Substitute(internalVar, focus));
	}
}