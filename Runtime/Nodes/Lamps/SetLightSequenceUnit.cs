// Visual Pinball Engine
// Copyright (C) 2022 freezy and VPE Team
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
using UnityEngine;
using VisualPinball.Engine.Math;

namespace VisualPinball.Unity.VisualScripting
{
	[UnitTitle("Set Light Sequence")]
	[UnitSurtitle("Scene")]
	[UnitCategory("Visual Pinball")]
	public class SetLightSequenceUnit : GleUnit
	{
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput InputTrigger;

		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput OutputTrigger;

		[DoNotSerialize]
		[PortLabel("Light Group")]
		public ValueInput LightGroup;

		[DoNotSerialize]
		[PortLabel("Intensity")]
		public ValueInput Value;

		[DoNotSerialize]
		[PortLabel("Color Channel")]
		public ValueInput ColorChannel;

		[DoNotSerialize]
		[PortLabel("Step")]
		public ValueInput Step;

		private int _currentIndex;

		protected override void Definition()
		{
			InputTrigger = ControlInput(nameof(InputTrigger), Process);
			OutputTrigger = ControlOutput(nameof(OutputTrigger));

			LightGroup = ValueInput<LightGroupComponent>(nameof(LightGroup), null);

			Value = ValueInput<float>(nameof(Value), 0);
			ColorChannel = ValueInput(nameof(ColorChannel), Engine.Math.ColorChannel.Alpha);
			Step = ValueInput<int>(nameof(Step), 1);

			Requirement(LightGroup, InputTrigger);
			Requirement(Value, InputTrigger);
			Succession(InputTrigger, OutputTrigger);
		}

		private ControlOutput Process(Flow flow)
		{
			if (!AssertPlayer(flow)) {
				Debug.LogError("Cannot find player.");
				return OutputTrigger;
			}

			var valueRaw = flow.GetValue<float>(Value);
			var colorChannelRaw = flow.GetValue<ColorChannel>(ColorChannel);
			var stepRaw = flow.GetValue<int>(Step);
			var lamps = flow.GetValue<LightGroupComponent>(LightGroup).Lights;

			for (var index = 0; index < lamps.Count; index++) {
				Player.Lamp(lamps[index]).OnLamp(
					index >= _currentIndex * stepRaw && index < (_currentIndex + 1) * stepRaw ? valueRaw : 0,
					colorChannelRaw);
			}

			if (++_currentIndex >= lamps.Count / stepRaw) {
				_currentIndex = 0;
			}

			return OutputTrigger;
		}
	}
}
