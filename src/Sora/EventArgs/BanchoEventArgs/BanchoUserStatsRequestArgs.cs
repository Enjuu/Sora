using System.Collections.Generic;
using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoUserStatsRequestArgs : IEventArgs, INeedPresence
    {
        public List<int> UserIds;
        public Presence Pr { get; set; }
    }
}