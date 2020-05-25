using System;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Client
{
    public class MatchChangeMods : IPacket
    {
        public Mod Mods;
        public PacketId Id => PacketId.ClientMatchChangeMods;

        public void ReadFromStream(MStreamReader sr)
        {
            Mods = (Mod) sr.ReadUInt32();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            throw new NotImplementedException();
        }
    }
}