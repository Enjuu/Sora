using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchPartArgs : IEventArgs, INeedPresence
    {
        public Presence Pr { get; set; }
    }
}