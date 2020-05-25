using ErrorStates = Sora.Enums.ErrorStates;
using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoExitArgs : IEventArgs, INeedPresence
    {
        public ErrorStates Err;
        public Presence Pr { get; set; }
    }
}