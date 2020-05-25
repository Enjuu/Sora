using System.Collections.Generic;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Client
{
    public class UserPresenceRequest : IPacket
    {
        public PacketId Id => PacketId.ClientUserPresenceRequest;

        public List<int> UserIds;

        public void ReadFromStream(MStreamReader sr)
        {
            UserIds = sr.ReadInt32List();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            sw.Write(UserIds);
        }
    }
}