using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using VisualPinball.Engine.Math;

namespace VisualPinball.Unity.VisualScripting
{
    [UnitTitle("Set Light Sequence")]
    [UnitCategory("Visual Pinball")]
    public class SetLightSequenceUnit : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput enter { get; private set; }

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput gameObjects;

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput value;

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput colorChannel;

        [DoNotSerialize]
        public ValueInput step;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit { get; private set; }

        private Player _player;
        private int _currentIndex = 0;

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Process);

            gameObjects = ValueInput<List<GameObject>>(nameof(gameObjects));

            value = ValueInput<float>(nameof(value), 0);
            colorChannel = ValueInput(nameof(colorChannel), ColorChannel.Alpha);
            step = ValueInput<int>(nameof(step), 1);

            exit = ControlOutput(nameof(exit));
        }

        private ControlOutput Process(Flow flow)
        {
            if (_player == null) {
                _player = UnityEngine.Object.FindObjectOfType<Player>();
            }

            var valueRaw = flow.GetValue<float>(value);
            var colorChannelRaw = flow.GetValue<ColorChannel>(colorChannel);
            var stepRaw = flow.GetValue<int>(step);

            var lights = new List<ILampDeviceComponent>();

            foreach (var go in flow.GetValue<List<GameObject>>(gameObjects)) {
                if (go != null) {
                    foreach (var lamp in go.GetComponentsInChildren<ILampDeviceComponent>()) {
                        if (lamp is LightGroupComponent) {
                            lights.AddRange(((LightGroupComponent)lamp).Lights);
                        }
                        else {
                            lights.Add(lamp);
                        }
                    }
                }
            }

            for (var index = 0; index < lights.Count(); index++) {
                _player.Lamp(lights[index]).OnLamp(
                     (index >= (_currentIndex * stepRaw) && index < ((_currentIndex + 1) * stepRaw)) ? valueRaw : 0,
                     colorChannelRaw);
            }

            if (++_currentIndex >= lights.Count() / stepRaw) {
                _currentIndex = 0;
            }

            return exit;
        }
    }
}