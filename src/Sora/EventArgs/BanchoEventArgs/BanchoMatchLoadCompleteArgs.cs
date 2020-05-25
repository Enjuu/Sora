using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchLoadCompleteArgs : INeedPresence, IEventArgs
    {
        public Presence Pr { get; set; }
    }
}