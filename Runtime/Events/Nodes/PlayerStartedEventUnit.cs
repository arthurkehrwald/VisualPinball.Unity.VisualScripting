using System;
using Unity.VisualScripting;

namespace VisualPinball.Unity.VisualScripting
{
    [UnitTitle("On Player Started Event")]
    [UnitCategory("Events\\Visual Pinball")]
    public sealed class PlayerStartedEventUnit : EventUnit<EventArgs>
    { 
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.PlayerStartedEvent);
        }

        protected override void Definition()
        {
            base.Definition();
        }
    }
}
