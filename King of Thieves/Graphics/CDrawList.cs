using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Graphics;
using King_of_Thieves.Actors;

namespace King_of_Thieves.Graphics
{
    class CDrawList
    {
        private Dictionary<int, List<CActor>> _drawList;

        public CDrawList()
        {
            _drawList = new Dictionary<int, List<CActor>>();
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

        public void drawAll(int layer)
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
                        sprite.drawMe();
                    }
                }
            }
        }
    }
}
