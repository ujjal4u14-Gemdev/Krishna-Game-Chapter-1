# Chapter 1 Production Bible — 54-Level Scope

## Verified reference structure

- The supplied walkthrough is titled **Level 1–54** and has a duration of 9:46.
- Additional indexed walkthroughs separate the content into:
  - **Levels 1–25: Collecting Makhan**
  - **Levels 26–54: Aghasura Caves**
- Production scope is locked to **all 54 levels**, combining both reference story arcs in one Chapter 1 deliverable.

## Accuracy limitation

The public stream exposes no captions or transcript, and its gameplay frames do not render in the current analysis browser. Exact per-level actions must therefore be extracted from an uploaded MP4/screen recording or a supplied timestamped screenshot set. No level description will be guessed.

## Final document structure

The completed production bible will contain:

1. Chapter story summary
2. Level count and story arcs
3. Level-by-level puzzle matrix
4. Character inventory
5. Environment inventory
6. Prop inventory
7. Puzzle-category totals
8. Reusable animation inventory
9. Unique animation requirements
10. UI screen and widget inventory
11. Audio and VFX requirements
12. Code-module mapping
13. Art-production estimates
14. Implementation order and dependencies
15. QA acceptance criteria

## Per-level analysis fields

| Field | Purpose |
| --- | --- |
| Level ID | Stable production identifier |
| Video timestamp | Source-verification location |
| Story beat | What changes in the narrative |
| Player goal | One-sentence objective |
| Puzzle category | Selection, manipulation, gesture, logic, timing or resource |
| Puzzle type | Exact reusable mechanic |
| Input | Tap, drag, swipe, hold or trace |
| Correct solution | Required player action |
| Failure cases | Wrong actions and responses |
| Characters | Visible and interactive cast |
| Environment | Reusable scene/background |
| Props | Interactive and decorative objects |
| Intro animation | Setup motion |
| Success animation | Outcome motion |
| Failure animation | Wrong-answer motion |
| Existing module | Framework code that can be reused |
| New requirement | New art, code, VFX or animation |
| Complexity | Small, medium or hero level |

## Proposed production segmentation

Once the reference is captured, levels should be grouped by reusable production packs rather than only by narrative order:

- Selection pack
- Drag and manipulation pack
- Hidden-object pack
- Sequence and matching pack
- Gesture pack
- Timing pack
- Battle/resource pack
- Hero cinematic pack

This lets the team implement and test one mechanic across several story scenes before moving to the next category.

## Chapter-level reuse targets

- At least 80% of levels use an existing evaluator.
- At least 70% of character motion comes from reusable animation clips.
- Every environment supports at least three puzzle beats where the story permits.
- Unique code is allowed only when a level cannot be expressed through current primitives.
- Unique hero animation is reserved for major story transitions and finales.
