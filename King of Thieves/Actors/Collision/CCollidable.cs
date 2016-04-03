using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using King_of_Thieves.MathExt;

namespace King_of_Thieves.Actors.Collision
{
    class CCollidable : CActor
    {
        private int _height;
        private int _width;

        private double _angleTopRight;
        private double _angleBottomRight;
        private double _angleBottomLeft;
        private double _angleTopLeft;

        private double _distToTopLeft;
        private double _distToTopRight;
        private double _distToBottomRight;
        private double _distToBottomLeft;

        private CTriangle _leftTri;
        private CTriangle _rightTri;
        private CTriangle _topTri;
        private CTriangle _bottomTri;

        public CCollidable() :
            base()
        {

        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            _width = Convert.ToInt32(additional[0]);
            _height = Convert.ToInt32(additional[1]);
            _hitBox = new CHitBox(this, 0, 0, _width, _height);

            //determine corner angles and magnitudes
            Vector2 center = _hitBox.center + position;

            _angleTopLeft = MathExt.MathExt.angle(center, position);
            _angleTopRight = MathExt.MathExt.angle(center, position + new Vector2(_width, 0));
            _angleBottomRight = MathExt.MathExt.angle(center, position + new Vector2(_width, _height));
            _angleBottomLeft = MathExt.MathExt.angle(center, position + new Vector2(0, _height));

            _distToTopLeft = MathExt.MathExt.distance(center, position);
            _distToTopRight = MathExt.MathExt.distance(center, _position + new Vector2(_width, 0));
            _distToBottomRight = MathExt.MathExt.distance(center, _position + new Vector2(_width, _height));
            _distToBottomLeft = MathExt.MathExt.distance(center, _position + new Vector2(0, _height));

            //calculate triangles
            _leftTri = MathExt.MathExt.buildTriangle(180.0, _angleBottomLeft - _angleTopLeft, (double)center.X - _width / 2.0, center);
            _rightTri = MathExt.MathExt.buildTriangle(0, (_angleTopRight + 360) - _angleBottomRight, (double)center.X + _width / 2.0, center);
            _topTri = MathExt.MathExt.buildTriangle(90.0, _angleTopLeft - _angleTopRight, (double)center.X - _height / 2.0, center);
            _bottomTri = MathExt.MathExt.buildTriangle(270.0, _angleBottomLeft - _angleBottomRight, (double)center.X + _height / 2.0, center);
        }

        public bool checkPointInLeftQuadrant(Vector2 point)
        {
            return MathExt.MathExt.checkPointInTriangle(point, _leftTri.A, _leftTri.B, _leftTri.C);
        }

        public bool checkPointInRightQuadrant(Vector2 point)
        {
            return MathExt.MathExt.checkPointInTriangle(point, _rightTri.A, _rightTri.B, _rightTri.C);
        }

        public bool checkPointInTopQuadrant(Vector2 point)
        {
            return MathExt.MathExt.checkPointInTriangle(point, _topTri.A, _topTri.B, _topTri.C);
        }

        public bool checkPointInBottomQuadrant(Vector2 point)
        {
            return MathExt.MathExt.checkPointInTriangle(point, _bottomTri.A, _bottomTri.B, _bottomTri.C);
        }

        public int height
        {
            get
            {
                return _height;
            }
        }

        public int width
        {
            get
            {
                return _width;
            }
        }
    }
}
