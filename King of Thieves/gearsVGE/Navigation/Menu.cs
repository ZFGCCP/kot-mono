using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace Gears.Navigation
{
    //TODO: Allow more parameters later on down the line.
    /// <summary>
    /// Menu is an instantiable class that allows you to create your own menus!
    /// TODO: Params and constructor doc.
    /// </summary>
    public sealed class Old_Menu : Old_MenuState
    {
        public Old_Menu(string menuText, IMenuItem[] menuItemList) : base(menuText, menuItemList) { }
        public Old_Menu(string menuText, List<IMenuItem> menuItemList) : base(menuText, menuItemList) { }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }

    /// <summary>
    /// A Menu is a collection of several MenuElements.
    /// Can be loaded into a MenuState to create a functioning navigational menu.
    /// </summary>
    [XmlRootAttribute("Menu", Namespace = "http://www.spectrumbranch.com", IsNullable = false)]
    public class Menu
    {
        [XmlArray("MenuElements", IsNullable = false)]
        public List<MenuElement> MenuElements = new List<MenuElement>();

        [XmlIgnore]
        private MenuElement[] _cacheNotHidden;

        [XmlIgnore]
        private MenuElement[] _cacheSelectable;

        [XmlIgnore]
        private int _cacheSelectableActiveIndex;

        /// <summary>
        /// Creates an instance of the Menu class.
        /// </summary>
        public Menu()
        {
            this.InitializeLocal();
        }

        private void InitializeLocal()
        {
            this.Recache();
        }

        /// <summary>
        /// Used to recalculate internals that are used to keep track specifically of non-hidden and selectable menus.
        /// This is an expensive calculation for what it's worth, so it should ideally only happen once, unless you need
        /// the menu to have dynamically changing options.
        /// </summary>
        public void Recache()
        {
            List<MenuElement> CacheNotHidden = new List<MenuElement>();
            List<MenuElement> CacheSelectable = new List<MenuElement>();

            foreach (MenuElement element in this.MenuElements)
            {
                if (element.Selectable)
                {
                    CacheSelectable.Add(element);
                }
                if (!element.Hidden)
                {
                    CacheNotHidden.Add(element);
                }
            }
            this._cacheNotHidden = CacheNotHidden.ToArray();
            this._cacheSelectable = CacheSelectable.ToArray();

            if (this._cacheSelectable.Length > 0)
            {
                this._cacheSelectableActiveIndex = 0;
            }
            else
            {
                this._cacheSelectableActiveIndex = -1;
            }
        }

        /// <summary>
        /// Adds a MenuElement to the end of the list.
        /// </summary>
        /// <param name="menuElement"></param>
        public void AddMenuElement(MenuElement menuElement)
        {
            this.MenuElements.Add(menuElement);
            this.Recache();
        }
        /// <summary>
        /// Adds many MenuElements to the end of the list.
        /// </summary>
        /// <param name="menuElement"></param>
        public void AddMenuElements(IEnumerable<MenuElement> menuElements)
        {
            this.MenuElements.AddRange(menuElements);
            this.Recache();
        }
        /// <summary>
        /// Remove all MenuElements from this menu.
        /// </summary>
        public void UnloadMenu()
        {
            this.MenuElements.Clear();
            this.Recache();
        }
        /// <summary>
        /// Gets all of the MenuElements that are not hidden, in the order they were added to the Menu.
        /// </summary>
        /// <returns>All MenuElements that are not hidden.</returns>
        public MenuElement[] GetDrawableMenuElements()
        {
            return this._cacheNotHidden;
        }
        /// <summary>
        /// Gets all of the MenuElements that are selectable, in the order they were added to the Menu.
        /// </summary>
        /// <returns>All MenuElements that are selectable.</returns>
        public MenuElement[] GetSelectableMenuElements()
        {
            return this._cacheSelectable;
        }
        /// <summary>
        /// Gets the numeric active menu index (starting at 0) for the Selectable MenuElements.
        /// </summary>
        /// <returns>A zero-index indicator of which MenuElement is currently active.</returns>
        public int GetActiveMenuIndex()
        {
            return this._cacheSelectableActiveIndex;
        }
        /// <summary>
        /// Sets the numeric active menu index for the Selectable MenuElements.
        /// </summary>
        /// <param name="index">The numeric index between the minimum and maximum values of the Selectable MenuElements.</param>
        public void SetActiveMenuIndex(int index)
        {
            if (index >= 0 && index < this._cacheSelectable.Length)
            {
                this._cacheSelectableActiveIndex = index;
            }
        }
    }
}
