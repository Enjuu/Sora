using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchReadyArgs : INeedPresence, IEventArgs
    {
        public Presence Pr { get; set; }
    }
}