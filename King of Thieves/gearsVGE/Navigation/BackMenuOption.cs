using Gears.Cloud;

namespace Gears.Navigation
{
    public sealed class BackMenuOption : MenuUserControl
    {
        public BackMenuOption() : base("Back") { }
        public override void ThrowPushEvent()
        {
            Master.Pop();
        }
    }
}
