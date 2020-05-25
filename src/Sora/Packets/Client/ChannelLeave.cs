using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Client
{
    public class ChannelLeave : IPacket
    {
        public string ChannelName;
        public PacketId Id => PacketId.ClientChannelJoin;

        public void ReadFromStream(MStreamReader sr)
        {
            ChannelName = sr.ReadString();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            sw.Write(ChannelName);
        }
    }
}