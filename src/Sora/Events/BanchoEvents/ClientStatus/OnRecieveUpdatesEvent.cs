using Sora.Attributes;
using Sora.Enums;
using Sora.EventArgs.BanchoEventArgs;
using HandleUpdate = Sora.Packets.Server.HandleUpdate;

namespace Sora.Events.BanchoEvents.ClientStatus
{
    [EventClass]
    public class OnRecieveUpdatesEvent
    {
        [Event(EventType.BanchoReceiveUpdates)]
        public void OnRecieveUpdates(BanchoEmptyEventArgs args)
        {
            args.Pr.Push(new HandleUpdate(args.Pr));
        }
    }
}