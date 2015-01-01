using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gears.Cloud.Events.Triggers
{
    class CFrameTrigger : CBaseEventTrigger
    {
        static CFrameTrigger Singleton = null;

        public void Update()
        {
            signalHandler(Events[0]);
        }

        public static CFrameTrigger initialize()
        {
            if (Singleton == null)
            {
                Singleton = new CFrameTrigger();
            }

            return Singleton;
        }
        private CFrameTrigger()
        {
            CEvent frameEvent = new CEvent();

            Events.Add(frameEvent);
        }
    }
}
