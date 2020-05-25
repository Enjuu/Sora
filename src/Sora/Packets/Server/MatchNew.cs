using System;
using Sora.Enums;
using Sora.Objects.Multiplayer;
using Sora.Utilities;

namespace Sora.Packets.Server
{
    public class MatchNew : IPacket
    {
        public Match Match;

        public MatchNew(Match match) => Match = match;

        public PacketId Id => PacketId.ServerMatchNew;

        public void ReadFromStream(MStreamReader sr)
        {
            throw new NotImplementedException();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            sw.Write(Match);
        }
    }
}