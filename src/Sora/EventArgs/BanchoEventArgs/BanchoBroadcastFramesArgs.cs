using Presence = Sora.Objects.Presence;
using SpectatorFrame = Sora.Packets.Client.SpectatorFrame;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoBroadcastFramesArgs : IEventArgs, INeedPresence
    {
        public SpectatorFrame Frames;
        public Presence Pr { get; set; }
    }
}