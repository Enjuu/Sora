using System;
using Sora.Enums;
using Sora.Objects.Multiplayer;
using Sora.Utilities;

namespace Sora.Packets.Server
{
    public class MatchDisband : IPacket
    {
        public Match Match;

        public MatchDisband(Match match) => Match = match;

        public PacketId Id => PacketId.ServerMatchDisband;

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