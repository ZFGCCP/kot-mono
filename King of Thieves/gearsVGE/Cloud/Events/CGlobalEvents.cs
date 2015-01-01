using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gears.Cloud.Events.Triggers;

namespace Gears.Cloud.Events
{
    class CGlobalEvents
    {
        public static Triggers.CFrameTrigger GFrameTrigger = Triggers.CFrameTrigger.initialize();
    }
}
