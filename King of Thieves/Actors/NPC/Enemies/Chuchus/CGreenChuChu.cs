using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Chuchus
{
    public class CGreenChuChu : CBaseChuChu
    {

        public CGreenChuChu(int sight, float fov, int foh, params dropRate[] drops)
            : base(sight, fov, foh, drops)
        {
            _position.X = _randNum.Next(0,200);
            _position.Y = _randNum.Next(0, 200);
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
            _imageIndex.Add("chuChuWobble", new Graphics.CSprite("chuChu:Wobble"));
            _imageIndex.Add("chuChuPopUp", new Graphics.CSprite("chuChu:PopUp"));
            _imageIndex.Add("chuChuIdle", new Graphics.CSprite("chuChu:Idle"));
            _imageIndex.Add("chuChuHop", new Graphics.CSprite("chuChu:Hop"));
            _imageIndex.Add("chuChuPopDown", new Graphics.CSprite("chuChu:PopDown"));
        }

        protected override void applyEffects()
        {
            throw new NotImplementedException();
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }
    }
}
