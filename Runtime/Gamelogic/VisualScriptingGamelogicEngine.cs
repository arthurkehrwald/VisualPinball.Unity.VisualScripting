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
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using VisualPinball.Engine.Game.Engines;

namespace VisualPinball.Unity.VisualScripting
{
	public class VisualScriptingGamelogicEngine : MonoBehaviour, IGamelogicEngine
	{
		public string Name { get; } = "Visual Scripting Gamelogic Engine";

		[Tooltip("The switches that are exposed in the Visual Scripting nodes.")]
		public VisualScriptingSwitch[] Switches;
		public VisualScriptingCoil[] Coils;
		public GamelogicEngineLamp[] Lamps;
		public GamelogicEngineWire[] Wires;

		public GamelogicEngineSwitch[] AvailableSwitches => Switches;

		public GamelogicEngineLamp[] AvailableLamps => Lamps;

		public GamelogicEngineCoil[] AvailableCoils => Coils;

		public GamelogicEngineWire[] AvailableWires => Wires;

		public event EventHandler<AvailableDisplays> OnDisplaysAvailable;
		public event EventHandler<DisplayFrameData> OnDisplayFrame;
		public event EventHandler<LampEventArgs> OnLampChanged;
		public event EventHandler<LampsEventArgs> OnLampsChanged;
		public event EventHandler<LampColorEventArgs> OnLampColorChanged;
		public event EventHandler<CoilEventArgs> OnCoilChanged;

		public void OnInit(Player player, TableApi tableApi, BallManager ballManager)
		{
		}

		public void SetCoil(string id, bool isEnabled)
		{
			OnCoilChanged?.Invoke(this, new CoilEventArgs(id, isEnabled));
		}

		public void Switch(string id, bool isClosed)
		{
			EventBus.Trigger(EventNames.SwitchEvent, new VisualScriptingScriptEvent(id, isClosed));
		}
	}
}
