using Mod = Sora.Enums.Mod;
using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoMatchChangeModsArgs : IEventArgs, INeedPresence
    {
        public Mod Mods { get; set; }
        public Presence Pr { get; set; }
    }
}