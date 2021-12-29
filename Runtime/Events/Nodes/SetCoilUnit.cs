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
	[UnitTitle("Set Coil")]
	[UnitCategory("Visual Pinball")]
	public class SetCoilUnit : GleUnit
	{
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput inputTrigger;

		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput outputTrigger;

		[DoNotSerialize]
		[PortLabel("Coil ID")]
		public ValueInput id { get; private set; }

		[DoNotSerialize]
		[PortLabel("Value")]
		public ValueInput enabled { get; private set; }

		protected override void Definition()
		{
			inputTrigger = ControlInput(nameof(inputTrigger), _ => outputTrigger);
			outputTrigger = ControlOutput(nameof(outputTrigger));

			id = ValueInput<string>(nameof(id), string.Empty);
			enabled = ValueInput<bool>(nameof(enabled), false);
		}
	}
}
