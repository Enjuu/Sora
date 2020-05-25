using System;
using Sora.Enums;
using Sora.Objects.Multiplayer;
using Sora.Utilities;

namespace Sora.Packets.Client
{
    public class MatchCreate : IPacket
    {
        public Match Match;
        public PacketId Id => PacketId.ClientMatchCreate;

        public void ReadFromStream(MStreamReader sr)
        {
            Match = new Match();
            Match.ReadFromStream(sr);
        }

        public void WriteToStream(MStreamWriter sw)
        {
            throw new NotImplementedException();
        }
    }
}