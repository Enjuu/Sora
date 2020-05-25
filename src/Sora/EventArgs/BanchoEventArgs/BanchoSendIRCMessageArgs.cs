using MessageStruct = Sora.Packets.Server.MessageStruct;
using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoSendIrcMessageArgs : IEventArgs, INeedPresence
    {
        public MessageStruct Message;
        public Presence Pr { get; set; }
    }
}