// Visual Pinball Engine
// Copyright (C) 2021 freezy and VPE Team
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace VisualPinball.Unity.VisualScripting
{
	[UnitTitle("On Switch Changed")]
	[UnitCategory("Events\\Visual Pinball")]
	public class SwitchEventUnit : EventUnit<SwitchEventArgs>
	{
		[DoNotSerialize]
		[PortLabel("Switch ID")]
		public ValueInput id { get; private set; }

		[DoNotSerialize]
		[PortLabel("Is Enabled")]
		public ValueOutput enabled { get; private set; }

		public VisualScriptingGamelogicEngine gle { get; private set; }

		[DoNotSerialize]
		public readonly List<string> Errors = new();

		protected override bool register => true;

		// Adding an EventHook with the name of the event to the list of visual scripting events.
		public override EventHook GetHook(GraphReference reference)
		{
			return new EventHook(EventNames.SwitchEvent);
		}

		protected override void Definition()
		{
			base.Definition();

			id = ValueInput(nameof(id), string.Empty);
			enabled = ValueOutput<bool>(nameof(enabled));
		}

		public override void Instantiate(GraphReference instance)
		{
			base.Instantiate(instance);
			Debug.Log("Switch event instantiated.");
		}

		protected override void AssignArguments(Flow flow, SwitchEventArgs args)
		{
			flow.SetValue(enabled, args.IsEnabled);
		}
	}
}
