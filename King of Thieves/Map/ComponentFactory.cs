using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Map
{
    class ComponentFactory : Gears.Playable.UnitTypeFactory
    {
        public ComponentFactory(King_of_Thieves.Actors.CComponent[] components)
        {
            base.Register(components);
        }

        public void addComponent(Actors.CComponent component)
        {
            AddUnit(component);
        }

        public void removeComponent(Actors.CComponent component)
        {
            RemoveUnit(component);
        }

        //public Actors.CComponent components
        //{
        //    get
        //    {
        //        return base.units;
        //    }
        //}
    }
}
