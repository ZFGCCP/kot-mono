using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Gears.Cloud;
using Gears.Cloud.Input;
using System.Collections.Generic;
using Gears.Cloud.Utility.Drawing;

namespace Gears.Navigation
{
    /// <summary>
    /// MenuState   rev.005
    ///     Abstract class, intended to be inherited from and then instantiated
    ///     with constructor parameters. Every MenuState is a Menu, and contains
    ///     a MenuItemCollection, which is simply a collection of possible 
    ///     MenuStates for use in the MenuState/Engine logic.
    ///     
    ///     To avoid feature creep, this revision does not include parameterized
    ///     fonts or font sizes or window sizes. This is done on purpose and can
    ///     easily be refactored in on a later revision.
    /// </summary>
    public abstract class Old_MenuState : MenuReadyGameState
    {
        private MenuItemCollection mic; 

        //graphics resources
        internal protected SpriteFont menuFont;
        internal protected SpriteFont menuItemFont;

        //hardcoded defaults at 1280x720
        //TODO: Parameterize this stuff.
        //private string menuTitle = "Debug Menu";//////////////////////
        private Vector2 menuTitlePosition = new Vector2(85, 30);
        private Color menuTitleColor = new Color(255, 255, 255);

        private Vector2 menuItemOriginPosition = new Vector2(35, 134);
        private Vector2 menuItemVerticalOffset = new Vector2(0, 46);
        private Vector2 menuItemHorizontalOffset = new Vector2(247, 0);
        private Color defaultMenuItemColor = new Color(225, 225, 225);
        private uint maxRows = 10;
        private uint maxColumns = 3;

        private int activeMenuIndex = 0; //0 = default
        private Color defaultActiveItemColor = new Color(200, 125, 125);

        public Old_MenuState(string menuText, IMenuItem[] menuItemList)
        {
            mic = new MenuItemCollection(menuItemList);
            Initialize(menuText);
            LoadContent();
        }
        public Old_MenuState(string menuText, List<IMenuItem> menuItemList)
        {
            mic = new MenuItemCollection(menuItemList);
            Initialize(menuText);
            LoadContent();
        }
        private void Initialize(string menuText)
        {
            //base._HandlesInput = true;
            MenuText = menuText;
        }
        private void LoadContent()
        {
            menuFont = Master.GetGame().Content.Load<SpriteFont>(@"Fonts\MenuFont");
            menuItemFont = Master.GetGame().Content.Load<SpriteFont>(@"Fonts\MenuItem");
        }
        /// <summary>
        /// Contains logic that should be fired every time the state becomes active.
        /// This should fire especially in cases where the state had become inactive
        ///     and then regains activity once again.
        /// </summary>
        private void ActivateState()
        {
            _StateIsActive = true;

            Master.GetInputManager().GetCurrentInputHandler().ClearEventHandler();
            Master.GetInputManager().GetCurrentInputHandler().SubscribeInputHook(KeyDown);
        }
        /// <summary>
        /// Contains logic that should be fired every time the state becomes inactive.
        /// This was originally implemented to avoid function pointers AKA delegates
        ///     from firing out of the Master.stack order.
        /// </summary>
        private void InactivateState()
        {
            //Master.GetInputManager().GetCurrentInputHandler().UnsubscribeInputHook(KeyDown);
            Master.GetInputManager().GetCurrentInputHandler().ClearEventHandler();
            _StateIsActive = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawMenu(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            if (StateIsActive())
            {
                UpdateInput(gameTime);
            }
            else
            {
                ActivateState();
            }
        }
        /// <summary>
        /// Only should be run if the state is active.
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdateInput(GameTime gameTime)
        {
            
        }

        /// <summary>
        /// Event based Input hook for MenuState.
        /// </summary>
        /// <param name="currentKeyboardState">Passed from Input class.</param>
        /// <param name="oldKeyboardState">Passed from Input class.</param>
        internal void KeyDown(ref KeyboardState currentKeyboardState, ref KeyboardState oldKeyboardState)
        {
            if (currentKeyboardState.IsKeyDown(Keys.Enter) &&
                currentKeyboardState.IsKeyDown(Keys.Enter) != oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                PushActiveMenuIndex();
            }
            else
            {
                if (currentKeyboardState.IsKeyDown(Keys.Down) &&
                    activeMenuIndex != (mic.Length - 1) &&
                    currentKeyboardState.IsKeyDown(Keys.Down) != oldKeyboardState.IsKeyDown(Keys.Down))
                {
                    activeMenuIndex++;
                }
                if (currentKeyboardState.IsKeyDown(Keys.Up) &&
                    activeMenuIndex != 0 &&
                    currentKeyboardState.IsKeyDown(Keys.Up) != oldKeyboardState.IsKeyDown(Keys.Up))
                {
                    activeMenuIndex--;
                }
                if (currentKeyboardState.IsKeyDown(Keys.Left) &&
                    activeMenuIndex != 0 &&
                    currentKeyboardState.IsKeyDown(Keys.Left) != oldKeyboardState.IsKeyDown(Keys.Left))
                {
                    activeMenuIndex = (int)MathHelper.Clamp(activeMenuIndex - maxRows, 0, mic.Length - 1);
                }
                if (currentKeyboardState.IsKeyDown(Keys.Right) &&
                    activeMenuIndex != (mic.Length - 1) &&
                    currentKeyboardState.IsKeyDown(Keys.Right) != oldKeyboardState.IsKeyDown(Keys.Right))
                {
                    activeMenuIndex = (int)MathHelper.Clamp((int)(activeMenuIndex + maxRows), (int)0, (int)(mic.Length - 1));
                }
            }
        }
        /// <summary>
        /// Called when a user presses Enter on a specific Menu Item.
        /// </summary>
        private void PushActiveMenuIndex()
        {
            InactivateState();
            mic.PushIndex(activeMenuIndex);
        }

        /// <summary>
        /// Contains abstract Draw logic for the Menu.
        /// </summary>
        /// <param name="spriteBatch"></param>
        protected internal void DrawMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(menuFont, MenuText, menuTitlePosition, menuTitleColor);

            //for (int j = 0; j * maxRows <= numMenuItems; j++) //keep this CHRIS
            for (int j = 0; j < maxColumns; j++)
            {
                //for (int i=0; (i + j*maxRows) % maxRows <numMenuItems % maxRows; i++) //keep this CHRIS
                for (int i = 0; i < mic.Length - (j * maxRows) && i < maxRows; i++)
                {
                    int currentMenuItem = (int)(i + (maxRows * j));

                    Color currentItemColor = mic.GetIndexItemColorSet(currentMenuItem) ? mic.GetIndexItemColor(currentMenuItem) : defaultMenuItemColor;


                    if (activeMenuIndex == currentMenuItem) //if the item we are drawing is active
                    {
                        spriteBatch.DrawString(menuItemFont, mic.GetIndexMenuText(currentMenuItem), menuItemOriginPosition + (j * menuItemHorizontalOffset) + (i * menuItemVerticalOffset), defaultActiveItemColor);
                    }
                    else //the item is not an active item
                    {
                        spriteBatch.DrawString(menuItemFont, mic.GetIndexMenuText(currentMenuItem), menuItemOriginPosition + (j * menuItemHorizontalOffset) + (i * menuItemVerticalOffset), currentItemColor);
                    }
                }
            }
        }
    }
    public class MenuState : GameState
    {
        private Menu _menu;
        private DrawingHelper _drawingHelper = new DrawingHelper();
        private Texture2D blank;

