using System;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Client
{
    public class StartSpectating : IPacket
    {
        public int ToSpectateId;
        public PacketId Id => PacketId.ClientStartSpectating;

        public void ReadFromStream(MStreamReader sr)
        {
            ToSpectateId = sr.ReadInt32();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            throw new NotImplementedException();
        }
    }
}