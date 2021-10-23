using Sora.Objects;
using Sora.Services;
using Sora.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Bot.Commands
{
    class RtxCommand : ISoraCommand
    {
        public string Command => "rtx";
        public string Description => "Send an RTX to a user";
        public List<Argument> Args
            => new List<Argument>
            {
                new Argument {ArgName = "Username"},
                new Argument {ArgName = "Message"},
            };
        public int ExpectedArgs => -1;
        public Permission RequiredPermission => Permission.From(Permission.GROUP_DEVELOPER);

        private PresenceService _ps;

        private readonly Sora sora;

        public RtxCommand(PresenceService ps)
        {
            _ps = ps;
        }

        public bool Execute(Presence executer, string ch, string[] args)
        {
            if (_ps.TryGet(args[1], out var pr))
            {
                pr.Push(new Rtx(args.Skip(1).ToString()));
                sora.SendMessage("rtx'd " + args[1] + ": " + args.Skip(1).ToString(), executer.User.UserName, true);
            }
            else
            {
                sora.SendMessage("User " + args[1] + "isn't online.", executer.User.UserName, true);
            }
            return true;
        }
    }
}
