using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoLobbyPartArgs : IEventArgs, INeedPresence
    {
        public Presence Pr { get; set; }
    }
}