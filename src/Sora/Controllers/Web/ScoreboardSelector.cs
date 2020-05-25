using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Sora.Database;
using Sora.Database.Models;
using Beatmap = Sora.Utilities.Beatmap;
using BeatmapSet = Sora.Utilities.BeatmapSet;
using Cache = Sora.Allocation.Cache;
using Crypto = Sora.Utilities.Crypto;
using Hex = Sora.Utilities.Hex;
using Logger = Sora.Utilities.Logger;
using Mod = Sora.Enums.Mod;
using Pisstaube = Sora.Utilities.Pisstaube;
using PlayMode = Sora.Enums.PlayMode;
using Scoreboard = Sora.Objects.Scoreboard;
using ScoreboardType = Sora.Enums.ScoreboardType;

namespace Sora.Controllers.Web
{
    [ApiController]
    [Route("/web/")]
    public class ScoreboardSelector : Controller
    {
        #region GET /web/osu-osz2-getscores.php

        [HttpGet("osu-osz2-getscores.php")]
        public async Task<IActionResult> GetScoreResult(
            [FromQuery(Name = "v")] ScoreboardType type,
            [FromQuery(Name = "c")] string fileMd5,
            [FromQuery(Name = "f")] string f,
            [FromQuery(Name = "m")] PlayMode playMode,
            [FromQuery(Name = "i")] int i,
            [FromQuery(Name = "mods")] Mod mods,
            [FromQuery(Name = "us")] string us,
            [FromQuery(Name = "ha")] string pa,
            [FromServices] IServiceProvider serviceProvider,
            [FromServices] SoraDbContext ctx,
            [FromServices] DbContextPool<SoraDbContext> ctxPool,
            [FromServices] Pisstaube pisstaube,
            [FromServices] Cache cache)
        {
            try
            {
                var dbUser = await DbUser.GetDbUser(ctx, us);
                if (dbUser?.IsPassword(pa) != true)
                    return Ok("error: pass");

                var cacheHash =
                    Hex.ToHex(
                        Crypto.GetMd5(
                            $"{fileMd5}{playMode}{mods}{type}{dbUser.Id}{dbUser.UserName}"
                        )
                    );

                if (cache.TryGet($"sora:Scoreboards:{cacheHash}", out string cachedData))
                    return Ok(cachedData);

                var scores = DbScore.GetScores(ctx, fileMd5, dbUser, playMode,
                    type == ScoreboardType.Friends,
                    type == ScoreboardType.Country,
                    type == ScoreboardType.Mods,
                    mods);
                
                BeatmapSet set = null;
                DbScore ownScore = null;

                var beatmap = DbBeatmap.GetBeatmap(ctx, fileMd5);
                if (beatmap == null)
                {
                    var apiSet = await pisstaube.FetchBeatmapSetAsync(fileMd5);

                    if (apiSet == null)
                        goto JustContinue;

                    var beatmaps = DbBeatmap.FromBeatmapSet(apiSet).ToList();
                    var beatmapChecksums = beatmaps.Select(s => s.FileMd5);
                    var dbBeatmaps =
                        ctx.Beatmaps.Where(rset => beatmapChecksums.Any(lFileMd5 => rset.FileMd5 == lFileMd5))
                            .ToList();

                    var pool = serviceProvider.GetRequiredService<DbContextPool<SoraDbContext>>();
                    Task.WaitAll(beatmaps.Select(rawBeatmap => Task.Run(async () =>
                    {
                        var context = pool.Rent();

                        try
                        {
                            var dbBeatmap = dbBeatmaps.FirstOrDefault(s => s.FileMd5 == rawBeatmap.FileMd5);

                            if (dbBeatmap != null && (dbBeatmap.Flags & DbBeatmapFlags.RankedFreeze) != 0)
                            {
                                rawBeatmap.RankedStatus = dbBeatmap.RankedStatus;
                                rawBeatmap.Flags = dbBeatmap.Flags;
                            }

                            context.Beatmaps.AddOrUpdate(rawBeatmap);
                            await context.SaveChangesAsync();
                        }
                        finally
                        {
                            pool.Return(context);
                        }
                    })).ToArray());

                    beatmap = beatmaps.FirstOrDefault(s => s.FileMd5 == fileMd5);
                }

                ownScore = await DbScore.GetLatestScore(ctx, new DbScore
                {
                    FileMd5 = fileMd5,
                    UserId = dbUser.Id,
                    PlayMode = playMode,
                    TotalScore = 0,
                });
                
                if (beatmap != null)
                    set = new BeatmapSet
                    {
                        SetID = beatmap.Id,
                        Artist = beatmap.Artist,
                        Title = beatmap.Title,
                        RankedStatus = beatmap.RankedStatus,

                        ChildrenBeatmaps = new List<Beatmap>
                        {
                            new Beatmap
                            {
                                FileMD5 = beatmap.FileMd5,
                                DiffName = beatmap.DiffName,
                                ParentSetID = beatmap.SetId,
                                BeatmapID = beatmap.Id,
                                Mode = beatmap.PlayMode,
                            },
                        },
                    };

                JustContinue:
                var sboard = new Scoreboard(set?.ChildrenBeatmaps.FirstOrDefault(bm => bm.FileMD5 == fileMd5),
                    set, scores, ownScore);

                cache.Set($"sora:Scoreboards:{cacheHash}", cachedData = await sboard.ToOsuString(ctxPool), TimeSpan.FromSeconds(30));
                return Ok(cachedData);
            }
            catch (Exception ex)
            {
                Logger.Err(ex);
                return Ok("Failed");
            }
        }

        #endregion
    }
}