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
        [PortLabelHidden]
        public ValueInput maxLights;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        private TableApi _tableApi;
        private int _currentIndex = 0;

        protected override void Definition()
        {
            inputTrigger = ControlInput("In", (flow) => Process(flow));

            lightGroup = ValueInput<LightGroupComponent>(nameof(lightGroup));
            maxLights = ValueInput<int>(nameof(maxLights), 1);

            outputTrigger = ControlOutput("");
        }

        private ControlOutput Process(Flow flow, bool invert = false)
        {
            if (_tableApi == null) {
                _tableApi = UnityEngine.Object.FindObjectOfType<Player>().TableApi;
            }

            // GetValue throws an exception because they are not basic types

            try {
                EnableLightGroup(flow.GetValue<LightGroupComponent>(lightGroup), flow.GetValue<int>(maxLights));
            }
            catch (Exception) { };

            return outputTrigger;
        }

        private void EnableLightGroup(LightGroupComponent lightGroupComponent, int maxLights)
        {
            var totalLights = lightGroupComponent.Lights.Count();

            for (var index = 0; index < totalLights; index++ ) { 
                _tableApi.Light(lightGroupComponent.Lights[index].GetComponentInChildren<LightComponent>()).State =
                    (index >= (_currentIndex * maxLights) && index < ((_currentIndex + 1) * maxLights)) ? 1 : 0;
            }

            if (++_currentIndex >= totalLights / maxLights) {
                _currentIndex = 0;
            }
        }
    }
}