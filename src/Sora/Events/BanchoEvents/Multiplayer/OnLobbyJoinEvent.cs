using Sora.Attributes;
using Sora.Enums;
using Sora.EventArgs.BanchoEventArgs;
using Sora.Services;
using Lobby = Sora.Objects.Multiplayer.Lobby;
using MatchNew = Sora.Packets.Server.MatchNew;

namespace Sora.Events.BanchoEvents.Multiplayer
{
    [EventClass]
    public class OnLobbyJoinEvent
    {
        private readonly ChannelService channelService;

        public OnLobbyJoinEvent(ChannelService channelService) => this.channelService = channelService;

        [Event(EventType.BanchoLobbyJoin)]
        public void OnLobbyJoin(BanchoLobbyJoinArgs args)
        {
            foreach (var room in Lobby.Self.Rooms)
                args.Pr.Push(new MatchNew(room));

            if (channelService.TryGet("#lobby", out var lobbyChannel))
                lobbyChannel.Join(args.Pr);

            Lobby.Self.Leave(args.Pr);
            Lobby.Self.Join(args.Pr);
        }
    }
}