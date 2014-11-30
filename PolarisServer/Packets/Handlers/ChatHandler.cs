﻿using System;

namespace PolarisServer.Packets.Handlers
{
    [PacketHandlerAttr(0x7, 0x0)]
    public class ChatHandler : PacketHandler
    {

        #region implemented abstract members of PacketHandler

        public override void handlePacket(Client context, byte[] data, uint position, uint size)
        {
            if (context.Character == null)
                return;

            //PacketReader reader = new PacketReader(data, position, size);

            var bytes = BitConverter.GetBytes((uint)context.User.PlayerID);
            data[0] = bytes[0];
            data[1] = bytes[1];
            data[2] = bytes[2];
            data[3] = bytes[3];

            data[9] = 4; // From player
            /*
            PacketReader reader = new PacketReader(data, position, size);
            reader.BaseStream.Seek(0x10, System.IO.SeekOrigin.Begin);
            UInt32 channel = reader.ReadUInt32();
            string message = reader.ReadUTF16(0x9d3f, 0x44);

            Logger.WriteLine("[CHT] <{0}> <{1}>", context.Character.Name, message);

            PacketWriter writer = new PacketWriter();
            writer.WritePlayerHeader((uint)context.Character.CharacterID);
            writer.Write((uint)channel);
            writer.WriteUTF16(message, 0x9d3f, 0x44);
            */
            foreach (Client c in Server.Instance.Clients)
            {
                if (c.Character == null)
                    continue;

                c.SendPacket(0x7, 0x0, 0x44, data);
            }
        }

        #endregion
    }
}
