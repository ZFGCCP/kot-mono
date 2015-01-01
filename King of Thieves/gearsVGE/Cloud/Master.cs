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

using Gears.Cloud._Debug;
using Gears.Cloud.Events;
using Gears.Cloud.Input;

namespace Gears.Cloud
{
    public static class Master
    {
        private static Color clearColor = new Color(105, 105, 105, 255);
        //private static Color nullColor = new Color(148, 0, 211, 255); // DarkViolet
        //0,0,255,255 Blue

        private static Stack<GameState> stack = new Stack<GameState>();
        private static LinkedList<GameState> overlays = new LinkedList<GameState>();


        private static InputManager inputManager = new InputManager();
        private static Game game;


        public static void Initialize(Game _game)
        {
            game = _game;
        }


        public static void Push(GameState gameState)
        {
            stack.Push(gameState);
        }
        public static GameState Peek()
        {
            return stack.Peek();
        }
        public static GameState Pop()
        {
            return stack.Pop();
        }
        /// <summary>
        /// Master Draw call.
        /// This should be the first and only interface from the main game
        ///     "Draw" loop for this instance of the VGE.
        /// </summary>
        /// <param name="spriteBatch">The global-parameter sprite batch.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            //no matter what, if draw is called, we are drawing the top stack item.
            stack.Peek().Draw(spriteBatch);

            //if we have at least one overlay. we are able to draw more than one layer
            if (overlays.Count > 0)
            {
                //**Note that this code does not take into account culling.
                foreach (GameState overlay in overlays)
                {
                    overlay.Draw(spriteBatch);
                }
            }
            //else //it's not an overlay. 
            //since we are already drawing the top stack item, we dont need to do anything else.
            //{ }  //this is just here in case it is useful in the future.
        }
        /// <summary>
        /// Master Update call. 
        /// This should be the first and only interface from the main game 
        ///     "Update" loop for this instance of the VGE.
        /// </summary>
        /// <param name="gameTime">The time snapshot.</param>
        public static void Update(GameTime gameTime)
        {
            //global events
            CGlobalEvents.GFrameTrigger.Update();

            //Input
            //new
            GetInputManager().Update(gameTime);
            //old
            //DefaultInput_old.Update(gameTime);

            if (overlays.Count > 0)
            {
                overlays.Last.Value.Update(gameTime);
            }
            else
            {
                //only updating the top item of the stack.
                //as of right now, we have no use cases where two items need to be updated at the same time.
                //if this feature is requested, it can be implemented.
                stack.Peek().Update(gameTime);
            }
            
            

            //NOTE: Only do this for the frame event!!!
            CGlobalEvents.GFrameTrigger.getEvent(0).triggered = false;

        }
        public static void AddOverlay(GameState overlay)
        {
            overlays.AddLast(overlay);
        }
        public static void RemoveLastOverlay()
        {
            overlays.RemoveLast();
        }
        //private static void PopToList()
        //{
        //    if (stack.Count != 0)
        //    {
        //        Debug.Out("Master::StoreTop(): Stack is not empty. Popping stack to list.");
        //        overlays.AddFirst(stack.Pop());
        //    }
        //    else
        //    {
        //        Debug.Out("Master::StoreTop(): ERROR Stack is empty. Cannot pop stack.");
        //    }
        //}
        //private static void PushReturnToStack()
        //{
        //    if (overlays.Count != 0)
        //    {
        //        Debug.Out("Master::ReturnToStack(): List is not empty. Pushing first item of list to stack.");
        //        stack.Push(overlays.First());
        //        overlays.RemoveFirst();
        //    }
        //    else
        //    {
        //        Debug.Out("Master::ReturnToStack(): ERROR List is empty. Unable to push any object to stack.");
        //    }
        //}

        private static void ClearOverlays()
        {
            overlays.Clear();
        }
        public static int GetListLength()
        {
            return overlays.Count;
        }
        public static int GetStackLength()
        {
            return stack.Count;
        }
        public static Color GetClearColor()
        {
            return clearColor;
        }
        public static void SetClearColor(Color color)
        {
            clearColor = color;
        }
        public static Game GetGame()
        {
            return game;
        }
        public static InputManager GetInputManager()
        {
            return inputManager;
        }
    }
}
