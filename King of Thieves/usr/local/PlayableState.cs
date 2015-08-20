using Gears.Navigation;
using King_of_Thieves.Actors;
using Gears.Cloud;
using King_of_Thieves.Input;
using King_of_Thieves.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.usr.local
{
    class PlayableState : MenuReadyGameState
    {
        Actors.Controllers.GameControllers.CDayClock clockTest = Actors.Controllers.GameControllers.CDayClock.instantiateSingleton();

        public PlayableState()
            : base()
        {
            CMasterControl.mapManager.swapMap("thieves_hideout_f1.xml","player",new Vector2(265,135));
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            CMasterControl.mapManager.drawMap();
            CEffects.drawThisShit();
            CMasterControl.healthController.drawMe(spriteBatch);
            CMasterControl.buttonController.drawMe(spriteBatch);
            CMasterControl.magicMeter.drawMe();

            if (CMasterControl.pickPocketMeter != null)
                CMasterControl.pickPocketMeter.drawMe();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            CMasterControl.mapManager.updateMap(gameTime);
            CMasterControl.healthController.update(gameTime);
            CMasterControl.buttonController.update(gameTime);
            CMasterControl.magicMeter.update(gameTime);
            //clockTest.update(gameTime);

            if (CMasterControl.pickPocketMeter != null)
                CMasterControl.pickPocketMeter.update(gameTime);
        }
    }
}
