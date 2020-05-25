using Presence = Sora.Objects.Presence;
using ScoreFrame = Sora.Packets.Client.ScoreFrame;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchScoreUpdateArgs : INeedPresence, IEventArgs
    {
        public ScoreFrame Frame;
        public Presence Pr { get; set; }
    }
}