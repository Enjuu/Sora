using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoPongArgs : IEventArgs, INeedPresence
    {
        public Presence Pr { get; set; }
    }
}