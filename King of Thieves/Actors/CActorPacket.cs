using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors
{
    enum RESERVED_COMMANDS
    {
        KILL = 1000
    }

    struct CActorPacket
    {
        private int _userEventID;
        private string _actorName;
        private List<object> _parameters;
        private CActor _sender;

        public CActorPacket(int userEvent, string actor, CActor sender, params object[] param)
        {
            _userEventID = userEvent;
            _actorName = actor;
            _sender = sender;
            _parameters = new List<object>();
            if (param.Count() > 0)
                _parameters.AddRange(param);
        }

        public object getParam(int index)
        {
            return _parameters[index];
        }

        public CActor sender
        {
            get
            {
                return _sender;
            }
        }

        public object[] getParams()
        {
            return _parameters.ToArray();
        }

        public int userEventID
        {
            get
            {
                return _userEventID;
            }
        }

        public string actor
        {
            get
            {
                return _actorName;
            }
        }
    }
}
