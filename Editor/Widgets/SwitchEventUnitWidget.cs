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

using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace VisualPinball.Unity.VisualScripting
{
	[Widget(typeof(SwitchEventUnit))]
	public sealed class SwitchEventUnitWidget : UnitWidget<SwitchEventUnit>
	{

		protected override NodeColorMix baseColor => GleAvailable ? NodeColorMix.TealReadable : new NodeColorMix { red = 1f, green = 0f, blue = 0f };
		private bool GameObjectAvailable => reference != null && reference.gameObject != null;
		private bool GleAvailable => GameObjectAvailable && Gle != null;
		private IGamelogicEngine Gle => reference.gameObject.GetComponentInParent<IGamelogicEngine>();

		private VariableNameInspector _lampIdInspector;
		private readonly Func<Metadata, VariableNameInspector> _switchIdInspectorConstructor;

		public SwitchEventUnitWidget(FlowCanvas canvas, SwitchEventUnit unit) : base(canvas, unit)
		{
			_switchIdInspectorConstructor = meta => new VariableNameInspector(meta, GetNameSuggestions);

			if (!GameObjectAvailable) {
				unit.Errors.Add("Not attached to GameObject. You need to attach this graph to a flow machine sitting on a GameObject in order to use it.");

			} else if (!GleAvailable) {
				unit.Errors.Add("No gamelogic engine found. One of the GameObject's parents must have a gamelogic engine component.");
			}
		}

		public override Inspector GetPortInspector(IUnitPort port, Metadata meta)
		{
			if (port == unit.id) {
				InspectorProvider.instance.Renew(ref _lampIdInspector, meta, _switchIdInspectorConstructor);

				return _lampIdInspector;
			}

			return base.GetPortInspector(port, meta);
		}

		private IEnumerable<string> GetNameSuggestions()
		{
			if (!GameObjectAvailable) {
				return new List<string>();
			}
			var gle = Gle;
			return gle == null ? new List<string>() : gle.AvailableSwitches.Select(lamp => lamp.Id).ToList();

		}
	}
}
