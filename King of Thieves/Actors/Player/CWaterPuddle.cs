using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Player
{
    class CWaterPuddle : CActor
    {
        private bool _wading = false;

        public CWaterPuddle() :
            base()
        {
            _imageIndex.Add(Graphics.CTextures.PLAYER_PUDDLE, new Graphics.CSprite(Graphics.CTextures.PLAYER_PUDDLE));
            _followRoot = true;
            _state = ACTOR_STATES.INVISIBLE;
            swapImage(Graphics.CTextures.PLAYER_PUDDLE);
            _hitBox = new Collision.CHitBox(this, 12, 25, 10, 10);

            startTimer0(30);
        }

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Collision.Water.CShallowWater));
        }

        public override void timer0(object sender)
        {
            if (_wading && _state != ACTOR_STATES.INVISIBLE)
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Background:waterWade"]);

            startTimer0(15);
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            _wading = false;
        }

        public override void keyDown(object sender)
        {
            Input.CInput input = (Input.CInput)sender;

            _wading = input.keysPressed.Contains(input.getKey(Input.CInput.KEY_WALK_DOWN)) ||
                      input.keysPressed.Contains(input.getKey(Input.CInput.KEY_WALK_UP)) ||
                      input.keysPressed.Contains(input.getKey(Input.CInput.KEY_WALK_LEFT)) ||
                      input.keysPressed.Contains(input.getKey(Input.CInput.KEY_WALK_RIGHT));
        }             
    }
}
