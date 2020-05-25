using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchFailedArgs : INeedPresence, IEventArgs
    {
        public Presence Pr { get; set; }
    }
}