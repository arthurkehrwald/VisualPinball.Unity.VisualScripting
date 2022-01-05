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

// ReSharper disable UnusedType.Global

using Unity.VisualScripting;
using VisualPinball.Unity.Editor;
using IconSize = VisualPinball.Unity.Editor.IconSize;

namespace VisualPinball.Unity.VisualScripting.Editor
{
	[Descriptor(typeof(SetLightSequenceUnit))]
	public class SetLightSequenceUnitDescriptor : UnitDescriptor<SetLightSequenceUnit>
	{
		public SetLightSequenceUnitDescriptor(SetLightSequenceUnit target) : base(target)
		{
		}

		protected override string DefinedSummary()
		{
			return "This node sets the intensity of the lights at the next position within a light group. On each execution, the position increases by the number of steps. The number of lights set is the step number.\n\nExample: Four lights, A B C D, step size = 2. First run, lights A and B are set. Second run, lights C and D are set.";
		}

		protected override EditorTexture DefinedIcon() => EditorTexture.Single(Unity.Editor.Icons.LampSequence);

		protected override void DefinedPort(IUnitPort port, UnitPortDescription desc)
		{
			base.DefinedPort(port, desc);

			switch (port.key) {
				case nameof(SetLightSequenceUnit.LightGroup):
					desc.summary = "The light group to cycle through.";
					break;

				case nameof(SetLightSequenceUnit.Value):
					desc.summary = "The intensity to apply to the current step (0-1).";
					break;

				case nameof(SetLightSequenceUnit.ColorChannel):
					desc.summary = "Which color channel to use. For non-RGB lights, use alpha.";
					break;

				case nameof(SetLightSequenceUnit.Step):
					desc.summary = "How position is increased and to how many lights the intensity is applied to.";
					break;
			}
		}
	}
}
