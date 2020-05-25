using System;
using Sora.Enums;
using Sora.Objects;
using Sora.Utilities;

namespace Sora.Packets.Server
{
    public class ChannelRevoked : IPacket
    {
        public string Channel;

        public ChannelRevoked(Channel channel) => Channel = channel.Name;
        public ChannelRevoked(string channel) => Channel = channel;

        public PacketId Id => PacketId.ServerChannelRevoked;

        public void ReadFromStream(MStreamReader sr)
        {
            throw new NotImplementedException();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            sw.Write(Channel);
        }
    }
}