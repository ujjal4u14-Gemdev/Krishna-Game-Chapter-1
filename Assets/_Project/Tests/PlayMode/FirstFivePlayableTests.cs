using System.Collections;
using MythicPuzzle.Runtime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace MythicPuzzle.Tests
{
    public sealed class FirstFivePlayableTests
    {
        private static readonly string[] Targets =
        {
            "ENT_C01_001_Rope_Target",
            "ENT_C01_002_Spill_Target",
            "ENT_C01_003_CupboardDoor_Target",
            "ENT_C01_004_LightWeight_Target",
            "ENT_C01_005_LowerCover_Target"
        };

        [UnityTest]
        public IEnumerator EveryLevelCanCompleteAndRevealItsReward()
        {
            for (var number = 1; number <= 5; number++)
            {
                SceneManager.LoadScene($"Level_{number:000}_BalGanesha");
                yield return null;

                var director = Object.FindFirstObjectByType<PuzzleDirector>();
                var router = Object.FindFirstObjectByType<PuzzleInputRouter>();
                var registry = Object.FindFirstObjectByType<SceneRegistry>();
                Assert.That(director, Is.Not.Null, $"Level {number} has no director");
                Assert.That(router, Is.Not.Null, $"Level {number} has no input router");
                Assert.That(registry, Is.Not.Null, $"Level {number} has no scene registry");

                var deadline = Time.realtimeSinceStartup + 10f;
                while (Time.realtimeSinceStartup < deadline && director.State != PuzzleState.AwaitingInput)
                    yield return null;
                Assert.That(director.State, Is.EqualTo(PuzzleState.AwaitingInput), $"Level {number} did not start");

                router.RaiseEraseProgress(Targets[number - 1], 1f);
                deadline = Time.realtimeSinceStartup + 10f;
                while (Time.realtimeSinceStartup < deadline && director.State != PuzzleState.Complete)
                    yield return null;
                Assert.That(director.State, Is.EqualTo(PuzzleState.Complete), $"Level {number} did not complete");
                Assert.That(registry.TryGet("level_complete_visual", out var reward), Is.True);
                Assert.That(reward.gameObject.activeSelf, Is.True, $"Level {number} did not reveal its reward");

                if (number == 1)
                {
                    Assert.That(registry.TryGet("jar_hanging", out var jar), Is.True);
                    Assert.That(jar.gameObject.activeSelf, Is.False);
                }
            }
        }
    }
}
