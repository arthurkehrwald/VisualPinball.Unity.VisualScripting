using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace VisualPinball.Unity.VisualScripting
{
    [UnitTitle("Lamp Group")]
    [UnitCategory("Visual Pinball")]
    public class LampGroupUnit : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlInput inputInvertTrigger;

        [DoNotSerialize]
        public ValueInput offLightGroup;

        [DoNotSerialize]
        public ValueInput onLightGroup;

        [DoNotSerialize]
        public ValueInput offLightGroups;

        [DoNotSerialize]
        public ValueInput onLightGroups;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        private TableApi _tableApi;

        protected override void Definition()
        {
            inputTrigger = ControlInput("In", (flow) => Process(flow));
            inputInvertTrigger = ControlInput("In (Invert)", (flow) => Process(flow, true));

            offLightGroup = ValueInput<LightGroupComponent>(nameof(offLightGroup));
            onLightGroup = ValueInput<LightGroupComponent>(nameof(onLightGroup));

            offLightGroups = ValueInput<List<LightGroupComponent>>(nameof(offLightGroups));
            onLightGroups = ValueInput<List<LightGroupComponent>>(nameof(onLightGroups));

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
                foreach (var lightGroupComponent in flow.GetValue<List<LightGroupComponent>>(offLightGroups))
                {
                    EnableLightGroup(lightGroupComponent, invert);
                }
            }
            catch (Exception) { };

            try
            {
                foreach (var lightGroupComponent in flow.GetValue<List<LightGroupComponent>>(onLightGroups))
                {
                    EnableLightGroup(lightGroupComponent, !invert);
                }
            }
            catch (Exception) { };

            try
            {
                EnableLightGroup(flow.GetValue<LightGroupComponent>(offLightGroup), invert);
            }
            catch (Exception) { };

            try
            {
                EnableLightGroup(flow.GetValue<LightGroupComponent>(onLightGroup), !invert);
            }
            catch (Exception) { };

            return outputTrigger;
        }

        private void EnableLightGroup(LightGroupComponent lightGroupComponent, bool enable)
        {
            foreach (var light in lightGroupComponent.Lights.SelectMany(l => l.GetComponentsInChildren<LightComponent>()))
            {
                var lightState = _tableApi.Light(light);
                lightState.State = enable ? 1 : 0;
            }
        }
    }
}