using System;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Client
{
    public class ReceiveUpdates : IPacket
    {
        public int UserId;
        public PacketId Id => PacketId.ClientReceiveUpdates;

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