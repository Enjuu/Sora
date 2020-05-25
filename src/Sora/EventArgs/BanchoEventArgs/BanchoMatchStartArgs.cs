using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchStartArgs : IEventArgs, INeedPresence
    {
        public Presence Pr { get; set; }
    }
}