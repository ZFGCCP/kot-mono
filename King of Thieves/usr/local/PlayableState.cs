﻿using Gears.Navigation;
using King_of_Thieves.Actors;
using Gears.Cloud;
using King_of_Thieves.Input;
using King_of_Thieves.Graphics;

namespace King_of_Thieves.usr.local
{
    class PlayableState : MenuReadyGameState
    {
        Actors.Controllers.GameControllers.CDayClock clockTest = Actors.Controllers.GameControllers.CDayClock.instantiateSingleton();

        public PlayableState()
            : base()
        {
            CMasterControl.mapManager.swapMap("tileTester.xml");
            CMasterControl.mapManager.setActorToFollow("player");
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            CMasterControl.mapManager.drawMap();
            CEffects.drawThisShit();
            CMasterControl.healthController.drawMe(spriteBatch);
            CMasterControl.buttonController.drawMe(spriteBatch);
            CMasterControl.magicMeter.drawMe();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            CMasterControl.mapManager.updateMap(gameTime);
            CMasterControl.healthController.update(gameTime);
            CMasterControl.buttonController.update(gameTime);
            CMasterControl.magicMeter.update(gameTime);
            clockTest.update(gameTime);
        }
    }
}