        public KeyboardState KoTCurrentKeyboard, KoTPreviousKeyboard;

        public MenuState(Menu menu)
        {
            this._menu = menu;
            this.InitializeLocal();
        }

        private void InitializeLocal()
        {
            blank = new Texture2D(Master.GetGame().GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });

            if (_menu != null)
            {
                foreach (MenuElement element in this._menu.MenuElements)
                {
                    if (element.GetFont() == null && element.SpriteFont != null)
                    {
                        element.SetFont(Master.GetGame().Content.Load<SpriteFont>(element.SpriteFont));
                    }
                    if (element.GetTexture() == null && element.Texture2D != null)
                    {
                        element.SetTexture(Master.GetGame().Content.Load<Texture2D>(element.Texture2D));
                    }
                }
            }
        }

        /// <summary>
        /// Contains logic that should be fired every time the state becomes active.
        /// This should fire especially in cases where the state had become inactive
        ///     and then regains activity once again.
        /// </summary>
        private void ActivateState()
        {
            base._StateIsActive = true;

            Master.GetInputManager().GetCurrentInputHandler().ClearEventHandler();
            Master.GetInputManager().GetCurrentInputHandler().SubscribeInputHook(KeyDown);
        }

        /// <summary>
        /// Contains logic that should be fired every time the state becomes inactive.
        /// This was originally implemented to avoid function pointers AKA delegates
        ///     from firing out of the Master.stack order.
        /// </summary>
        private void InactivateState()
        {
            base._StateIsActive = false;
            Master.GetInputManager().GetCurrentInputHandler().ClearEventHandler();
        }

        /// <summary>
        /// Called when a user presses Enter on a specific Menu Item.
        /// </summary>
        private void PushActiveMenuIndex()
        {
            InactivateState();
            _menu.GetSelectableMenuElements()[_menu.GetActiveMenuIndex()].ThrowPushEvent();
        }

        public int activeMenuIndex
        {
            get
            {
                return _menu.GetActiveMenuIndex();
            }
        }

