using Sora.Objects;
using Sora.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Bot.Commands
{
    class AnnounceClientCommand : ISoraCommand
    {
        public string Command => "announceclient";
        public string Description => "Send an announce to a user.";
        public List<Argument> Args
            => new List<Argument>
            {
                new Argument {ArgName = "Username"},
                new Argument {ArgName = "Message"},
            };

        public int ExpectedArgs => -1;
        public Permission RequiredPermission => Permission.From(Permission.GROUP_CHAT_MOD);

        private PresenceService _ps;

        private Sora sora;

        public AnnounceClientCommand(PresenceService ps)
        {
            _ps = ps;
        }

        public bool Execute(Presence executer, string ch, string[] args)
        {
            if (_ps.TryGet(args[1], out var pr))
            {
                pr.Alert(args.Skip(1).ToString());
                sora.SendMessage("Alerted " + args[1] + " with message" + args.Skip(1).ToString(), executer.User.UserName, true);
            }
            else
            {
                sora.SendMessage("The user" + args[1] + " isn't online.", executer.User.UserName, true);
            }

            return true;
        }
    }
}
