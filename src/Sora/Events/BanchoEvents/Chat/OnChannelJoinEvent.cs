using Sora.Attributes;
using Sora.Enums;
using Sora.EventArgs.BanchoEventArgs;
using Sora.Services;
using Channel = Sora.Objects.Channel;
using ChannelAvailable = Sora.Packets.Server.ChannelAvailable;
using ChannelJoinSuccess = Sora.Packets.Server.ChannelJoinSuccess;
using ChannelRevoked = Sora.Packets.Server.ChannelRevoked;

namespace Sora.Events.BanchoEvents.Chat
{
    [EventClass]
    public class OnChannelJoinEvent
    {
        private readonly ChannelService _cs;

        public OnChannelJoinEvent(ChannelService cs) => _cs = cs;

        [Event(EventType.BanchoChannelJoin)]
        public void OnChannelJoin(BanchoChannelJoinArgs args)
        {
            Channel channel;
            switch (args.ChannelName)
            {
                case "#spectator":
                    channel = args.Pr.Spectator?.Channel;
                    break;
                case "#multiplayer":
                    channel = args.Pr.ActiveMatch?.Channel;
                    break;
                default:
                    _cs.TryGet(args.ChannelName, out channel);
                    break;
            }

            if (channel == null)
            {
                args.Pr.Push(new ChannelRevoked(args.ChannelName));
                return;
            }

            channel.Leave(args.Pr);
            channel.Join(args.Pr);

            args.Pr.Push(new ChannelJoinSuccess(channel));

            channel.Push(new ChannelAvailable(channel));
        }
    }
}