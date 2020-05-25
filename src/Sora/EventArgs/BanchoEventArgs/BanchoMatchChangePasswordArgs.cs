using Match = Sora.Objects.Multiplayer.Match;
using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchChangePasswordArgs : INeedPresence, IEventArgs
    {
        public Match Room { get; set; }
        public Presence Pr { get; set; }
    }
}