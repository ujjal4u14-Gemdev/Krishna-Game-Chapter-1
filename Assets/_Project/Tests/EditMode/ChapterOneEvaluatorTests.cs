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

        private void Set<T>(string fieldName, T value)
        {
            typeof(PuzzleDefinition)
                .GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(definition, value);
        }
    }
}
