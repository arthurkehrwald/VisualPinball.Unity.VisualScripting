using Unity.VisualScripting;

namespace VisualPinball.Unity.VisualScripting
{
    public static class EventNames
    {
        public static string LampEvent = "LampEvent";
    }

    [UnitTitle("On Lamp Event")]
    [UnitCategory("Events\\Visual Pinball")]
    public sealed class LampEventUnit : EventUnit<LampEventArgs>
    { 
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput id { get; private set; }

        [DoNotSerialize]
        public ValueOutput value { get; private set; }

        [DoNotSerialize]
        public ValueOutput enabled { get; private set; }

        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.LampEvent);
        }

        protected override void Definition()
        {
            base.Definition();

            id = ValueInput(nameof(id), string.Empty);

            value = ValueOutput<int>(nameof(value));
            enabled = ValueOutput<bool>(nameof(enabled));
        }

        protected override void AssignArguments(Flow flow, LampEventArgs args)
        {
            flow.SetValue(value, args.Value);
            flow.SetValue(enabled, args.Value > 0);
        }

        protected override bool ShouldTrigger(Flow flow, LampEventArgs args)
        {
            return args.Id == flow.GetValue<string>(id);
        }
    }
}