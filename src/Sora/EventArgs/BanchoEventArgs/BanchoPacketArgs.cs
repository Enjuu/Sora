using MStreamReader = Sora.Utilities.MStreamReader;
using PacketId = Sora.Enums.PacketId;
using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoPacketArgs : IEventArgs, INeedPresence
    {
        public MStreamReader Data;
        public PacketId PacketId;
        public Presence Pr { get; set; }
    }
}