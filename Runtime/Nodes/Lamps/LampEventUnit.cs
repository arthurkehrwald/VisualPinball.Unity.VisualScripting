// Visual Pinball Engine
// Copyright (C) 2021 freezy and VPE Team
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.

using Unity.VisualScripting;

namespace VisualPinball.Unity.VisualScripting
{
	[UnitTitle("On Lamp Changed")]
	[UnitCategory("Events\\Visual Pinball")]
	public sealed class LampEventUnit : GleEventUnit<LampEventArgs>
	{
		[DoNotSerialize]
		[PortLabel("Lamp ID")]
		public ValueInput Id { get; private set; }

		[DoNotSerialize]
		[PortLabel("Intensity")]
		public ValueOutput Value { get; private set; }

		[DoNotSerialize]
		[PortLabel("Is Enabled")]
		public ValueOutput IsEnabled { get; private set; }

		protected override bool register => true;

		public override EventHook GetHook(GraphReference reference)
		{
			return new EventHook(VisualScriptingEventNames.LampEvent);
		}

		protected override void Definition()
		{
			base.Definition();

			Id = ValueInput(nameof(Id), string.Empty);

			Value = ValueOutput<float>(nameof(Value));
			IsEnabled = ValueOutput<bool>(nameof(IsEnabled));
		}

		protected override void AssignArguments(Flow flow, LampEventArgs args)
		{
			flow.SetValue(Value, args.Value);
			flow.SetValue(IsEnabled, args.Value > 0);
		}

		protected override bool ShouldTrigger(Flow flow, LampEventArgs args)
		{
			return args.Id == flow.GetValue<string>(Id);
		}
	}
}
