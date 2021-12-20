using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VisualPinball.Engine.Math;

namespace VisualPinball.Unity.VisualScripting
{
    [UnitTitle("Set Light")]
    [UnitCategory("Visual Pinball")]
    public class SetLightUnit : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput enter { get; private set; }

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput value;

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput colorChannel;

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput gameObjects;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit { get; private set; }

        private Player _player;

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Process);

            gameObjects = ValueInput<List<GameObject>>(nameof(gameObjects));

            value = ValueInput<float>(nameof(value), 0);
            colorChannel = ValueInput(nameof(colorChannel), ColorChannel.Alpha);
           
            exit = ControlOutput(nameof(exit));
        }

        private ControlOutput Process(Flow flow)
        {
            if (_player == null) {
                _player = UnityEngine.Object.FindObjectOfType<Player>();
            }

            var valueRaw = flow.GetValue<float>(value);
            var colorChannelRaw = flow.GetValue<ColorChannel>(colorChannel);

            foreach (var go in flow.GetValue<List<GameObject>>(gameObjects)) {
                if (go != null) {
                    foreach (var lamp in go.GetComponentsInChildren<ILampDeviceComponent>()) {
                        _player.Lamp(lamp).OnLamp(valueRaw, colorChannelRaw);
                    }
                }
            }

            return exit;
        }
    }
}