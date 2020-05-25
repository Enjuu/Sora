using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchHasBeatmapArgs : INeedPresence, IEventArgs
    {
        public Presence Pr { get; set; }
    }
}