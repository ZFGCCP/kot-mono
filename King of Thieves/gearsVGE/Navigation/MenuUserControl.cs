using Microsoft.Xna.Framework;
namespace Gears.Navigation
{
    public abstract class MenuUserControl : IMenuItem
    {
        //TODO: SomeDataConnectionHook
        //TODO: SomeUserControlHook

        public Color ItemColor { get; set; }

        private string menuText;
        public string MenuText
        {
            get { return menuText; }
            set
            {
                //18 chars or less to fit release 2 of menu implementation
                if (value.Length <= 18)
                {
                    menuText = value;
                }
                else
                {
                    //do nothing currently
                }
            }
        }
        private bool itemColorSet = false;
        public bool ItemColorSet
        {
            get { return itemColorSet; }
        }

        public MenuUserControl(string menuText)
        {
            MenuText = menuText;
        }

        public virtual void ThrowPushEvent() { }
    }
}
