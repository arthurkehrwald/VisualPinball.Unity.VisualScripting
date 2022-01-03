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
	[UnitTitle("Get Lamp Value")]
	[UnitCategory("Visual Pinball")]
	public class GetLampValueUnit : Unit
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

		private Player _player;

		protected override void Definition()
		{
			id = ValueInput(nameof(id), string.Empty);

			value = ValueOutput(nameof(value), GetValue);
			enabled = ValueOutput(nameof(enabled), GetEnabled);
		}

		private float GetValue(Flow flow)
		{
			if (_player == null) {
				_player = UnityEngine.Object.FindObjectOfType<Player>();
			}

			var key = flow.GetValue<string>(id);
			return _player.LampStatuses.ContainsKey(key) ? _player.LampStatuses[key] : 0;
		}

		private bool GetEnabled(Flow flow)
		{
			if (_player == null) {
				_player = flow.stack.self.GetComponentInParent<Player>();
			}

			var key = flow.GetValue<string>(id);
			return _player.LampStatuses.ContainsKey(key) && (_player.LampStatuses[key] > 0);
		}
	}
}
