using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace King_of_Thieves
{
    static class CMasterControl
    {
        public static Actors.HUD.health.CHealthController healthController;
        public static Actors.HUD.buttons.CButtonController buttonController;
        public static Sound.CAudioPlayer audioPlayer;
        public static Map.CMapManager mapManager = new Map.CMapManager();
        public static GameTime gameTime;
        public static Dictionary<int, List<Actors.CActorPacket>> commNet = new Dictionary<int, List<Actors.CActorPacket>>();
        public static ContentManager glblContent;
        public static Input.CInput glblInput;
        public static Graphics.CCamera camera = new Graphics.CCamera();
        
    }
}
