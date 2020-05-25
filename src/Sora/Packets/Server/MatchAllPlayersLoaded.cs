using System;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Server
{
    public class MatchAllPlayersLoaded : IPacket
    {
        public PacketId Id => PacketId.ServerMatchAllPlayersLoaded;

        public void ReadFromStream(MStreamReader sr)
        {
            throw new NotImplementedException();
        }

        public void WriteToStream(MStreamWriter sw)
        {
        }
    }
}