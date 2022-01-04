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
using UnityEditor;
using UnityEngine;
using VisualPinball.Unity.Editor;

namespace VisualPinball.Unity.VisualScripting.Editor
{
	public class ObjectPickerInspector<T> : Inspector where T: class, IIdentifiableItemComponent
	{
		private readonly ObjectReferencePicker<T> _devicePicker;

		public ObjectPickerInspector(Metadata metadata, string title, TableComponent tableComponent) : base(metadata)
		{
			_devicePicker = new ObjectReferencePicker<T>(title, tableComponent, true);
		}

		protected override float GetHeight(float width, GUIContent label) => EditorGUIUtility.singleLineHeight;

		protected override void OnGUI(Rect position, GUIContent label)
		{
			position = BeginLabeledBlock(metadata, position, label);
			_devicePicker.Render(position, (T)metadata.value, component => {
				if (EndBlock(metadata)) {
					metadata.RecordUndo();
					metadata.value = component;
				}
			});
		}
	}
}
