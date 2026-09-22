# Level 1 — generated asset manifest

The Level 1 visuals were generated in built-in ImageGen mode, then imported as independent Unity sprites. The user's `Ganesha.png` supplied the style and character identity reference. The first-five-level video supplied mechanics only; its art was not reproduced.

| Slot | Project asset | Export | Use |
|---|---|---|---|
| `BG_Far` | `Assets/_Project/Art/Environments/C01/BG_C01_001_Courtyard.png` | 1170 × 2532, opaque | Full-bleed clean palace courtyard; no objects baked in |
| `INT_C01_001_Target` | `Assets/_Project/Art/Props/C01/INT_C01_001_Target.png` | Transparent PNG | Tile-masked vertical erase rope |
| `PROP_Jar_Hanging` | `Assets/_Project/Art/Props/C01/PROP_Jar_Hanging.png` | Transparent PNG | Movable intact jar |
| `PROP_Jar_Broken` | `Assets/_Project/Art/Props/C01/PROP_Jar_Broken.png` | Transparent PNG | Success-state ceramic pieces |
| `PROP_Modaks_Pile` | `Assets/_Project/Art/Props/C01/PROP_Modaks_Pile.png` | Transparent PNG | Separate success-state reward |
| `CHR_Ganesha_Crawl` | `Assets/_Project/Art/Characters/CHR_GANESHA_CHILD/Export/CHR_Ganesha_Crawl.png` | Transparent PNG | Canonical Ganesha, shared across the slice |

## Generation prompts used

- Background: Original warm 2D children's storybook palace courtyard, full-bleed 1170:2532 portrait, morning light from upper left, sandstone arch, saffron plaster, subdued turquoise and marigold details, clear lower-left crawl lane, clear upper-right hanging-object area and lower-right landing zone. No Ganesha, jar, rope, reward, UI, or text; paint complete uninterrupted scene behind removable objects.
- Rope: One long straight golden-brown natural-fiber rope, tiny upper loop and frayed lower end, clearly readable phone-size silhouette, isolated true-alpha sprite. No jar or beam.
- Hanging jar: One round terracotta festival modak jar with cream and muted turquoise bands, small lid and single gold suspension loop, isolated true-alpha sprite. No long rope.
- Broken jar: Two or three large rounded terracotta jar pieces with matching cream/turquoise bands, harmless edges, low cluster, isolated true-alpha sprite. No modaks.
- Modak pile: About seven to nine plump golden pleated modaks, low readable group, isolated true-alpha sprite. No jar, plate, or floor.

All prompts used the canonical Ganesha image for stylistic continuity, not as a request to place him in prop or background art. The background was mechanically resampled from its generated aspect-matched source to the exact reference dimensions; the rope's empty transparent margin was trimmed for correct in-scene scale. Sprite pixel sizes do **not** need to equal screen size: the `SpriteArtSlot` reference box controls their world-space display size. For other display aspect ratios, camera framing preserves the reference vertical field while UI anchors scale to the device.

To replace art later, keep each slot ID and update `ART_C01_001.asset`. Puzzle IDs, erase colliders, and success-sequence entities remain independent of art files.
