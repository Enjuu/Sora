using System.Collections.Generic;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Client
{
    public class UserStatsRequest : IPacket
    {
        public List<int> Userids;
        public PacketId Id => PacketId.ClientUserStatsRequest;

        public void ReadFromStream(MStreamReader sr)
        {
            Userids = sr.ReadInt32List();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            sw.Write(Userids);
        }
    }
}