using Sora.Objects;
using Sora.Utilities;

namespace Sora
{
    public interface IPacketPusher
    {
        void Push(IPacket packet, Presence skip = null);
    }
}