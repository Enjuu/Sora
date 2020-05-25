using System;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Client
{
    public class MatchTransferHost : IPacket
    {
        public int SlotId;
        public PacketId Id => PacketId.ClientMatchTransferHost;

        public void ReadFromStream(MStreamReader sr)
        {
            SlotId = sr.ReadInt32();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            throw new NotImplementedException();
        }
    }
}