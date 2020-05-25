using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchCompleteArgs : INeedPresence, IEventArgs
    {
        public Presence Pr { get; set; }
    }
}