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
using Unity.VisualScripting;
using UnityEngine;

namespace VisualPinball.Unity.VisualScripting
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(IGamelogicEngine))]
	[RequireComponent(typeof(Player))]
	[AddComponentMenu("Visual Pinball/Game Logic Engine/Visual Scripting Bridge")]
	public class VisualScriptingGamelogicBridge : MonoBehaviour
	{
		private IGamelogicEngine _gle;
		private Player _player;

		private void Awake()
		{
			_gle = GetComponent<IGamelogicEngine>();
			_player = GetComponent<Player>();
			if (_gle == null) {
				Debug.LogWarning("Cannot find gamelogic engine.");
				return;
			}
			if (_player == null) {
				Debug.LogWarning("Cannot find player.");
				return;
			}

			_gle.OnSwitchChanged += OnSwitchChanged;
			_gle.OnCoilChanged += OnCoilChanged;
			_gle.OnLampChanged += OnLampChanged;
			_player.OnPlayerStarted += OnPlayerStarted;
		}

		private void OnDestroy()
		{
			if (_player != null) {
				_player.OnPlayerStarted -= OnPlayerStarted;
			}
			if (_gle != null) {
				_gle.OnSwitchChanged -= OnSwitchChanged;
				_gle.OnCoilChanged -= OnCoilChanged;
				_gle.OnLampChanged -= OnLampChanged;
			}
		}

		private static void OnSwitchChanged(object sender, SwitchEventArgs2 e)
		{
			EventBus.Trigger(VisualScriptingEventNames.SwitchEvent, new SwitchEventArgs2(e.Id, e.IsEnabled));
		}

		private static void OnCoilChanged(object sender, CoilEventArgs e)
		{
			EventBus.Trigger(VisualScriptingEventNames.CoilEvent, e);
		}

		private static void OnPlayerStarted(object sender, EventArgs e)
		{
			EventBus.Trigger(VisualScriptingEventNames.PlayerStartedEvent, EventArgs.Empty);
		}

		private static void OnLampChanged(object sender, LampEventArgs e)
		{
			EventBus.Trigger(VisualScriptingEventNames.LampEvent, e);
		}

	}
}
