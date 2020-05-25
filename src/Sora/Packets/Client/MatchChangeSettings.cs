using System;
using Sora.Enums;
using Sora.Objects.Multiplayer;
using Sora.Utilities;

namespace Sora.Packets.Client
{
    public class MatchChangeSettings : IPacket
    {
        public Match Match;
        public PacketId Id => PacketId.ClientMatchChangeSettings;

        public void ReadFromStream(MStreamReader sr)
        {
            (Match = new Match()).ReadFromStream(sr);
        }

        public void WriteToStream(MStreamWriter sw)
        {
            throw new NotImplementedException();
        }
    }
}