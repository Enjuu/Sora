using System;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Server
{
    public class SpectatorJoined : IPacket
    {
        public readonly int UserId;

        public SpectatorJoined(int userid) => UserId = userid;

        public PacketId Id => PacketId.ServerSpectatorJoined;

        public void ReadFromStream(MStreamReader sr)
        {
            throw new NotImplementedException();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            sw.Write(UserId);
        }
    }
}