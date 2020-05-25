using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs
{
    public class EventArgs<T> : INeedPresence, IEventArgs
    {
        public T Data { get; set; }
        public Presence Pr { get; set; }
    }
}