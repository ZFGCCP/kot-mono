using Microsoft.Xna.Framework;
namespace Gears.Navigation
{
    public interface IMenuItem
    {
        string MenuText { get; set; }
        bool ItemColorSet { get; }
        Color ItemColor { get; set; }
        void ThrowPushEvent();
    }
}
