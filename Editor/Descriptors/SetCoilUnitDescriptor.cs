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
	[Descriptor(typeof(SetCoilUnit))]
	public class SetCoilUnitDescriptor : UnitDescriptor<SetCoilUnit>
	{
		public SetCoilUnitDescriptor(SetCoilUnit target) : base(target)
		{
		}

		protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
		{
			base.DefinedPort(port, description);

			switch (port.key)
			{
				case nameof(SetCoilUnit.Id):
					description.summary = "The ID of the coil to be set.";
					break;
				case nameof(SetCoilUnit.IsEnabled):
					description.summary = "The value to assign to the coil.";
					break;
			}
		}
	}
}