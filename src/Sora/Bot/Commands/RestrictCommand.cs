using System;
using System.Collections.Generic;
using Presence = Sora.Objects.Presence;
using Sora.Database;
using Sora.Database.Models;
using Sora.Packets.Server;
using Sora.EventArgs.BanchoEventArgs;
using Sora.Enums;
using Sora.Services;

namespace Sora.Bot.Commands
{
    public class RestrictCommand : ISoraCommand
    {
        public string Command => "restrict";
        public string Description => "Restrict a user.";

        public List<Argument> Args
            => new List<Argument>
            {
                new Argument {ArgName = "User"},
                new Argument {ArgName = "Duration (10d)"},
                new Argument {ArgName = "Reason"},
            };

        public int ExpectedArgs => 3;
        public Permission RequiredPermission => Permission.From(Permission.ADMIN_RESTRICT);

        private SoraDbContext _context;

        public RestrictCommand(SoraDbContext context) => _context = context;

        private PresenceService ps;

        private Sora sora;

        public bool Execute(Presence executor, string ch, string[] args)
        {
            if (ps.TryGet(args[1], out var pr))
            {
                sora.SendMessage("Your Account is currently in restricted mode.", args[1], true);

                pr.Push(new LoginResponse(LoginResponses.Failed));
            }

            DbUser.RestrictUser(_context, executor.User.UserName, args[1], args[2]);

            if (ch.StartsWith('#'))
                sora.SendMessage("Restricted " + args[1] + " for " + args[2], ch, false);
            else
                sora.SendMessage("Restricted " + args[1] + " for " + args[2], ch, true);

            return true;
        }
    }
}
