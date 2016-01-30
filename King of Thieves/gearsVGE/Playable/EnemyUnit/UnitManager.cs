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
    public abstract class UnitManager : IManager
    {
        private UnitTypeFactory[] _factories;

        public UnitManager()
        {

        }
        private void Initialize()
        {
            //
        }
        public int factorySize(int factory)
        {
            return _factories[factory].unitsSize;
        }
        public void AddUnit(Unit unit, int factory)
        {
            _factories[factory].AddUnit(unit);
        }
        public void RemoveUnit(Unit unit, int factory)
        {
            _factories[factory].RemoveUnit(unit);
        }
        public void disposeFactories()
        {
            foreach (UnitTypeFactory utf in _factories)
            {
                utf.disposeUnits();
            }
            _factories = null;
        }
        public void Update(GameTime gameTime)
        {
            foreach (UnitTypeFactory utf in _factories)
            {
                utf.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (UnitTypeFactory utf in _factories)
            {
                utf.Draw(spriteBatch);
            }
        }
        protected internal void Register(UnitTypeFactory[] factories)
        {
            _factories = factories;
            Initialize();
        }
    }
}
