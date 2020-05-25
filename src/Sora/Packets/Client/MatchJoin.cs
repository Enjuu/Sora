using System;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Client
{
    public class MatchJoin : IPacket
    {
        public int MatchId;
        public string Password;
        public PacketId Id => PacketId.ClientMatchJoin;

        public void ReadFromStream(MStreamReader sr)
        {
            MatchId = sr.ReadInt32();
            Password = sr.ReadString();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            throw new NotImplementedException();
        }
    }
}