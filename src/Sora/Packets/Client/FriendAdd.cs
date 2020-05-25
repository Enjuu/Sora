using System;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Client
{
    public class FriendAdd : IPacket
    {
        public int FriendId;
        public PacketId Id => PacketId.ClientFriendAdd;

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