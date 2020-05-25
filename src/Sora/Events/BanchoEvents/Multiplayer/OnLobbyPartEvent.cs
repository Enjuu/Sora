using Sora.Attributes;
using Sora.Enums;
using Sora.EventArgs.BanchoEventArgs;
using Sora.Services;
using Lobby = Sora.Objects.Multiplayer.Lobby;

namespace Sora.Events.BanchoEvents.Multiplayer
{
    [EventClass]
    public class OnLobbyPartEvent
    {
        private readonly ChannelService channelService;

        public OnLobbyPartEvent(ChannelService channelService) => this.channelService = channelService;

        [Event(EventType.BanchoLobbyPart)]
        public void OnLobbyPart(BanchoLobbyPartArgs args)
        {
            if (channelService.TryGet("#lobby", out var lobbyChannel))
                lobbyChannel.Leave(args.Pr);

            Lobby.Self.Leave(args.Pr);
        }
    }
}