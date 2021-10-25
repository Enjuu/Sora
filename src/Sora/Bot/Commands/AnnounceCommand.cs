using Sora.Objects;
using Sora.Packets.Server;
using Sora.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Bot.Commands
{
    class AnnounceCommand : ISoraCommand
    {
        public string Command => "announce";
        public string Description => "Send an announce to all users.";
        public List<Argument> Args => new List<Argument> { new Argument { ArgName = "Message" } };
        public int ExpectedArgs => -1;
        public Permission RequiredPermission => Permission.From(Permission.GROUP_CHAT_MOD);

        private readonly PresenceKeeper _pk;
        public AnnounceCommand(PresenceKeeper pk)
        {
            _pk = pk;
        }

        public bool Execute(Presence executer, string ch, string[] args)
        {
            _pk.Push(new Announce(args.ToString()));
            return true;
        }
    }
}
