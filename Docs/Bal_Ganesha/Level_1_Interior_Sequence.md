# Level 1 — interior art and playable sequence

The approved single-rope interior concept supersedes the outdoor courtyard composition for playable Level 1. Older courtyard PNGs remain in the repository for comparison and are no longer bound to `ART_C01_001.asset`. The original video informs puzzle mechanics only; its characters and UI are not reused.

Approved composition reference: `Docs/Bal_Ganesha/Concepts/Level_1_Interior_Approved_v2.png`. It is a reference image, not a flattened gameplay sprite.

## Separate assets

| Level art slot | File |
|---|---|
| `BG_Far` | `Assets/_Project/Art/Environments/C01/BG_C01_001_Interior_v2.png` (opaque 1170 × 2532) |
| `CHR_Ganesha_Crawl` | `Assets/_Project/Art/Characters/CHR_GANESHA_CHILD/Export/CHR_Ganesha_Crawl_Interior_v2.png` |
| `CHR_Ganesha_Happy_Modak` | `Assets/_Project/Art/Characters/CHR_GANESHA_CHILD/Export/CHR_Ganesha_Happy_Modak_v2.png` |
| `INT_C01_001_Target` | `Assets/_Project/Art/Props/C01/INT_C01_001_Rope_Interior_v2.png` |
| `PROP_Jar_Hanging` | `Assets/_Project/Art/Props/C01/PROP_Jar_Hanging_Interior_v2.png` |
| `PROP_Jar_Broken` | `Assets/_Project/Art/Props/C01/PROP_Jar_Broken_Interior_v2.png` |
| `PROP_Modaks_Pile` | `Assets/_Project/Art/Props/C01/PROP_Modaks_Pile_Interior_v2.png` |
| `UI_Eraser_Tutorial` | `Assets/_Project/Art/UI/UI_Eraser_Tutorial_v2.png` |

The background is repainted continuously behind the character, rope, hanging pot and reward. The rope, pot, reward, poses, and eraser are individually replaceable through stable art slots; gameplay colliders and sequence entity IDs are independent of PNG filenames.

## Gameplay timeline

1. Load the clean interior; Ganesha looks toward the beam-hung pot. A non-interactive eraser icon loops up and down alongside the single erasable rope.
2. First erase progress hides the tutorial icon. The player rubs the rope until progress reaches the puzzle threshold.
3. The pot falls, disappears at the floor, and is replaced by the broken-pot and modak sprites. Ganesha crawls to the reward.
4. Swap to Ganesha's seated laughing pose holding one modak. Ease the camera to a closer view, hold for 2 seconds, then automatically load Level 2. Level 1 does not show the manual Next panel.

## Built-in ImageGen prompts

- Interior background: remove all gameplay objects from the approved concept and repaint the beam, ochre wall, window, distant jars, trim and tiled floor; keep the room simple and uncluttered.
- Crawl sprite: extract canonical crawling Ganesha from the approved concept onto true transparency, preserving crown, face, two arms, orange dhoti and identity.
- Happy sprite: same canonical Ganesha seated and joyfully laughing, holding exactly one golden modak near his mouth, isolated on transparency.
- Rope: one vertical natural-fiber strand with ceiling-beam wraps above and a small attachment knot below; no beam or jar, true transparency.
- Hanging pot: isolated simple round terracotta pot, cream painted band, visible modaks and a central top handle; no rope.
- Broken pot and modaks: matching harmless rounded ceramic pieces and a separate small pale-gold modak pile, each isolated on transparency.
- Tutorial eraser: simple tilted pale-cyan and blue rectangular eraser with a clear contact corner, isolated on transparency.

Assets were generated with built-in ImageGen using the approved concept, user's canonical Ganesha, and the provided eraser crop as labeled references. The opaque room was mechanically exported to 1170 × 2532; the transparent sprites use tight native canvases and are scaled by `SpriteArtSlot` in Unity.
