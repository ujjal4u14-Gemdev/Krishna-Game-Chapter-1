# Reusable Puzzle Catalog

The complete framework recognizes 18 puzzle templates. The first eight are sufficient for the initial chapter; the others should be added only after the vertical slice is stable.

## MVP puzzle templates

| # | Template | Player action | Reusable scene examples |
| ---: | --- | --- | --- |
| 1 | Tap Select | Tap the correct target | Correct pot, correct path, safe animal, hidden switch |
| 2 | Multi Select | Find several targets | Feathers, butter pieces, footprints, friends |
| 3 | Drag & Drop | Drag source to destination | Give flute, place bridge stone, feed animal |
| 4 | Sequence Order | Tap items in order | Musical notes, stepping stones, ritual sequence |
| 5 | Swipe Direction | Swipe correctly | Dodge branch, move cloud, redirect projectile |
| 6 | Hidden Object | Reveal and select | Krishna behind curtain, object in bushes, hidden key |
| 7 | Match Pairs | Pair related objects | Shadow matching, tool to owner, animal to food |
| 8 | Route Choice | Pick or build a path | Escape maze, cross river, guide cows home |

## Expansion templates

| # | Template | Player action | Reusable scene examples |
| ---: | --- | --- | --- |
| 9 | Combine Items | Combine two or more props | Rope plus hook, stick plus cloth, ingredients |
| 10 | State Toggle | Change entity states | Open/close gates, turn lamps on, rotate tiles |
| 11 | Timing Tap | Tap inside a timing window | Block attack, jump obstacle, catch falling pot |
| 12 | Hold Charge | Hold and release | Lift mountain, charge magic, pull rope |
| 13 | Trace Path | Trace a route or symbol | Draw protection mark, guide flute melody, follow trail |
| 14 | Count / Compare | Choose based on amount | More fruit, fewer footprints, equal groups |
| 15 | Memory Recall | Remember positions or order | Cups, character positions, melody sequence |
| 16 | Physics Balance | Balance weight or movement | Seesaw, boat load, stacked pots |
| 17 | Resource Battle | Spend limited energy/actions | Shield versus attack, health/energy encounter |
| 18 | Dialogue Choice | Choose the logical response | Riddle, moral choice, character conversation |

## Reuse rule

Every puzzle template needs:

- A neutral mechanic name unrelated to the story.
- A serializable configuration.
- A pure evaluator that accepts `PuzzleInputSignal`.
- A reusable hint behavior.
- Success and failure events.
- Reset support.
- At least three visually different test scenes proving reuse.

## MVP implementation order

1. Tap Select
2. Drag & Drop
3. Multi Select
4. Sequence Order
5. Swipe Direction
6. Hidden Object
7. Match Pairs
8. Route Choice

Timing, tracing, physics and battle mechanics should wait until the core content pipeline is proven.

