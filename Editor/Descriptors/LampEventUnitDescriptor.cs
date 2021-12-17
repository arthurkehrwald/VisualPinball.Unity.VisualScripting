using Unity.VisualScripting;

namespace VisualPinball.Unity.VisualScripting
{
    [Descriptor(typeof(LampEventUnit))]
    public class LampEventUnitDescriptor : UnitDescriptor<LampEventUnit>
    {
        public LampEventUnitDescriptor(LampEventUnit target) : base(target)
        {
        }

        protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
        {
            base.DefinedPort(port, description);

            description.showLabel = false;
        }
    }
}
