using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchChangeSlotArgs : INeedPresence, IEventArgs
    {
        public int SlotId { get; set; }
        public Presence Pr { get; set; }
    }
}