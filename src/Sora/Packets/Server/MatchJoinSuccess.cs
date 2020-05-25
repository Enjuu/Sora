using System;
using Sora.Enums;
using Sora.Objects.Multiplayer;
using Sora.Utilities;

namespace Sora.Packets.Server
{
    public class MatchJoinSuccess : IPacket
    {
        public Match Match;

        public MatchJoinSuccess(Match match) => Match = match;

        public PacketId Id => PacketId.ServerMatchJoinSuccess;

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