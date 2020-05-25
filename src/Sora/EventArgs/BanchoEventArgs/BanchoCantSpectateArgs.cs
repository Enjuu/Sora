using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoCantSpectateArgs : IEventArgs, INeedPresence
    {
        public Presence Pr { get; set; }
    }
}