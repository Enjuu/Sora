using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Sora.Database;
using Sora.Database.Models;
using Sora.Enums;
using Sora.Utilities;

namespace Sora.Objects
{
    public class Scoreboard
    {
        private Beatmap _bm;
        private readonly BeatmapSet _parent;
        private IAsyncEnumerable<DbScore> _scores;
        private DbScore _ownScore;

        public Scoreboard(Beatmap bm, BeatmapSet bmParent, IAsyncEnumerable<DbScore> scores, DbScore ownScore = null)
        {
            _bm = bm;
            _parent = bmParent;
            _scores = scores;
            _ownScore = ownScore;
        }

        public async Task<string> ToOsuString(DbContextPool<SoraDbContext> ctxPool)
        {
            var returnString = string.Empty;

            if (_bm == null)
            {
                Logger.Err("Failed to set Beatmap! Beatmap is null!");
                return $"{(int) RankedStatus.NeedUpdate}|false\n";
            }

            var count = 0;
            if (_ownScore != null)
            {
                var ctx = ctxPool.Rent();
                try
                {
                    returnString += $"{_ownScore?.ToOsuString(ctx)}\n";
                }
                finally
                {
                    ctxPool.Return(ctx);
                }
            }

            await foreach (var score in _scores)
            {
                count++;
                var ctx = ctxPool.Rent();
                try
                {
                    returnString += $"{score.ToOsuString(ctx)}\n";
                }
                finally
                {
                    ctxPool.Return(ctx);
                }
            }

            return ScoreboardHeader(count) + returnString;
        }

        private string ScoreboardHeader(int count)
        {
            if (_bm != null && _parent != null)
                return $"{(int) Fixer.FixRankedStatus(_parent.RankedStatus)}|false|" +
                       $"{_bm.BeatmapID}|" +
                       $"{_bm.ParentSetID}|" +
                       $"{count}\n" +
                       "0\n" +
                       $"{_parent.Artist} - {_parent.Title} [{_bm.DiffName}]\n" +
                       "10.0\n";

            return $"{(int) RankedStatus.NeedUpdate}|false|" +
                   "-1|" +
                   "-1|" +
                   "-1\n" +
                   "0\n" +
                   "Unknown\n" +
                   "0\n";
        }
    }
}