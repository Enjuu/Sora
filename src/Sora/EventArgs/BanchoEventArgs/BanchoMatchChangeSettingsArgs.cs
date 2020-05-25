using Match = Sora.Objects.Multiplayer.Match;
using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchChangeSettingsArgs : IEventArgs, INeedPresence
    {
        public Match Room { get; set; }
        public Presence Pr { get; set; }
    }
}