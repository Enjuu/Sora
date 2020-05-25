using System.Collections.Generic;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Server
{
    public class PresenceBundle : IPacket
    {
        public List<int> UserIds;

        public PresenceBundle(List<int> userIds) => UserIds = userIds;

        public PacketId Id => PacketId.ServerUserPresenceBundle;

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