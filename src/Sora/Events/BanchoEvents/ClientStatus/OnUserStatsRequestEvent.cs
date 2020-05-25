using System.Linq;
using Sora.Attributes;
using Sora.Enums;
using Sora.EventArgs.BanchoEventArgs;
using Sora.Services;
using ErrorStates = Sora.Enums.ErrorStates;
using HandleUpdate = Sora.Packets.Server.HandleUpdate;
using HandleUserQuit = Sora.Packets.Server.HandleUserQuit;
using UserQuitStruct = Sora.Packets.Server.UserQuitStruct;

namespace Sora.Events.BanchoEvents.ClientStatus
{
    [EventClass]
    public class OnUserStatsRequestEvent
    {
        private readonly PresenceService _ps;

        public OnUserStatsRequestEvent(PresenceService ps) => _ps = ps;

        [Event(EventType.BanchoUserStatsRequest)]
        public void OnUserStatsRequest(BanchoUserStatsRequestArgs args)
        {
            foreach (var id in args.UserIds.Where(id => id != args.Pr.User.Id))
            {
                if (!_ps.TryGet(id, out var opr))
                {
                    args.Pr.Push(new HandleUserQuit(new UserQuitStruct {UserId = id, ErrorState = ErrorStates.Ok}));
                    continue;
                }

                args.Pr.Push(new HandleUpdate(opr));
            }
        }
    }
}