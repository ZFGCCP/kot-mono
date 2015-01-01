using Gears.Cloud;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Gears.Navigation
{
    internal sealed class MenuItemCollection
    {
        private IMenuItem[] _mi;

        internal int Length
        {
            get { return _mi.Length; }
        }
        internal MenuItemCollection(IMenuItem[] menuItems)
        {
            _mi = menuItems;
        }
        internal MenuItemCollection(List<IMenuItem> menuItems)
        {
            _mi = menuItems.ToArray();
        }
        internal void PushIndex(int index)
        {
            //TODO: this check needs to benchmark with "is" to see which is faster. 
            //if (_mi[index].GetType(). == typeof(GameState)) 
            if (_mi[index] is GameState)
            {
                Master.Push((GameState)_mi[index]);
            }
            else
            {
                //_mi[index].ThrowPushEvent();
            }
            _mi[index].ThrowPushEvent();
        }
        internal string GetIndexMenuText(int index)
        {
            return _mi[index].MenuText;
        }
        internal Color GetIndexItemColor(int index)
        {
            return _mi[index].ItemColor;
        }
        internal bool GetIndexItemColorSet(int index)
        {
            return _mi[index].ItemColorSet;
        }
    }
}
