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
    }
}