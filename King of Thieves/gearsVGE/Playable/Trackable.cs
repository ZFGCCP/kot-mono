/**
 * Implementation of the class Trackable
 * 
 * @author Steven E. Barbaro
 * @date November 12th, 2011
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gears.Cloud;

namespace Gears.Playable
{
    public abstract class Trackable
    {
        private Guid uuid_ = Guid.NewGuid();

        public string getUUID()
        {
            return this.uuid_.ToString();
        }

        public Trackable()
        {
            // Add this item to the EntityTracker
            EntityTracker.trackEntity(this);   
        }

        ~Trackable()
        {
            // Remove this item from the EntityTracker
            EntityTracker.forgetEntity(this);
        }
    }
}
