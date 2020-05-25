using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoStartSpectatingArgs : IEventArgs, INeedPresence
    {
        public int SpectatorHostId;
        public Presence Pr { get; set; }
    }
}