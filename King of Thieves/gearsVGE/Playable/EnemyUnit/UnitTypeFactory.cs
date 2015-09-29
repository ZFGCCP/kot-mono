using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

namespace Gears.Playable
{
    public abstract class UnitTypeFactory
    {
        //private Unit[] _units; //phase this out
        private List<Unit> _units;

        public UnitTypeFactory()
        {
            //
        }
        private void Initialize()
        {
            //LoadContent(); //once again the design pattern doesn't need this.
        }
        private void LoadContent()
        {
            //foreach (Unit u in _units)
            //{
            //    u.LoadContent();
            //}
        }
        protected List<Unit> units
        {
            get
            {
                return _units;
            }
        }
        public void Update(GameTime gameTime)
        {
            if (_units != null)
            {
                for (int i = 0; i < _units.Count; i++)
                {
                    _units[i].Update(gameTime);
                }
            }
            //else
            //{
            //    throw new Exception("DESIGNER ERROR: Gears.Playable.UnitTypeFactory: No units assigned. _units is null.");
            //}
        }

        public void AddUnit(Unit unit)
        {
            if (!_units.Contains(unit))
            {
                _units.Add(unit);
            }
            else
            {
                throw new InvalidOperationException("Unit " + unit.ToString() + " already exists!");
            }
        }

        public void RemoveUnit(Unit unit)
        {
            try
            {
                _units.Remove(unit);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new InvalidOperationException("Could not find Unit " + unit.ToString(), ex);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_units != null)
            {
                foreach (Unit u in _units)
                {
                    u.Draw(spriteBatch);
                }
            }
            //else
            //{
            //    throw new Exception("DESIGNER ERROR: Gears.Playable.UnitTypeFactory: No units assigned. _units is null.");
            //}
        }
        //TODO: Phase this out!!
        protected internal void Register(Unit[] units)
        {
            _units = units.ToList();
            //Initialize();
        }
        //protected internal void Register(List<Unit> units)
        //{
        //    _units = units;
        //    //Initialize();
        //}
        protected internal void Register(IEnumerable<Unit> units)
        {
            _units = units.ToList();
            //Initialize();
        }
        public int unitsSize
        {
            get
            {
                return _units.Count;
            }
        }
    }
}
