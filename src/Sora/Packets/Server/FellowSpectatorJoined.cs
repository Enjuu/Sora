using System;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Server
{
    public class FellowSpectatorJoined : IPacket
    {
        public int UserId;

        public FellowSpectatorJoined(int userid) => UserId = userid;

        public PacketId Id => PacketId.ServerFellowSpectatorJoined;

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