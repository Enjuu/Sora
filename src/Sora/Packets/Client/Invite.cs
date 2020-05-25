using System;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Client
{
    public class Invite : IPacket
    {
        public int UserId;
        public PacketId Id => PacketId.ClientInvite;

        public void ReadFromStream(MStreamReader sr)
        {
            UserId = sr.ReadInt32();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            throw new NotImplementedException();
        }
    }
}