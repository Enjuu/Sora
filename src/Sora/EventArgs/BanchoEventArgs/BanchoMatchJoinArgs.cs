using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchJoinArgs : IEventArgs, INeedPresence
    {
        public int MatchId;
        public string Password;
        public Presence Pr { get; set; }
    }
}