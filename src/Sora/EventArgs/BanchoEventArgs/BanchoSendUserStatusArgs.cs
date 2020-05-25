using Presence = Sora.Objects.Presence;
using UserStatus = Sora.Objects.UserStatus;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoSendUserStatusArgs : IEventArgs, INeedPresence
    {
        public UserStatus Status;
        public Presence Pr { get; set; }
    }
}