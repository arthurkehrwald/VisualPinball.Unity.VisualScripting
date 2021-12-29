using System.Collections.Generic;
using Unity.VisualScripting;
using VisualPinball.Unity;
using VisualPinball.Unity.VisualScripting;

namespace Editor
{

	[Analyser(typeof(SwitchEventUnit))]
	public class SwitchEventUnitAnalyzer : GleUnitAnalyser<SwitchEventArgs>
	{
		public SwitchEventUnitAnalyzer(GraphReference reference, SwitchEventUnit target) : base(reference, target)
		{
		}
	}

	public abstract class GleUnitAnalyser<TArgs> : UnitAnalyser<GleEventUnit<TArgs>>
	{
		protected GleUnitAnalyser(GraphReference reference, GleEventUnit<TArgs> target) : base(reference, target) { }

		protected override IEnumerable<Warning> Warnings()
		{
			foreach (var baseWarning in base.Warnings()) {
				yield return baseWarning;
			}

			foreach (var warning in unit.Errors) {
				yield return Warning.Error(warning);
			}
		}
	}
}
