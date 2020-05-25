using System;
using Sora.Enums;
using Sora.Objects.Multiplayer;
using Sora.Utilities;

namespace Sora.Packets.Server
{
    public class MatchUpdate : IPacket
    {
        public Match Match;

        public MatchUpdate(Match match) => Match = match;

        public PacketId Id => PacketId.ServerMatchUpdate;

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