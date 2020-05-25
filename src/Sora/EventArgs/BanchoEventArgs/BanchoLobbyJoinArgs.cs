using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoLobbyJoinArgs : IEventArgs, INeedPresence
    {
        public Presence Pr { get; set; }
    }
}