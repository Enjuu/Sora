using System.Collections.Generic;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Server
{
    public class FriendsList : IPacket
    {
        public List<int> FriendIds;

        public FriendsList(List<int> friendIds) => FriendIds = friendIds;

        public PacketId Id => PacketId.ServerFriendsList;

        public void ReadFromStream(MStreamReader sr)
        {
            FriendIds = sr.ReadInt32List();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            sw.Write(FriendIds);
        }
    }
}