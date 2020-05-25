using System;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Packets.Server
{
    public class SpectatorCantSpectate : IPacket
    {
        public readonly int UserId;

        public SpectatorCantSpectate(int userId) => UserId = userId;

        public PacketId Id => PacketId.ServerSpectatorCantSpectate;

        public void ReadFromStream(MStreamReader sr)
        {
            throw new NotImplementedException();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            sw.Write(UserId);
        }
    }
}