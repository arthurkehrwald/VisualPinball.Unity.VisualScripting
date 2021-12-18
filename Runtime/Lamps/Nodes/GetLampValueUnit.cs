using Unity.VisualScripting;

namespace VisualPinball.Unity.VisualScripting
{
    [UnitTitle("Get Lamp Value")]
    [UnitCategory("Visual Pinball")]
    public class GetLampValueUnit : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput id { get; private set; }

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput value { get; private set; }

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput enabled { get; private set; }

        private Player _player;

        protected override void Definition()
        {
            id = ValueInput(nameof(id), string.Empty);

            value = ValueOutput(nameof(value), GetValue);
            enabled = ValueOutput(nameof(enabled), GetEnabled);
        }

        private float GetValue(Flow flow)
        {
            if (_player == null) {
                _player = UnityEngine.Object.FindObjectOfType<Player>();
            }

            var key = flow.GetValue<string>(id);
            return _player.LampStatuses.ContainsKey(key) ? _player.LampStatuses[key] : 0;
        }

        private bool GetEnabled(Flow flow)
        {
            if (_player == null) {
                _player = UnityEngine.Object.FindObjectOfType<Player>();
            }

            var key = flow.GetValue<string>(id);
            return _player.LampStatuses.ContainsKey(key) ? (_player.LampStatuses[key] > 0) : false;
        }
    }
}