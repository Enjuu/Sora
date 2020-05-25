using MStreamReader = Sora.Utilities.MStreamReader;
using MStreamWriter = Sora.Utilities.MStreamWriter;
using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs.BanchoEventArgs
{
    public class BanchoLoginRequestArgs : IEventArgs, INeedPresence
    {
        public string IpAddress;
        public MStreamReader Reader;
        public MStreamWriter Writer;

        public Presence Pr { get; set; }
    }
}