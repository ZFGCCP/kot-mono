using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Graphics
{
    static class CEffects
    {
        private class CEffectWrapper
        {
            public CEffectWrapper(CSprite sprite, Vector2 position, int duration)
            {
                this.sprite = sprite;
                this.position = position;
                this.duration = duration;
            }

            public CSprite sprite;
            public Vector2 position;
            public int duration;
        }

        public static string SMOKE_POOF = "effects:smokePoof";
        public static string EXPLOSION = "effects:explosion";

        private static List<CEffectWrapper> _drawList = new List<CEffectWrapper>();

        public static void createEffect(string effect, Vector2 position, int duration = -1)
        {
            CSprite addToDrawList = new CSprite(effect, false, false, null, true);
            _drawList.Add(new CEffectWrapper(addToDrawList,position,duration));
            addToDrawList = null;
        }

        public static void drawThisShit()
        {
            for (int i = 0; i < _drawList.Count; i++)
            {
                CEffectWrapper effect = _drawList[i];

                if (effect.sprite.draw((int)effect.position.X, (int)effect.position.Y))
                {
                    if (effect.duration < 0)
                        _drawList.Remove(effect);
                    effect.duration -= 1;
                    continue;
                }

                if (effect.duration == 0)
                    _drawList.Remove(effect);
            }
        }
    }
}
