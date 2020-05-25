using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoEmptyEventArgs : IEventArgs, INeedPresence
    {
        public Presence Pr { get; set; }
    }
}