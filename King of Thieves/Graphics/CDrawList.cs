using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Graphics;
using King_of_Thieves.Actors;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Graphics
{
    class CDrawList
    {
        private SortedDictionary<int, List<CActor>> _drawList; 

        public CDrawList()
        {
            _drawList = new SortedDictionary<int, List<CActor>>();
        }

        public void addSpriteToList(int depth, CActor sprite)
        {
            if (!_drawList.ContainsKey(depth))
                _drawList.Add(depth, new List<CActor>());

            _drawList[depth].Add(sprite);
        }

        public void addSpriteToList(CActor[] sprites)
        {
            foreach (CActor sprite in sprites)
                addSpriteToList(sprite.drawDepth,sprite);
            
        }

        public void removeSpriteFromDrawList(CActor sprite)
        {
            int depth = sprite.drawDepth;
            if (!_drawList.ContainsKey(depth))
                return;

            _drawList[depth].Remove(sprite);
        }

        public void changeSpriteDepth(CActor sprite, int currentDepth, int newDepth)
        {
            _drawList[currentDepth].Remove(sprite);

            if (!_drawList.ContainsKey(newDepth))
                _drawList.Add(newDepth, new List<CActor>());

            _drawList[newDepth].Add(sprite);
            sprite.drawDepth = newDepth;
        }

        public void drawAll(int layer, SpriteBatch spriteBatch = null)
        {
            foreach (KeyValuePair<int, List<CActor>> kvp in _drawList)
            {
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    CActor sprite = kvp.Value[i];

                    if (sprite.layer == layer)
                    {
                        if (sprite.killMe)
                        {
                            kvp.Value.Remove(sprite);
                            continue;
                        }
                        sprite.drawMe(false,spriteBatch);
                    }
                }
            }
        }
    }
}
