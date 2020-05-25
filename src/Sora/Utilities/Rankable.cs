using Sora.Enums;

namespace Sora.Utilities
{
    public static class Rankable
    {
        public static bool IsStatusRankable(Pisstaube.RankedStatus r)
        {
            switch (r)
            {
                case Pisstaube.RankedStatus.Ranked:
                case Pisstaube.RankedStatus.Approved:
                    return true;

                default:
                    return false;
            }
        }

        public static bool HasScoreboard(Pisstaube.RankedStatus r)
        {
            switch (r)
            {
                case Pisstaube.RankedStatus.Qualified:
                case Pisstaube.RankedStatus.Loved:
                case Pisstaube.RankedStatus.Ranked:
                case Pisstaube.RankedStatus.Approved:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsRankableMods(Mod m)
        {
            if (m.HasFlag(Mod.Easy) && m.HasFlag(Mod.HardRock))
                return false;

            if (m.HasFlag(Mod.NoFail) && (m.HasFlag(Mod.SuddenDeath) || m.HasFlag(Mod.Perfect) ||
                                          m.HasFlag(Mod.Relax) || m.HasFlag(Mod.Relax2)))
                return false;

            if (m.HasFlag(Mod.DoubleTime) && (m.HasFlag(Mod.DoubleTime) || m.HasFlag(Mod.Nightcore)))
                return false;

            if (m.HasFlag(Mod.Relax) && (m.HasFlag(Mod.Relax2) || m.HasFlag(Mod.NoFail) ||
                                         m.HasFlag(Mod.SuddenDeath) || m.HasFlag(Mod.Perfect)))
                return false;

            if (m.HasFlag(Mod.Relax2) && (m.HasFlag(Mod.Relax) || m.HasFlag(Mod.NoFail) ||
                                          m.HasFlag(Mod.SuddenDeath) || m.HasFlag(Mod.Perfect)))
                return false;

            return !m.HasFlag(Mod.Relax) && !m.HasFlag(Mod.Autoplay) && !m.HasFlag(Mod.Relax2);
        }
    }
}