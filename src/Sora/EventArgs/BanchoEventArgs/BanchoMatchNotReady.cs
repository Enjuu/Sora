using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchNotReadyArgs : INeedPresence, IEventArgs
    {
        public Presence Pr { get; set; }
    }
}