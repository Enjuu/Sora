using Presence = Sora.Objects.Presence;

namespace Sora.EventArgs
{
    public interface INeedPresence
    {
        Presence Pr { get; set; }
    }
}