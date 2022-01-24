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

using UnityEditor;
using UnityEngine;

namespace VisualPinball.Unity.VisualScripting.Editor
{
	public abstract class VisualScriptingDeviceItemPropertyDrawer : PropertyDrawer
	{
		private const float Padding = 2f;

		protected abstract string NameName { get; }
		protected abstract string DescName { get; }

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight + Padding;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			var nameProperty = property.FindPropertyRelative(NameName);
			var descProperty = property.FindPropertyRelative(DescName);

			position.y += 2f;
			position.height = EditorGUIUtility.singleLineHeight;

			const float space = 4f;
			var leftWidth = position.width / 3f;
			var rightWidth = position.width / 3f * 2f;
			var labelWidth = 20f;
			var halfPos = position;
			halfPos.width = leftWidth - labelWidth - space;
			var labelPos = halfPos;
			halfPos.x += labelWidth;
			labelPos.width = labelWidth;
			EditorGUI.LabelField(labelPos, "ID:");
			EditorGUI.PropertyField(halfPos, nameProperty, GUIContent.none);

			labelWidth = 42f;
			halfPos = position;
			halfPos.width = rightWidth - labelWidth;
			labelPos = halfPos;
			labelPos.x += leftWidth;
			halfPos.x += leftWidth + labelWidth;
			labelPos.width = labelWidth;
			EditorGUI.LabelField(labelPos, "Descr:");
			EditorGUI.PropertyField(halfPos, descProperty, GUIContent.none);

			EditorGUI.EndProperty();
		}
	}
}
