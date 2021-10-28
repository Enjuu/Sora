using Sora.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Bot.Commands
{
    class StealthCommand : ISoraCommand
    {
        public string Command => "stealth";
        public string Description => "Stealh test";
        public List<Argument> Args => new List<Argument>();
        public int ExpectedArgs => 0;
        public Permission RequiredPermission => Permission.From(Permission.GROUP_DEVELOPER);

        public bool Execute(Presence executor, string ch, string[] args)
        {
            if (executor.AdminStealth)
            {
                executor.Alert("You're visible again.");
                executor.AdminStealth = false;
            } 
            else
            {
                executor.Alert("You're hidden.");
                executor.AdminStealth = true;
            }

            return true;
        }
    }
}
