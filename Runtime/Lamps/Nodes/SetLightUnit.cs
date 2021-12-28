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

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VisualPinball.Engine.Math;

namespace VisualPinball.Unity.VisualScripting
{
	[UnitTitle("Set Light")]
	[UnitCategory("Visual Pinball")]
	public class SetLightUnit : Unit
	{
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput value;

		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput colorChannel;

		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput gameObjects;

		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		private Player _player;



		protected override void Definition()
		{
			enter = ControlInput(nameof(enter), Process);

			gameObjects = ValueInput<List<GameObject>>(nameof(gameObjects));

			value = ValueInput<float>(nameof(value), 0);
			colorChannel = ValueInput(nameof(colorChannel), ColorChannel.Alpha);

			exit = ControlOutput(nameof(exit));
		}

		private ControlOutput Process(Flow flow)
		{
			if (_player == null) {
				_player = UnityEngine.Object.FindObjectOfType<Player>();
			}

			var valueRaw = flow.GetValue<float>(value);
			var colorChannelRaw = flow.GetValue<ColorChannel>(colorChannel);

			foreach (var go in flow.GetValue<List<GameObject>>(gameObjects)) {
				if (go != null) {
					foreach (var lamp in go.GetComponentsInChildren<ILampDeviceComponent>()) {
						_player.Lamp(lamp).OnLamp(valueRaw, colorChannelRaw);
					}
				}
			}

			return exit;
		}
	}
}
