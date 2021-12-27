using System;
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

        public GamelogicEngineSwitch[] AvailableSwitches => throw new NotImplementedException();

        public GamelogicEngineLamp[] AvailableLamps => throw new NotImplementedException();

        public GamelogicEngineCoil[] AvailableCoils => throw new NotImplementedException();

        public GamelogicEngineWire[] AvailableWires => throw new NotImplementedException();

        public event EventHandler<AvailableDisplays> OnDisplaysAvailable;
        public event EventHandler<DisplayFrameData> OnDisplayFrame;
        public event EventHandler<LampEventArgs> OnLampChanged;
        public event EventHandler<LampsEventArgs> OnLampsChanged;
        public event EventHandler<LampColorEventArgs> OnLampColorChanged;
        public event EventHandler<CoilEventArgs> OnCoilChanged;

        public void OnInit(Player player, TableApi tableApi, BallManager ballManager)
        {
            throw new NotImplementedException();
        }

        public void Switch(string id, bool isClosed)
        {
            throw new NotImplementedException();
        }
    }
}
