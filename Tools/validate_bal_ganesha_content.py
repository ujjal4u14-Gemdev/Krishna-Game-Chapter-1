#!/usr/bin/env python3
"""Validate the locked 54-level Bal Ganesha production tracker."""

from __future__ import annotations

import csv
import sys
from collections import Counter
from pathlib import Path


EXPECTED_COUNTS = {
    "EraseReveal": 13,
    "BinaryChoice": 15,
    "AimAndShoot": 26,
}

FIRST_FIVE_TARGETS = {
    1: "ENT_C01_001_Rope_Target",
    2: "ENT_C01_002_Spill_Target",
    3: "ENT_C01_003_CupboardDoor_Target",
    4: "ENT_C01_004_LightWeight_Target",
    5: "ENT_C01_005_LowerCover_Target",
}


def fail(message: str) -> None:
    print(f"ERROR: {message}", file=sys.stderr)


def main() -> int:
    repo_root = Path(__file__).resolve().parents[1]
    tracker = repo_root / "Docs" / "Bal_Ganesha" / "Bal_Ganesha_Level_Art_Tracker.csv"
    errors = 0

    with tracker.open(newline="", encoding="utf-8") as stream:
        rows = list(csv.DictReader(stream))

    if len(rows) != 54:
        fail(f"expected 54 level rows, found {len(rows)}")
        errors += 1

    levels = [int(row["Level"]) for row in rows]
    expected_levels = list(range(1, 55))
    if levels != expected_levels:
        fail("level numbers must be exactly 1 through 54 in order")
        errors += 1

    ids = [row["LevelId"] for row in rows]
    if len(ids) != len(set(ids)):
        fail("LevelId values must be unique")
        errors += 1

    for row in rows:
        level = int(row["Level"])
        expected_id = f"LVL_C01_{level:03d}"
        if row["LevelId"] != expected_id:
            fail(f"Level {level}: expected ID {expected_id}, found {row['LevelId']}")
            errors += 1

        expected_arc = "Missing Modaks" if level <= 25 else "Shadow Naga Cave"
        if row["Arc"] != expected_arc:
            fail(f"Level {level}: expected arc {expected_arc}, found {row['Arc']}")
            errors += 1

        for required in ("PuzzleFamily", "EnvironmentKit", "ArtObjective", "InteractiveTarget", "UniqueArtTier"):
            if not row[required].strip():
                fail(f"Level {level}: {required} is empty")
                errors += 1

        if level in FIRST_FIVE_TARGETS:
            expected_target = FIRST_FIVE_TARGETS[level]
            if row["InteractiveTarget"] != expected_target:
                fail(
                    f"Level {level}: expected first-slice target {expected_target}, "
                    f"found {row['InteractiveTarget']}"
                )
                errors += 1
            if row["ConceptStatus"] != "Prompt Ready":
                fail(f"Level {level}: expected ConceptStatus Prompt Ready")
                errors += 1

    counts = Counter(row["PuzzleFamily"] for row in rows)
    if dict(counts) != EXPECTED_COUNTS:
        fail(f"expected puzzle counts {EXPECTED_COUNTS}, found {dict(counts)}")
        errors += 1

    if errors:
        print(f"Bal Ganesha content validation failed with {errors} error(s).", file=sys.stderr)
        return 1

    print("Bal Ganesha content validation passed: 54 levels, 13 EraseReveal, 15 BinaryChoice, 26 AimAndShoot.")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
