﻿using Sora.Enums;
using Sora.Helpers;

namespace Sora.Packets.Server
{
    public class Announce : IPacketSerializer
    {
        public string Message;
        public Announce(string message) => Message = message;

        public PacketId Id => PacketId.ServerAnnounce;

        public void ReadFromStream(MStreamReader sr) => Message = sr.ReadString();

        public void WriteToStream(MStreamWriter sw) => sw.Write(Message, false);
    }
}
