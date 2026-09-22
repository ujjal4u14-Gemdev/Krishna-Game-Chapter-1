using System.Collections.Generic;
using System.Reflection;
using MythicPuzzle.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace MythicPuzzle.Tests
{
    public sealed class ChapterOneEvaluatorTests
    {
        private PuzzleDefinition definition;

        [SetUp]
        public void SetUp() => definition = ScriptableObject.CreateInstance<PuzzleDefinition>();

        [TearDown]
        public void TearDown() => Object.DestroyImmediate(definition);

        [Test]
        public void BinaryChoice_AcceptsConfiguredChoice()
        {
            Set("correctEntityIds", new List<string> { "safe_route" });
            var evaluator = new BinaryChoiceEvaluator();
            evaluator.Initialize(definition);

            Assert.That(
                evaluator.Evaluate(new PuzzleInputSignal(PuzzleInputKind.Tap, "safe_route")),
                Is.EqualTo(PuzzleEvaluation.Correct));
        }

        [Test]
        public void EraseReveal_WaitsUntilCoverageThreshold()
        {
            Set("correctEntityIds", new List<string> { "rope_mask" });
            Set("requiredProgress", 0.7f);
            var evaluator = new EraseRevealEvaluator();
            evaluator.Initialize(definition);

            Assert.That(
                evaluator.Evaluate(new PuzzleInputSignal(PuzzleInputKind.EraseProgress, "rope_mask", scalarValue: 0.69f)),
                Is.EqualTo(PuzzleEvaluation.Continue));
            Assert.That(
                evaluator.Evaluate(new PuzzleInputSignal(PuzzleInputKind.EraseProgress, "rope_mask", scalarValue: 0.7f)),
                Is.EqualTo(PuzzleEvaluation.Correct));
        }

        [Test]
        public void AimAndShoot_RejectsHazardAndAcceptsTarget()
        {
            Set("correctEntityIds", new List<string> { "modak_jar" });
            var evaluator = new AimAndShootEvaluator();
            evaluator.Initialize(definition);

            Assert.That(
                evaluator.Evaluate(new PuzzleInputSignal(PuzzleInputKind.ProjectileHit, "bell")),
                Is.EqualTo(PuzzleEvaluation.Incorrect));
            Assert.That(
                evaluator.Evaluate(new PuzzleInputSignal(PuzzleInputKind.ProjectileHit, "modak_jar")),
                Is.EqualTo(PuzzleEvaluation.Correct));
        }

        [Test]
        public void LevelArtSet_ResolvesBindingByStableSlotId()
        {
            var artSet = ScriptableObject.CreateInstance<LevelArtSet>();
            try
            {
                SetField(artSet, "bindings", new List<LevelArtBinding>
                {
                    new LevelArtBinding { slotId = "INT_C01_001_Target", tint = Color.white }
                });

                Assert.That(artSet.TryGet("INT_C01_001_Target", out var binding), Is.True);
                Assert.That(binding.slotId, Is.EqualTo("INT_C01_001_Target"));
                Assert.That(artSet.TryGet("missing", out _), Is.False);
            }
            finally
            {
                Object.DestroyImmediate(artSet);
            }
        }

        private void Set<T>(string fieldName, T value)
        {
            SetField(definition, fieldName, value);
        }

        private static void SetField<TObject, TValue>(TObject target, string fieldName, TValue value)
        {
            typeof(TObject)
                .GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(target, value);
        }
    }
}
