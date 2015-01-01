//TODO: NEEDS LOTS OF CLEANING UP.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Microsoft.Xna.Framework.Storage;

namespace Gears.Cloud
{
    /// <summary>
    /// ViewportHandler
    /// </summary>
    public static class ViewportHandler
    {
        private static int _screenWidth;
        private static int _screenHeight;
        private static Matrix _matrixScale = new Matrix();
        private static Vector3 _scale = new Vector3();
        private static Viewport _viewport; //note
        private static DisplayMode _mode;

        private static Dictionary<int, Viewport> _viewports = new Dictionary<int,Viewport>(); // PROPOSAL ONLY: For future implementation, we will soon support multiple viewports. We'll wait for the camera to be implemented for this.

        private static void SetWidth(int w)
        {
            _screenWidth = w;
        }
        private static void SetHeight(int h)
        {
            _screenHeight = h;
        }

        private static void SetViewport(Viewport viewport)
        {
            _viewport = viewport;
            _viewport.Width = GetWidth();
            _viewport.Height = GetHeight();
        }

        private static void SetViewport(int index, Viewport viewport, int width, int height)
        {
            _viewports.Add(index, viewport);
            _viewport.Width = width;
            _viewport.Height = height;
        }

        /// <summary>
        /// FetchResolutions()
        /// Internally defaults to native resolution if fullscreen set to true.
        /// Not quite sure how this is going to work yet.
        /// </summary>
        private static void FetchAndSetResolution()
        {
            foreach (DisplayMode mode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
#if DEBUG
                Gears.Cloud._Debug.Debug.Out(mode.ToString());
#endif
                _mode = mode;
            }

            if (Master.GetGame().GraphicsDevice.PresentationParameters.IsFullScreen)
            {
#if DEBUG
                Gears.Cloud._Debug.Debug.Out("Setting resolution to native resolution: " + _mode.Width + "," + _mode.Height);
#endif
                SetScreen(_mode.Width, _mode.Height);
            }
            else
            {
                Gears.Cloud._Debug.Debug.Out("Detected supported resolutions: " + _mode);
            }

        }
        /// <summary>
        /// SetScaleMatrix (public)
        /// _scale is a Vector3 object which is the scaleMatrix. This may be useful for re-scaling the screen to a smaller or bigger resolution,
        /// if desired.  
        /// </summary>
        /// <param name="w">Screen Width</param>
        /// <param name="h">Screen Height</param>
        public static void SetScaleMatrix(float w, float h)
        {
            _scale = new Vector3(_screenWidth / w, _screenHeight / h, 1); // z value remains 1 always. 
            _matrixScale = Matrix.CreateScale(_scale);
        }

        public static void SetScaleMatrix(float screenWidth, float screenHeight, float w, float h)
        {
            _scale = new Vector3(screenWidth / w, screenHeight / h, 1);
            _matrixScale = Matrix.CreateScale(_scale);
        }

        /// <summary>
        /// Defines game screen size, sets up viewport, and scales screen if desired.
        /// TODO: Needs refactoring. 
        /// </summary>
        /// <param name="w">ScreenWidth</param>
        /// <param name="h">ScreenHeight</param>
        public static void SetScreen(int w, int h)
        {
            //Filter input as to prevent crash
            if (w > 0 && h > 0)
            {
                SetWidth(w);
                SetHeight(h);
                SetViewport(Master.GetGame().GraphicsDevice.Viewport);
                SetScaleMatrix(w, h);
            }
            else
            {
                throw new System.ArgumentException("Developer Error: Viewport parameters cannot be less than or equal to zero.", "ViewportHandler.SetScreen(w,h)");
            }
        }

        public static int GetWidth()
        {
            return _screenWidth;
        }

        public static int GetHeight()
        {
            return _screenHeight;
        }

        public static Viewport GetViewport()
        {
            return _viewport;
        }

        public static Matrix GetScaleMatrix()
        {
            return _matrixScale;
        }
    }
}
