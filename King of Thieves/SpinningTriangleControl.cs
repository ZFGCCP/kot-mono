#region File Description
//-----------------------------------------------------------------------------
// SpinningTriangleControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using King_of_Thieves.Graphics;
using King_of_Thieves.Map;
#endregion

namespace WinFormsGraphicsDevice
{
    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, which allows it to
    /// render using a GraphicsDevice. This control shows how to draw animating
    /// 3D graphics inside a WinForms application. It hooks the Application.Idle
    /// event, using this to invalidate the control, which will cause the animation
    /// to constantly redraw.
    /// </summary>
    class SpinningTriangleControl : GraphicsDeviceControl
    {
        BasicEffect effect;
        Stopwatch timer;
        SpriteBatch spriteBatch;
        private CTile _selectedTile;
        private CSprite _selectedSprite;

        public void changeSelectedTile(CTile tile)
        {
            _selectedTile = tile;
        }

        public void changeCurrentTileSet(CSprite tileSet)
        {
            _selectedSprite = tileSet;
        }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            // Create our effect.
            effect = new BasicEffect(GraphicsDevice);

            effect.VertexColorEnabled = true;

            // Start the animation timer.
            timer = Stopwatch.StartNew();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }


        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            if (_selectedTile != null && _selectedSprite != null)
            {
                _selectedTile.draw(_selectedSprite, spriteBatch);
            }
            spriteBatch.End();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            System.Drawing.Point mousePos = PointToClient(MousePosition);

            if (_selectedTile != null)
            {
                int snapX = (int)System.Math.Floor((mousePos.X) / 16.0);
                int snapY = (int)System.Math.Floor((mousePos.Y) / 16.0);

                _selectedTile.tileCoords = new Vector2(snapX, snapY);
            }
        }
    }
}
