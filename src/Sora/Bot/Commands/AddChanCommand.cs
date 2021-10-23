using Sora.Objects;
using Sora.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Bot.Commands
{
    class AddChanCommand : ISoraCommand
    {
        public string Command => "addchan";
        public string Description => "Add an Channel.";
        public List<Argument> Args => new List<Argument> { new Argument { ArgName = "Channelname" } };
        public int ExpectedArgs => 1;
        public Permission RequiredPermission => Permission.From(Permission.GROUP_ADMIN);
        private ChannelService _cs;
        public AddChanCommand(ChannelService cs)
        {
            _cs = cs;
        }

        public bool Execute(Presence executer, string ch, string[] args)
        {
            return true;
        }
    }
}
