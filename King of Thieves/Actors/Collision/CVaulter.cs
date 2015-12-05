using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Collision
{
    class CVaulter : CActor
    {
        private int _airTime = 0;
        private Vector2 _vaultDirection = Vector2.Zero;

        public CVaulter() : base()
        {

        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            _hitBox = new CHitBox(this, 0, 0, Convert.ToInt32(additional[0]), Convert.ToInt32(additional[1]));
            _airTime = Convert.ToInt32(additional[2]);

            _vaultDirection = new Vector2(Convert.ToInt32(additional[3]), Convert.ToInt32(additional[4]));
        }

        public int airTime
        {
            get
            {
                return _airTime;
            }
        }

        public Vector2 vaultDirection
        {
            get
            {
                return _vaultDirection;
            }
        }
            
    }
}
