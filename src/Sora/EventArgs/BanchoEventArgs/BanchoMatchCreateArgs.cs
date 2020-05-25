using Match = Sora.Objects.Multiplayer.Match;
using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchCreateArgs : IEventArgs, INeedPresence
    {
        public Match Room;
        public Presence Pr { get; set; }
    }
}