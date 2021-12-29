using System.Collections.Generic;
using Unity.VisualScripting;
using VisualPinball.Unity.VisualScripting;

namespace Editor
{
	[Analyser(typeof(SwitchEventUnit))]
	public class GleUnitAnalyser : UnitAnalyser<SwitchEventUnit>
	{
		public GleUnitAnalyser(GraphReference reference, SwitchEventUnit target) : base(reference, target) { }

		protected override IEnumerable<Warning> Warnings()
		{
			foreach (var baseWarning in base.Warnings())
			{
				yield return baseWarning;
			}

			foreach (var warning in unit.Errors) {
				yield return Warning.Error(warning);
			}
		}
	}
}
