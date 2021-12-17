using System;
using System.Linq;
using Unity.VisualScripting;

namespace VisualPinball.Unity.VisualScripting
{
    [UnitTitle("Lamp Group Sequence")]
    [UnitCategory("Visual Pinball")]
    public class LampGroupSequenceUnit : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ValueInput lightGroup;

        [DoNotSerialize]
        public ValueInput maxLights;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        private TableApi _tableApi;
        private int _currentIndex;

        protected override void Definition()
        {
            inputTrigger = ControlInput("In", (flow) => Process(flow));

            lightGroup = ValueInput<LightGroupComponent>(nameof(lightGroup));
            maxLights = ValueInput<int>(nameof(maxLights));

            outputTrigger = ControlOutput("");
        }

        private ControlOutput Process(Flow flow, bool invert = false)
        {
            
            if (_tableApi == null)
            {
                _tableApi = UnityEngine.Object.FindObjectOfType<Player>().TableApi;
            }

            // GetValue throws an exception because they are not basic types

            try
            {
                EnableLightGroup(flow.GetValue<LightGroupComponent>(lightGroup), flow.GetValue<int>(maxLights));
            }
            catch (Exception) { };

            return outputTrigger;
        }

        private void EnableLightGroup(LightGroupComponent lightGroupComponent, int maxLights)
        {
            int index = 0;
            int count = 0;

            bool enabled = false;
   
            var lights = lightGroupComponent.Lights.SelectMany(l => l.GetComponentsInChildren<LightComponent>());

            foreach (var light in lights)
            {
                if (index == _currentIndex)
                {
                    enabled = true;
                }

                if (enabled && ++count > maxLights)
                {
                    enabled = false;
                }

                var lightState = _tableApi.Light(light);
                lightState.State = enabled ? 1 : 0;

                index++;
            }

            _currentIndex += maxLights;

            if (_currentIndex > lights.Count())
            {
                _currentIndex = 0;
            }
        }
    }
}