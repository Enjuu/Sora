using System.Collections.Generic;
using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoBeatmapInfoRequestArgs : IEventArgs, INeedPresence
    {
        public List<string> FileNames { get; set; }
        public Presence Pr { get; set; }
    }
}