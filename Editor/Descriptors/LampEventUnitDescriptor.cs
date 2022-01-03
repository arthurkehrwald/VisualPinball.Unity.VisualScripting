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

namespace VisualPinball.Unity.VisualScripting.Editor
{
	[Descriptor(typeof(LampEventUnit))]
	public class LampEventUnitDescriptor : UnitDescriptor<LampEventUnit>
	{
		public LampEventUnitDescriptor(LampEventUnit target) : base(target)
		{
		}

		protected override string DefinedSummary()
		{
			return "This node triggers an event when a lamp with a given ID changes its intensity.";
		}

		protected override EditorTexture DefinedIcon() => EditorTexture.Single(Unity.Editor.Icons.LampEvent);

		protected override void DefinedPort(IUnitPort port, UnitPortDescription desc)
		{
			base.DefinedPort(port, desc);

			switch (port.key) {
				case nameof(LampEventUnit.Id):
					desc.summary = "The ID of the lamp that changed its intensity.";
					break;
				case nameof(LampEventUnit.Value):
					desc.summary = "The new intensity of the lamp (0-255).";
					break;
				case nameof(LampEventUnit.IsEnabled):
					desc.summary = "Whether the intensity is larger than 0.";
					break;
			}
		}
	}
}
