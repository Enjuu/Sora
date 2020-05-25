using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchChangeTeamArgs : IEventArgs, INeedPresence
    {
        public Presence Pr { get; set; }
    }
}