        /// <summary>
        /// Event based Input hook for MenuState.
        /// </summary>
        /// <param name="currentKeyboardState">Passed from Input class.</param>
        /// <param name="oldKeyboardState">Passed from Input class.</param>
        protected internal virtual void KeyDown(ref KeyboardState currentKeyboardState, ref KeyboardState oldKeyboardState)
        {
            if (currentKeyboardState.IsKeyDown(Keys.Enter) &&
                currentKeyboardState.IsKeyDown(Keys.Enter) != oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                PushActiveMenuIndex();
            }
            else
            {
                int activeMenuIndex = _menu.GetActiveMenuIndex();
                int selectableMenuLength = _menu.GetSelectableMenuElements().Length;

                if (currentKeyboardState.IsKeyDown(Keys.Down) &&
                    activeMenuIndex != (selectableMenuLength - 1) &&
                    currentKeyboardState.IsKeyDown(Keys.Down) != oldKeyboardState.IsKeyDown(Keys.Down))
                {
                    activeMenuIndex++;
                    _menu.SetActiveMenuIndex(activeMenuIndex);
                }
                if (currentKeyboardState.IsKeyDown(Keys.Up) &&
                    activeMenuIndex != 0 &&
                    currentKeyboardState.IsKeyDown(Keys.Up) != oldKeyboardState.IsKeyDown(Keys.Up))
                {
                    activeMenuIndex--;
                    _menu.SetActiveMenuIndex(activeMenuIndex);
                }
                // Temporarily not using this.
                //if (currentKeyboardState.IsKeyDown(Keys.Left) &&
                //    activeMenuIndex != 0 &&
                //    currentKeyboardState.IsKeyDown(Keys.Left) != oldKeyboardState.IsKeyDown(Keys.Left))
                //{
                //    activeMenuIndex = (int)MathHelper.Clamp(activeMenuIndex - maxRows, 0, mic.Length - 1);
                //}
                //if (currentKeyboardState.IsKeyDown(Keys.Right) &&
                //    activeMenuIndex != (mic.Length - 1) &&
                //    currentKeyboardState.IsKeyDown(Keys.Right) != oldKeyboardState.IsKeyDown(Keys.Right))
                //{
                //    activeMenuIndex = (int)MathHelper.Clamp((int)(activeMenuIndex + maxRows), (int)0, (int)(mic.Length - 1));
                //}
            }
        }

        /// <summary>
        /// Contains abstract Draw logic for the Menu.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch</param>
        protected internal void DrawMenu(SpriteBatch spriteBatch)
        {
            int i = 0;
            foreach (MenuElement element in this._menu.GetDrawableMenuElements()) 
            {
                //Draw in this order:
                //  BackgroundColor
                //  Texture
                //  Font
                if (element.BackgroundColor.PackedValue != 0)
                {
                    int activeMenuIndex = _menu.GetActiveMenuIndex();
                    Color bgcolor;
                    bool currentElementIsActive = this._menu.GetDrawableMenuElements()[i].Equals(this._menu.GetSelectableMenuElements()[activeMenuIndex]);
                    if (currentElementIsActive)
                    {
                        if (element.ActiveBackgroundColor.PackedValue != 0)
                        {
                            bgcolor = element.ActiveBackgroundColor;
                        }
                        else
                        {
                            bgcolor = element.BackgroundColor;
                        }
                    }
                    else
                    {
                        bgcolor = element.BackgroundColor;
                    }

                    _drawingHelper.DefaultDrawRectangle(ref spriteBatch, element.ActiveArea, ref blank, bgcolor);
                }

                if (element.GetTexture() != null)
                {
                    spriteBatch.Draw(element.GetTexture(), element.ActiveArea, Color.White);
                }

                if (element.GetFont() != null && !string.IsNullOrEmpty(element.MenuText))
                {
                    int activeMenuIndex = _menu.GetActiveMenuIndex();
                    Color fgcolor;

                    MenuElement drawingElemTmp = this._menu.GetDrawableMenuElements()[i];
                    MenuElement selectedElemTmp = this._menu.GetSelectableMenuElements()[activeMenuIndex];
                    bool currentElementIsActive = this._menu.GetDrawableMenuElements()[i].Equals(this._menu.GetSelectableMenuElements()[activeMenuIndex]);
                    if (currentElementIsActive)
                    {
                        if (element.ActiveForegroundColor.PackedValue != 0)
                        {
                            fgcolor = element.ActiveForegroundColor;
                        }
                        else
                        {
                            fgcolor = element.ForegroundColor;
                        }
                    }
                    else
                    {
                        fgcolor = element.ForegroundColor;
                    }

                    spriteBatch.DrawString(element.GetFont(), element.MenuText, new Vector2(element.ActiveArea.X, element.ActiveArea.Y), fgcolor);
                }
                i++;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawMenu(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            if (StateIsActive())
            {
                KeyDown(ref KoTCurrentKeyboard, ref KoTPreviousKeyboard);
            }
            else
            {
                ActivateState();
            }
        }

        public void swapMenu(Menu menu)
        {
            this._menu = menu;
            this.InitializeLocal();
        }
    }
}
