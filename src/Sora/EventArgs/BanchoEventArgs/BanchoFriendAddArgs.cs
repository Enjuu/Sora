using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoFriendAddArgs : IEventArgs, INeedPresence
    {
        public int FriendId;
        public Presence Pr { get; set; }
    }
}