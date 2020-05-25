using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchNoBeatmapArgs : IEventArgs, INeedPresence
    {
        public Presence Pr { get; set; }
    }
}