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
using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using King_of_Thieves.Actors;
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
        private CMap _currentMap = null;
        private King_of_Thieves.Graphics.CCamera _camera = new King_of_Thieves.Graphics.CCamera();
        public double mouseSnapX = 16;
        public double mouseSnapY = 16;

        private int _HScrollVal = 0;
        private int _VScrollVal = 0;

        public void changeSelectedTile(CTile tile)
        {
            _selectedTile = tile;
        }

        public void changeCurrentTileSet(CSprite tileSet)
        {
            _selectedSprite = tileSet;
            
        }

        public void dropTile(CTile tile, CLayer layer, int offSetX = 0, int offSetY = 0)
        {
            CTile test = tile as CAnimatedTile;
            CTile newTile = null;
            if (test == null)
                newTile = new CTile(tile);
            else
                newTile = new CAnimatedTile((CAnimatedTile)tile);

            Vector2 position = _getMouseSnap(offSetX,offSetY);
            newTile.tileCoords = position;
            layer.addTile(newTile);
        }

        public void scrollVertical(int amount)
        {
            Vector3 translation = Vector3.Zero;
            translation.Y = amount;
            translation.X = _camera.position.X;
            _VScrollVal = amount;
            _camera.jump(translation);
        }

        public void scrollHorizontal(int amount)
        {
            Vector3 translation = Vector3.Zero;
            translation.X = amount;
            translation.Y = _camera.position.Y;
            _HScrollVal = amount;
            _camera.jump(translation);
        }

        public void removeTile(CLayer layer, Vector2 position,int offSetX = 0, int offSetY = 0)
        {
            position.X += offSetX;
            position.Y += offSetY;
            int index = layer.indexOfTileReverse(position);

            layer.removeTile(index);
        }

        public void dropActor(string actor, string name, Vector2 position, int layer, string[] parameters)
        {
            Type actorType = Type.GetType(actor);
            CActor tempActor = (CActor)Activator.CreateInstance(actorType);
            CComponent tempComponent = null;
            bool dontAddComp = false;
            if (actorType == typeof(King_of_Thieves.Actors.Collision.CSolidTile))
            {
                if (_currentMap._layers[layer].hitboxAddress == CReservedAddresses.HITBOX_NOT_PRESENT)
                {
                    tempComponent = new CComponent(_currentMap.largestAddress + 1);
                    _currentMap._layers[layer].hitboxAddress = tempComponent.address;
                }
                else
                {
                    tempComponent = _currentMap.queryComponentRegistry(_currentMap._layers[layer].hitboxAddress);
                    dontAddComp = true;
                }
            }
            else
            {
                tempComponent = new CComponent(_currentMap.largestAddress + 1);
            }

            tempActor.init(name, position, actorType.ToString(), 0, parameters);
            tempActor.layer = layer;
            tempActor.swapImage(CActor._MAP_ICON);
            tempActor.mapParams = new System.Collections.Generic.List<string>(parameters);

            tempComponent.addActor(tempActor, name);

            _currentMap.addToActorRegistry(tempActor);

            if (!dontAddComp)
                _currentMap.addComponent(tempComponent, layer);
            else
                _currentMap._layers[layer].addToDrawList(tempActor);
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

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, _camera.transformation);

            _currentMap.draw(spriteBatch);

            if (_selectedTile != null && _selectedSprite != null)
            {
                _selectedTile.update();
                _selectedTile.draw(_selectedSprite, spriteBatch,mouseSnapX,mouseSnapY);
            }
            spriteBatch.End();

        }

        public CMap map
        {
            set
            {
                _currentMap = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
                _camera.update();

                System.Drawing.Point mousePos = PointToClient(MousePosition);

                if (_selectedTile != null)
                {
                    int snapX = (int)(System.Math.Floor((mousePos.X) / mouseSnapX) * mouseSnapX);
                    int snapY = (int)(System.Math.Floor((mousePos.Y) / mouseSnapY) * mouseSnapY);

                    _selectedTile.tileCoords = _getMouseSnap(-_HScrollVal, -_VScrollVal);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                spriteBatch.End();
            }
        }

        private Vector2 _getMouseSnap(int offSetX = 0, int offSetY = 0)
        {
            System.Drawing.Point mousePos = PointToClient(MousePosition);

            int snapX = (int)(System.Math.Floor((mousePos.X + offSetX) / mouseSnapX) * mouseSnapX);
            int snapY = (int)(System.Math.Floor((mousePos.Y + offSetY) / mouseSnapY) * mouseSnapY);

            return new Vector2(snapX, snapY);
        }

        public Vector2 getMouseSnapCoords(int offSetX = 0, int offSetY = 0)
        {
            System.Drawing.Point mousePos = PointToClient(MousePosition);

            int snapX = mousePos.X + offSetX;
            int snapY = mousePos.Y + offSetY;

            return new Vector2(snapX, snapY);
        }

        public void deleteHitbox(Vector2 position, int offSetX, int offSetY)
        {
            CActor[] hitboxes = _currentMap.queryActorRegistry(typeof(King_of_Thieves.Actors.Collision.CSolidTile), getMouseSnapCoords(offSetX,offSetY));

            if (hitboxes != null && hitboxes.Length > 0)
                _currentMap.removeActorFromComponent(hitboxes[0], hitboxes[0].componentAddress);
        }
    }
}
