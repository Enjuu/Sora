﻿#region LICENSE
/*
    Sora - A Modular Bancho written in C#
    Copyright (C) 2019 Robin A. P.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
#endregion

using IPacket = Sora.Interfaces.IPacket;
using MStreamReader = Sora.Helpers.MStreamReader;
using MStreamWriter = Sora.Helpers.MStreamWriter;
using PacketId = Sora.Enums.PacketId;

namespace Sora.Packets.Client
{
    public class ChannelLeave : IPacket
    {
        public string ChannelName;
        public PacketId Id => PacketId.ClientChannelJoin;

        public void ReadFromStream(MStreamReader sr)
        {
            ChannelName = sr.ReadString();
        }

        public void WriteToStream(MStreamWriter sw)
        {
            sw.Write(ChannelName);
        }
    }
}