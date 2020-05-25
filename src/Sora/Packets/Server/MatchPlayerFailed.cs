using System;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Server
{
    public class MatchPlayerFailed : IPacket
    {
        public int SlotId;

        public MatchPlayerFailed(int slotId) => SlotId = slotId;

        public PacketId Id => PacketId.ServerMatchPlayerFailed;

        public void ReadFromStream(MStreamReader sr)
        {
            throw new NotImplementedException();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            sw.Write(SlotId);
        }
    }
}