/// <summary>
///     Implementation of the class EntityTracker
///      
///     This is a singleton that maintains a dictionary of unique id string to Trackable reference pairs. 
///     Global access and retrieval of contained Trackable instances from the dictionary is available.
///     Trackable instances are added implicitly to this class on construction and removed on destruction.
///     A Trackable object can also be removed from the internal dictionary by calling forgetEntity().
///     
///     @author Steven E. Barbaro
///     @date November 13th, 2011
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gears.Playable;

namespace Gears.Cloud
{
    public static class EntityTracker
    {

        // A dictionary pairing unique id strings to Trackable object references.
        private static Dictionary<string, Trackable> _entities = _entities = new Dictionary<string, Trackable>();


        /// <summary>
        ///     Adds a string to Trackable pair for the passed entity to the dictionary _entities.
        ///     
        ///     If a pair already exists this method does nothing.
        /// </summary>
        /// <param name="entity">The entity to create the pair for.</param> 
        /// <seealso cref="getTrackedEntity"/>  
        /// <seealso cref="forgetEntity"/>
        public static void trackEntity(Trackable entity)
        {
            // If the entity is valid and not already present in the dictionary
            if (entity != null && EntityTracker.getTrackedEntity(entity.getUUID()) == null)
            {
                // Add this entity to the dictionary
                _entities.Add(entity.getUUID(), entity);
            }
        }

        /// <summary>
        ///     Removes the string to Trackable pair for the passed entity from the dictionary _entities.
        /// </summary>
        /// <param name="entity">The entity reference to remove from the dictionary.</param>
        /// <seealso cref="getTrackedEntity"/>  
        /// <seealso cref="trackEntity"/>
        public static void forgetEntity(Trackable entity)
        {
            // If the entity is valid
            if (entity != null)
            {
                // Remove this entity from the dictionary
                _entities.Remove(entity.getUUID());
            }
        }

        /// <summary>
        ///     Returns a Trackable reference paired in the dictionary _entities with the passed id.
        ///     If a match is not found this method returns null.
        /// </summary>
        /// <param name="id">The UUID string to search in the dictionary for the pair.</param>
        /// <seealso cref="forgetEntity"/>  
        /// <seealso cref="trackEntity"/>
        public static Trackable getTrackedEntity(string id)
        {
            // Default the return reference to null for fail case.
            Trackable return_object = null;

            // Retrieve an entity in the dictionary paired with the passed id. If no match is found return_object remains null.
            _entities.TryGetValue(id, out return_object);

            return return_object;
        }
    }
}
