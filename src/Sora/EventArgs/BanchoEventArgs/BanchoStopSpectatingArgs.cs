using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoStopSpectatingArgs : IEventArgs, INeedPresence
    {
        public Presence Pr { get; set; }
    }
}