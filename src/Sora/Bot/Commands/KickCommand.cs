using Sora.Database;
using Sora.Database.Models;
using Sora.Enums;
using Sora.Packets.Server;
using Sora.Services;
using System;
using System.Collections.Generic;
using Presence = Sora.Objects.Presence;

namespace Sora.Bot.Commands
{
    public class KickCommand : ISoraCommand
    {
        public string Command => "kick";
        public string Description => "Kick somebody from your Server";
        public List<Argument> Args => new List<Argument> {new Argument {ArgName = "Username"}};
        public int ExpectedArgs => 1;
        public Permission RequiredPermission => Permission.From(Permission.ADMIN_KICK);

        private Sora sora;

        private PresenceService ps;

        public bool Execute(Presence executer, string ch, string[] args)
        {
            if (ps.TryGet(args[1], out var pr))
            {
                sora.SendMessage("You have been kicked from the server.", args[1], true);

                sora.SendMessage(args[1] + " has been kicked from the server.", executer.User.UserName, true);

                pr.Push(new LoginResponse(LoginResponses.Failed));
                return true;
            }
            else
            {
                sora.SendMessage("This user isn't online.", executer.User.UserName, true);
                return true;
            }
        }
    }
}
