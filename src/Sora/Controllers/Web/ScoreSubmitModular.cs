using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sora.Database;
using Sora.Database.Models;
using Sora.Enums;
using Sora.EventArgs.BanchoEventArgs;
using Sora.Services;
using Sora.Utilities;
using Beatmap = Sora.Utilities.Beatmap;
using Chart = Sora.Objects.Chart;
using Crypto = Sora.Utilities.Crypto;
using Hex = Sora.Utilities.Hex;
using LCol = Sora.Utilities.LCol;
using Logger = Sora.Utilities.Logger;
using Mod = Sora.Enums.Mod;
using ModUtil = Sora.Utilities.ModUtil;
using Pisstaube = Sora.Utilities.Pisstaube;
using PlayMode = Sora.Enums.PlayMode;
using RankedMods = Sora.Utilities.RankedMods;
using ScoreSubmissionParser = Sora.Utilities.ScoreSubmissionParser;

namespace Sora.Controllers.Web
{
    [ApiController]
    [Route("/web/")]
    public class ScoreSubmitModular : Controller
    {
        #region POST /web/osu-submit-modular-selector.php

        [HttpPost("osu-submit-modular-selector.php")]
        public async Task<IActionResult> PostSubmitModular(
            [FromServices] PresenceService ps,
            [FromServices] SoraDbContext ctx,
            [FromServices] EventManager ev,
            [FromServices] Pisstaube pisstaube,
            [FromServices] Bot.Sora sora,
            [FromServices] Config config
        )
        {
            if (!Directory.Exists("data/replays"))
                Directory.CreateDirectory("data/replays");

            string encScore = Request.Form["score"];
            string iv = Request.Form["iv"];
            string osuver = Request.Form["osuver"];
            string passwd = Request.Form["pass"];

            var (pass, score) = await ScoreSubmissionParser.ParseScore(ctx, encScore, iv, osuver);
            var dbUser = await DbUser.GetDbUser(ctx, score.ScoreOwner.UserName);

            if (dbUser == null)
                return Ok("error: pass");

            if (!dbUser.IsPassword(passwd))
                return Ok("error: pass");

            if (!ps.TryGet(dbUser.Id, out var pr))
                return Ok("error: pass"); // User not logged in in Bancho!

            if (!pass || !RankedMods.IsRanked(score.Mods))
            {
                var lb = await DbLeaderboard.GetLeaderboardAsync(ctx, dbUser);

                lb.IncreasePlaycount(score.PlayMode);
                lb.IncreaseScore((ulong) score.TotalScore, false, score.PlayMode);

                await lb.SaveChanges(ctx);

                // Send to other People
                await ev.RunEvent(
                    EventType.BanchoUserStatsRequest,
                    new BanchoUserStatsRequestArgs {UserIds = new List<int> {score.Id}, Pr = pr}
                );

                // Send to self
                await ev.RunEvent(
                    EventType.BanchoSendUserStatus,
                    new BanchoSendUserStatusArgs {Status = pr.Status, Pr = pr}
                );

                return Ok("Thanks for your hard work! onii-chyan~"); // even though, we're Sora, we can still be cute!
            }

            var replayFileData = Request.Form.Files.GetFile("score");

            var dbScore = new DbScore
            {
                Accuracy = score.ComputeAccuracy(),
                Count100 = score.Count100,
                Count50 = score.Count50,
                Count300 = score.Count300,
                Date = score.Date,
                Mods = score.Mods,
                CountGeki = score.CountGeki,
                CountKatu = score.CountKatu,
                CountMiss = score.CountMiss,
                FileMd5 = score.FileMd5,
                MaxCombo = score.MaxCombo,
                PlayMode = score.PlayMode,
                ScoreOwner = dbUser,
                TotalScore = score.TotalScore,
                UserId = dbUser.Id,
            };

            await pisstaube.DownloadBeatmapAsync(dbScore.FileMd5);

            var set = await pisstaube.FetchBeatmapSetAsync(dbScore.FileMd5);

            var bm = set?.ChildrenBeatmaps.First(x => x.FileMD5 == dbScore.FileMd5) ?? new Beatmap();


            await using (var m = new MemoryStream())
            {
                replayFileData.CopyTo(m);
                m.Position = 0;
                dbScore.ReplayMd5 = Hex.ToHex(Crypto.GetMd5(m)) ?? string.Empty;
                if (!string.IsNullOrEmpty(dbScore.ReplayMd5))
                {
                    await using var replayFile = System.IO.File.Create($"data/replays/{dbScore.ReplayMd5}");
                    m.Position = 0;
                    m.WriteTo(replayFile);
                    m.Close();
                    replayFile.Close();
                }
            }

            dbScore.PerformancePoints = dbScore.ComputePerformancePoints();

            var oldScore = await DbScore.GetLatestScore(ctx, dbScore);

            var oldLb = await DbLeaderboard.GetLeaderboardAsync(ctx, dbScore.ScoreOwner);
            var oldStdPos = oldLb.GetPosition(ctx, dbScore.PlayMode);

            var oldAcc = oldLb.GetAccuracy(ctx, dbScore.PlayMode);
            double newAcc;

            if (oldScore != null && oldScore.TotalScore <= dbScore.TotalScore)
            {
                ctx.Remove(oldScore);
                System.IO.File.Delete($"data/replays/{oldScore.ReplayMd5}");

                await DbScore.InsertScore(ctx, dbScore);
            }
            else if (oldScore == null)
            {
                await DbScore.InsertScore(ctx, dbScore);
            }
            else
            {
                System.IO.File.Delete($"data/replays/{oldScore.ReplayMd5}");
            }

            var newlb = await DbLeaderboard.GetLeaderboardAsync(ctx, dbScore.ScoreOwner);

            newlb.IncreasePlaycount(dbScore.PlayMode);
            newlb.IncreaseScore((ulong) dbScore.TotalScore, true, dbScore.PlayMode);
            newlb.IncreaseScore((ulong) dbScore.TotalScore, false, dbScore.PlayMode);

            if (Rankable.IsStatusRankable(set.RankedStatus))
                newlb.UpdatePp(ctx, dbScore.PlayMode);

            await newlb.SaveChanges(ctx);

            var newStdPos = newlb.GetPosition(ctx, dbScore.PlayMode);
            newAcc = newlb.GetAccuracy(ctx, dbScore.PlayMode);

            var newScore = await DbScore.GetLatestScore(ctx, dbScore);

            ulong oldRankedScore;
            ulong newRankedScore;

            double oldPp;
            double newPp;

            switch (dbScore.PlayMode)
            {
                case PlayMode.Osu:
                    oldRankedScore = oldLb.RankedScoreOsu;
                    newRankedScore = newlb.RankedScoreOsu;

                    oldPp = oldLb.PerformancePointsOsu;
                    newPp = newlb.PerformancePointsOsu;
                    break;
                case PlayMode.Taiko:
                    oldRankedScore = oldLb.RankedScoreTaiko;
                    newRankedScore = newlb.RankedScoreTaiko;

                    oldPp = oldLb.PerformancePointsTaiko;
                    newPp = newlb.PerformancePointsTaiko;
                    break;
                case PlayMode.Ctb:
                    oldRankedScore = oldLb.RankedScoreCtb;
                    newRankedScore = newlb.RankedScoreCtb;

                    oldPp = oldLb.PerformancePointsCtb;
                    newPp = newlb.PerformancePointsCtb;
                    break;
                case PlayMode.Mania:
                    oldRankedScore = oldLb.RankedScoreMania;
                    newRankedScore = newlb.RankedScoreMania;

                    oldPp = oldLb.PerformancePointsMania;
                    newPp = newlb.PerformancePointsMania;
                    break;
                default:
                    return Ok("");
            }

            var newScorePosition = newScore != null ? await newScore.Position(ctx) : 0;
            var oldScorePosition = oldScore != null ? await oldScore.Position(ctx) : 0;

            if (newScorePosition == 1)
                sora.SendMessage(
                    $"[http://{config.Server.ScreenShotHostname}/{dbScore.ScoreOwner.Id} {dbScore.ScoreOwner.UserName}] " +
                    $"has reached #1 on [https://osu.ppy.sh/b/{bm.BeatmapID} {set?.Title} [{bm.DiffName}]] " +
                    $"using {ModUtil.ToString(newScore.Mods)} " +
                    $"Good job! +{newScore.PerformancePoints:F}PP",
                    "#announce",
                    false
                );

            Logger.Info(
                $"{LCol.RED}{dbScore.ScoreOwner.UserName}",
                $"{LCol.PURPLE}( {dbScore.ScoreOwner.Id} ){LCol.WHITE}",
                $"has just submitted a Score! he earned {LCol.BLUE}{newScore?.PerformancePoints:F}PP",
                $"{LCol.WHITE}with an Accuracy of {LCol.RED}{newScore?.Accuracy * 100:F}",
                $"{LCol.WHITE}on {LCol.YELLOW}{set?.Title} [{bm.DiffName}]",
                $"{LCol.WHITE}using {LCol.BLUE}{ModUtil.ToString(newScore?.Mods ?? Mod.None)}"
            );

            var bmChart = new Chart(
                "beatmap",
                "Beatmap Ranking",
                $"https://osu.ppy.sh/b/{bm.BeatmapID}",
                oldScorePosition,
                newScorePosition,
                oldScore?.MaxCombo ?? 0,
                newScore?.MaxCombo ?? 0,
                oldScore?.Accuracy * 100 ?? 0,
                newScore?.Accuracy * 100 ?? 0,
                (ulong) (oldScore?.TotalScore ?? 0),
                (ulong) (newScore?.TotalScore ?? 0),
                oldScore?.PerformancePoints ?? 0,
                newScore?.PerformancePoints ?? 0,
                newScore?.Id ?? 0
            );

            var overallChart = new Chart(
                "overall",
                "Global Ranking",
                $"https://osu.ppy.sh/u/{dbUser.Id}",
                (int) oldStdPos,
                (int) newStdPos,
                0,
                0,
                oldAcc * 100,
                newAcc * 100,
                oldRankedScore,
                newRankedScore,
                oldPp,
                newPp,
                newScore?.Id ?? 0,
                AchievementProcessor.ProcessAchievements(
                    ctx, dbScore.ScoreOwner, score, bm, set, oldLb, newlb
                )
            );

            pr["LB"] = newlb;
            pr.Stats.Accuracy = (float) newlb.GetAccuracy(ctx, score.PlayMode);
            pr.Stats.Position = newlb.GetPosition(ctx, score.PlayMode);
            switch (score.PlayMode)
            {
                case PlayMode.Osu:
                    pr.Stats.PerformancePoints = (ushort) newlb.PerformancePointsOsu;
                    pr.Stats.TotalScore = (ushort) newlb.TotalScoreOsu;
                    pr.Stats.RankedScore = (ushort) newlb.RankedScoreOsu;
                    pr.Stats.PlayCount = (ushort) newlb.PlayCountOsu;
                    break;
                case PlayMode.Taiko:
                    pr.Stats.PerformancePoints = (ushort) newlb.PerformancePointsTaiko;
                    pr.Stats.TotalScore = (ushort) newlb.TotalScoreTaiko;
                    pr.Stats.RankedScore = (ushort) newlb.RankedScoreTaiko;
                    pr.Stats.PlayCount = (ushort) newlb.PlayCountTaiko;
                    break;
                case PlayMode.Ctb:
                    pr.Stats.PerformancePoints = (ushort) newlb.PerformancePointsCtb;
                    pr.Stats.TotalScore = (ushort) newlb.TotalScoreCtb;
                    pr.Stats.RankedScore = (ushort) newlb.RankedScoreCtb;
                    pr.Stats.PlayCount = (ushort) newlb.PlayCountCtb;
                    break;
                case PlayMode.Mania:
                    pr.Stats.PerformancePoints = (ushort) newlb.PerformancePointsMania;
                    pr.Stats.TotalScore = (ushort) newlb.TotalScoreMania;
                    pr.Stats.RankedScore = (ushort) newlb.RankedScoreMania;
                    pr.Stats.PlayCount = (ushort) newlb.PlayCountMania;
                    break;
            }

            // Send to other People
            await ev.RunEvent(
                EventType.BanchoUserStatsRequest,
                new BanchoUserStatsRequestArgs {UserIds = new List<int> {score.Id}, Pr = pr}
            );

            // Send to self
            await ev.RunEvent(
                EventType.BanchoSendUserStatus,
                new BanchoSendUserStatusArgs {Status = pr.Status, Pr = pr}
            );

            return Ok(
                $"beatmapId:{bm.BeatmapID}|beatmapSetId:{bm.ParentSetID}|beatmapPlaycount:0|beatmapPasscount:0|approvedDate:\n\n" +
                bmChart.ToOsuString() + "\n" + overallChart.ToOsuString()
            );
        }

        #endregion
    }
}