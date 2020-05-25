using System;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Client
{
    public class FriendRemove : IPacket
    {
        public int FriendId;
        public PacketId Id => PacketId.ClientFriendRemove;

        public void ReadFromStream(MStreamReader sr)
        {
            FriendId = sr.ReadInt32();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            throw new NotImplementedException();
        }
    }
}