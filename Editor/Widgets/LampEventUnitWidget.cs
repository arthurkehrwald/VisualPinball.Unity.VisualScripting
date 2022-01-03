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
using Unity.VisualScripting;

namespace VisualPinball.Unity.VisualScripting.Editor
{
	[Widget(typeof(LampEventUnit))]
	public sealed class LampEventUnitWidget : UnitWidget<LampEventUnit>
	{
		public LampEventUnitWidget(FlowCanvas canvas, LampEventUnit unit) : base(canvas, unit)
		{
			lampIdInspectorConstructor = (metadata) => new VariableNameInspector(metadata, GetNameSuggestions);
		}

		protected override NodeColorMix baseColor => NodeColorMix.TealReadable;

		private VariableNameInspector lampIdInspector;
		private Func<Metadata, VariableNameInspector> lampIdInspectorConstructor;

		public override Inspector GetPortInspector(IUnitPort port, Metadata metadata)
		{
			if (port == unit.id) {
				InspectorProvider.instance.Renew(ref lampIdInspector, metadata, lampIdInspectorConstructor);

				return lampIdInspector;
			}

			return base.GetPortInspector(port, metadata);
		}

		private IEnumerable<string> GetNameSuggestions()
		{
			var list = new List<string>();

			var tableComponent = TableSelector.Instance.SelectedTable;

			if (tableComponent != null) {
				var gle = tableComponent.gameObject.GetComponent<IGamelogicEngine>();

				if (gle != null) {
					foreach (var lamp in gle.AvailableLamps) {
						list.Add(lamp.Id);
					}
				}
			}

			return list;
		}
	}
}
