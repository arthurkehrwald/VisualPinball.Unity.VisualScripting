using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NLog;
using UnityEngine;
using VisualPinball.Engine.Game.Engines;
using VisualPinball.Unity;
using Logger = NLog.Logger;

namespace VisualPinball.Unity.VisualScripting
{
	public class VisualScriptingGamelogicEngine : MonoBehaviour, IGamelogicEngine
	{
		public string Name { get; } = "Visual Scripting Gamelogic Engine";

        public GamelogicEngineSwitch[] AvailableSwitches => new GamelogicEngineSwitch[0];

        public GamelogicEngineLamp[] AvailableLamps => new GamelogicEngineLamp[0];

        public GamelogicEngineCoil[] AvailableCoils => new GamelogicEngineCoil[0];

        public GamelogicEngineWire[] AvailableWires => new GamelogicEngineWire[0];

        public event EventHandler<AvailableDisplays> OnDisplaysAvailable;
        public event EventHandler<DisplayFrameData> OnDisplayFrame;
        public event EventHandler<LampEventArgs> OnLampChanged;
        public event EventHandler<LampsEventArgs> OnLampsChanged;
        public event EventHandler<LampColorEventArgs> OnLampColorChanged;
        public event EventHandler<CoilEventArgs> OnCoilChanged;

        public void OnInit(Player player, TableApi tableApi, BallManager ballManager)
        {
      
        }

        public void Switch(string id, bool isClosed)
        {
        }
    }
}
