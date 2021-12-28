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
	[UnitTitle("On Lamp Event")]
	[UnitCategory("Events\\Visual Pinball")]
	public sealed class LampEventUnit : EventUnit<LampEventArgs>
	{
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput id { get; private set; }

		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput value { get; private set; }

		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput enabled { get; private set; }

		protected override bool register => true;

		public override EventHook GetHook(GraphReference reference)
		{
			return new EventHook(EventNames.LampEvent);
		}

		protected override void Definition()
		{
			base.Definition();

			id = ValueInput(nameof(id), string.Empty);

			value = ValueOutput<int>(nameof(value));
			enabled = ValueOutput<bool>(nameof(enabled));
		}

		protected override void AssignArguments(Flow flow, LampEventArgs args)
		{
			flow.SetValue(value, args.Value);
			flow.SetValue(enabled, args.Value > 0);
		}

		protected override bool ShouldTrigger(Flow flow, LampEventArgs args)
		{
			return args.Id == flow.GetValue<string>(id);
		}
	}
}
