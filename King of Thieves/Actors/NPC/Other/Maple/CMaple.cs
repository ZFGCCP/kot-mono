using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gears.Cloud;
using King_of_Thieves.Input;
using Microsoft.Xna.Framework.Input;
using King_of_Thieves.Actors.Collision;

namespace King_of_Thieves.Actors.NPC.Other.Maple
{
    class CMaple : CActor
    {
        public CMaple() : base()
        {
            _name = "Maple";
            _position = Vector2.Zero;

            image = _imageIndex["MapleWalkDown"];
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
            _imageIndex.Add("MapleWalkDown", new Graphics.CSprite("Npc:Maple:WalkDown"));
            _imageIndex.Add("MapleWalkLeft", new Graphics.CSprite("Npc:Maple:WalkLeft"));
            _imageIndex.Add("MapleWalkRight", new Graphics.CSprite("Npc:Maple:WalkLeft",true));
            _imageIndex.Add("MapleWalkUp", new Graphics.CSprite("Npc:Maple:WalkUp"));

            _imageIndex.Add("MapleIdleDown", new Graphics.CSprite("Npc:Maple:IdleDown"));
            _imageIndex.Add("MapleIdleLeft", new Graphics.CSprite("Npc:Maple:IdleLeft"));
            _imageIndex.Add("MapleIdleRight", new Graphics.CSprite("Npc:Maple:IdleLeft", true));
            _imageIndex.Add("MapleIdleUp", new Graphics.CSprite("Npc:Maple:IdleUp"));
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            Vector2 position = (Vector2)Map.CMapManager.propertyGetter("player", Map.EActorProperties.POSITION);

            _direction = moveToPoint(position.X, position.Y, .5f);

            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage("MapleWalkDown", false);
                    break;

                case DIRECTION.UP:
                    swapImage("MapleWalkUp", false);
                    break;

                case DIRECTION.LEFT:
                    swapImage("MapleWalkLeft", false);
                    break;

                case DIRECTION.RIGHT:
                    swapImage("MapleWalkRight", false);
                    break;
            }
        }
    }
}
