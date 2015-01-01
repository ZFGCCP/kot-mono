using System.Collections.Generic;

namespace Gears.Cloud.Events
{
    class CBaseEventTrigger
    {
        protected List<CEvent> Events;

        public CBaseEventTrigger() 
        {
            Events = new List<CEvent>();
        }

        public void signalHandler(CEvent ev)
        {
            ev.triggered = true;
        }

        public CEvent getEvent(int index)
        {
            return Events[index];
        }

        protected void AddEvent(CEvent ev)
        {
            Events.Add(ev);
        }
    }
}
