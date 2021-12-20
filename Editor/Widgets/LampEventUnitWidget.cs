using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using VisualPinball.Unity;

namespace VisualPinball.Unity.VisualScripting
{
    [Widget(typeof(LampEventUnit))]
    public sealed class LampEventUnitWidget : UnitWidget<LampEventUnit>
    {
        public LampEventUnitWidget(FlowCanvas canvas, LampEventUnit unit) : base(canvas, unit)
        {
            lampIdInspectorConstructor = (metadata) => new VariableNameInspector(metadata, GetNameSuggestions);
        }

        protected override NodeColorMix baseColor => NodeColorMix.TealReadable;

        private VariableNameInspector lampIdInspector;
        private Func<Metadata, VariableNameInspector> lampIdInspectorConstructor;

        public override Inspector GetPortInspector(IUnitPort port, Metadata metadata)
        {
            if (port == unit.id) {
                InspectorProvider.instance.Renew(ref lampIdInspector, metadata, lampIdInspectorConstructor);

                return lampIdInspector;
            }

            return base.GetPortInspector(port, metadata);
        }

        private IEnumerable<string> GetNameSuggestions()
        {
            var list = new List<string>();

            var tableComponent = TableSelector.Instance.SelectedTable;

            if (tableComponent != null) {
                var gle = tableComponent.gameObject.GetComponent<IGamelogicEngine>();

                if (gle != null) {
                    foreach (var lamp in gle.AvailableLamps) {
                        list.Add(lamp.Id);
                    }
                }
            }

            return list;
        }
    }
}
