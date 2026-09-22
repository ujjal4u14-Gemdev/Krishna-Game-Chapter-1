# Bal Ganesha — Levels 1–5 production art prompts

These prompts support the playable Unity slice in `FirstFiveLevelsBuilder.cs`. The supplied `Lvl 1-25.mp4` was reviewed for the first five interactions only. It is a mechanical reference, not an art reference: do not reproduce its characters, interface, backgrounds, palette or animation frames. The user-supplied `Ganesha.png` is the canonical character reference.

## Locks shared by every prompt

- Original premium 2D children's storybook art for a 1170 × 2532 portrait game; warm Indian palace architecture; soft, readable silhouettes; saffron, terracotta, muted turquoise and marigold accents; upper-left morning light.
- Preserve the supplied Ganesha's peach-pink skin, **two arms**, curled trunk, two short tusks, red forehead mark, tall ornate gold crown with central red jewel, red-gem gold jewelry, sacred thread, and orange dhoti with yellow trim. No peacock feather, blue skin, third or fourth arm, or alternate costume.
- Background art contains no characters, targets, rewards, removable covers, colliders, UI or text. Every removed object needs complete repaint beneath it.
- Sprite prompts produce one isolated object on a truly transparent background with safe padding. Background prompts bleed to all four edges. No text, UI, watermark, fake screenshot, photorealism, weapons, horror or modern objects.
- Master composition is 2340 × 5064; Unity reference is 1170 × 2532. Keep required interactions within X 98–1072 and Y 330–2228 at reference size. Import sprite PNGs with sRGB and alpha on, mipmaps off; backgrounds require a 4096-pixel max texture size or higher. Keep layered PSD/KRA masters for final production.
- The prompt's composition image is a review aid. Export the listed objects as separate PNGs named exactly after the `SpriteArtSlot` IDs below. Do not use a flattened scene image as gameplay art.

### Canonical character prompt

**Reference image:** `Assets/_Project/Art/Characters/CHR_GANESHA_CHILD/Source/Ganesha_Canonical_Reference.png`

> Use case: identity-preserve. Asset type: transparent 2D game sprite `CHR_Ganesha_Crawl`. Image 1 is the exact canonical Ganesha reference. Repose the same child Ganesha into a low right-facing three-quarter crawl, one hand reaching and one supporting on the floor. Preserve face, eyes, ears, curled trunk, short tusks, red forehead mark, tall ornate gold crown with central red jewel, gold ornaments with red gems, sacred thread, peach-pink skin, orange dhoti and yellow trim. Exactly two arms and two legs. Friendly curious expression, clear mobile-scale silhouette, warm upper-left light, entire body visible, generous transparent padding, bottom-center floor pivot. Original polished 2D storybook rendering. No background, floor, cast shadow, text, UI, watermark, blue skin, peacock feather or extra limbs.

The first transparent crawl candidate is stored at `Assets/_Project/Art/Characters/CHR_GANESHA_CHILD/Export/CHR_Ganesha_Crawl.png`. It is a single flat sprite for the playable slice; final animation still needs a reviewed layered turnaround and cutout rig. The Unity scenes share this one sprite and will accept a replacement through the same slot ID.

### Reusable palace kit prompts

| Unity slot | Prompt | Export |
|---|---|---|
| `BG_Far` | Distant Indian-inspired palace architecture and soft sunlit sky, broad low-contrast masses, depth only, no gameplay objects, no text, seamless portrait bleed. | Opaque full-frame PNG |
| `BG_Mid_Palace` | Warm exterior palace wall for Levels 1–2: simple plaster, distant windows and a few clearly decorative subdued niches; empty left crawl lane and upper-right hanging-object area. No jar, rope, spill or character. | Transparent or full-frame PNG |
| `BG_Mid_Storeroom` | Warm indoor storeroom wall for Levels 3–5: broad quiet plaster, sparse carved trim and believable room depth; uncluttered middle-right puzzle space. No cupboard, beam, pots, character or removable objects. | Transparent or full-frame PNG |
| `BG_Floor` | Reusable warm terracotta and sandstone floor plane with subtle large tiles, clean repaint throughout the playable route and landing zones; no spill, shards or reward. | Transparent or full-frame PNG |
| `ENV_Palace_Arch` | A single restrained carved sandstone arch or beam accent, horizontal, softly lit from upper-left, isolated and transparent; keep it behind interactive objects. | Transparent PNG |

The current builder binds `BG_Mid_Palace` on Levels 1–2 and `BG_Mid_Storeroom` on Levels 3–5. These are kit modules, so the five levels share lighting and scale without reusing one flattened full-screen image.

## Level 1 — The First Modak Jar

**Video-verified action:** erase one vertical rope. A suspended jar drops and safely breaks, revealing modaks. Ganesha starts at lower-left; rope and jar are upper-right; the landing space is clear. No decoy target.

**Composition review prompt**

> Original portrait 2D Bal Ganesha puzzle composition inside a sunlit palace courtyard entrance. Place the canonical two-armed Ganesha crawling at lower-left, looking toward one large terracotta modak jar suspended at upper-right by one clearly visible vertical rope. Preserve a broad finger-rub lane around the rope and a clear floor landing zone below the jar. Warm sandstone arch, saffron plaster, restrained marigold and turquoise detail; subdued distant decoration. Friendly rounded storybook art, no text or UI. Show the intact setup, then plan complete clean background repaint beneath the rope and jar. Export each gameplay object separately.

| Slot | Transparent asset prompt / state | Pivot |
|---|---|---|
| `CHR_Ganesha_Crawl` | Shared canonical crawl sprite above. | Bottom-center |
| `INT_C01_001_Target` | One long, cleanly readable vertical natural-fiber rope, isolated; no jar or roof beam. | Top-center |
| `PROP_Jar_Hanging` | One intact rounded terracotta festival jar with simple cream/turquoise painted bands, lid and top suspension loop; no long rope. | Top-center loop |
| `PROP_Jar_Broken` | Two or three large harmless rounded jar pieces at rest after falling; no sharp shards. | Bottom-center |
| `PROP_Modaks_Pile` | Small readable pile of golden modaks separate from broken ceramic. | Bottom-center |

The playable Level 1 now uses the complete clean `BG_C01_001_Courtyard` plate at exactly 1170 × 2532, plus four independent transparent sprites listed above and the shared Ganesha crawl sprite. The plate intentionally contains no interactive object, character, food, or break-state art. Paths and generation prompts are recorded in `Level_1_Asset_Manifest.md`. Future scenes can use the modular palace kit instead of a hero plate.

Success animation uses the existing jar entity moving downward, then swaps to the separate broken-jar and modak sprites. Rope dust and extra falling frames are later animation additions, not required `LevelArtSet` slots.

## Level 2 — A Messy Path

**Video-verified action:** erase one continuous floor obstruction made of a pale food spill and blocking clay pieces. Ganesha crosses from lower-left to the large jar on the right. Decorative distant pottery is not interactive.

**Composition review prompt**

> Original portrait palace exterior floor puzzle with canonical two-armed Ganesha crawling from lower-left toward one large terracotta serving jar on the right. Between them, one broad horizontal removable obstruction combines a pale golden butter spill and rounded clay fragments; leave the destination jar visible. Clear route and generous finger-rub area, warm morning light, quiet palace wall and floor, child-friendly hand-painted storybook art. No basket pile, text or UI. Supply an entirely clean floor repaint under the obstruction.

| Slot | Transparent asset prompt / state | Pivot |
|---|---|---|
| `INT_C01_002_Target` | One connected silhouette of pale butter spill plus the blocking rounded clay pieces; isolated on transparent canvas. | Center |
| `PROP_Clay_Shards` | A few subdued **nonblocking** edge fragments; visually distinct from the erasable obstruction. | Bottom-center |
| `PROP_Jar_Large` | One large intact destination serving jar filled with modaks, easy to recognize at phone size. | Bottom-center |
| `VFX_Reward_Glow` | Soft isolated golden reward sparkle with transparent falloff, no jar baked into it. | Center |

## Level 3 — The Cupboard Secret

**Video-verified action:** erase one closed cupboard door. Exactly three bowls of modaks are visible on two shelves after reveal. One floor jar and a column are context only.

**Composition review prompt**

> Original warm palace storeroom puzzle in 1170:2532 portrait. One large wall cupboard stands center-right at child eye level; a single broad wooden door is the only erasable target. Canonical two-armed Ganesha waits lower-left and looks toward it. A substantial column and one floor jar frame the scene without competing for attention. Behind the removable door: a complete wooden frame and two shelves holding exactly three separate bowls of golden modaks. Soft amber interior light and simple carved wood; no second cupboard, text or UI. Provide complete shelf and wall repaint beneath the door.

| Slot | Transparent asset prompt / state | Pivot |
|---|---|---|
| `ENV_Cupboard_Frame` | Open wooden frame and two complete empty shelves, no door or bowls. | Bottom-center |
| `INT_C01_003_Target` | One full closed wooden cupboard door with a simple turquoise knob; no frame. | Hinge side |
| `PROP_Modak_Bowl_1` | One small golden bowl of modaks for top shelf. | Bottom-center |
| `PROP_Modak_Bowl_2` | Matching but not identical bowl for lower-left shelf. | Bottom-center |
| `PROP_Modak_Bowl_3` | Matching but not identical bowl for lower-right shelf. | Bottom-center |
| `VFX_Reward_Glow` | Reuse Level 2 glow. | Center |

## Level 4 — The Balance Trick

**Video-verified action:** erase the smaller wrapped load on the **left** end of a balance beam. The large covered jar on the **right** remains; the beam rotates around its geometric center and presents the reward jar. Broken pottery at the far side is dressing, not a target.

**Composition review prompt**

> Original portrait palace storeroom physics puzzle. A child-friendly wooden beam sits on a central triangular fulcrum. One small purple wrapped weight rests on the left end; one much larger covered terracotta jar rests on the right end. Canonical two-armed Ganesha watches from the lower side in a thoughtful pose. Leave visible rotation clearance and make the small left target easy to rub without touching the right load. Warm amber plaster and softly textured floor, rounded harmless forms, polished hand-painted storybook style. No dangerous machinery, text or UI. Export beam, fulcrum, left weight, heavy covered jar and revealed jar separately.

| Slot | Transparent asset prompt / state | Pivot |
|---|---|---|
| `PROP_Balance_Fulcrum` | One sturdy triangular wooden support. | Top-center |
| `PROP_Balance_Beam` | One straight sturdy wooden beam with clean center point and no load attached. | Exact geometric center |
| `INT_C01_004_Target` | Small purple wrapped weight only; clear silhouette, no beam segment. | Bottom-center |
| `PROP_CoveredJar_Heavy` | Much larger covered terracotta load for right beam end. | Bottom-center |
| `PROP_Jar_Reward` | Uncovered large modak jar for the success state; reuse this visual language in Level 5. | Bottom-center |

## Level 5 — Which Pot Has Modaks?

**Video-verified action:** three covered pots form a triangle: upper-left, upper-right, and lower-center. Erase only the lower-center cover. The upper covers remain. A subtle crumb clue can be used; do not make color alone identify the answer.

**Composition review prompt**

> Original portrait palace storeroom choice-by-erasing puzzle. Exactly three covered terracotta pots form a spacious triangle: one upper-left, one upper-right, and the correct lower-center pot closer to the viewer. Canonical two-armed Ganesha observes from lower-left. Give the three cloths distinct festive hems and colors while keeping all plausible; add one subtle golden crumb near the lower-center pot as a fair clue. Beneath that removable lower-center cloth is one full terracotta modak jar. Warm lamp light, quiet walls, generous touch separation, original polished 2D storybook art. No fourth pot, text, UI or watermark. Supply intact repaint behind every cover.

| Slot | Transparent asset prompt / state | Pivot |
|---|---|---|
| `PROP_CoveredJar_Decoy_A` | Upper-left pot with brown festive cloth; intact and noninteractive. | Bottom-center |
| `PROP_CoveredJar_Decoy_B` | Upper-right pot with purple festive cloth; intact and noninteractive. | Bottom-center |
| `INT_C01_005_Target` | Lower-center removable dark cloth only; no jar body baked into it. | Top-center |
| `PROP_Jar_Reward` | Correct lower-center jar with golden modaks; may reuse the Level 4 reward jar art. | Bottom-center |
| `VFX_Reward_Glow` | Reuse the same soft gold sparkle. | Center |

## Import and approval gate

1. Review five flat-color compositions together against the video-verified target positions and the supplied Ganesha identity.
2. Export the shared environment kit and isolated sprites using the exact slot names above. A single prompt-generated flattened scene is a concept, not a Unity-ready art package.
3. Check alpha, safe padding, pivots, 1170 × 2532 framing and complete repaint beneath each removable target.
4. In Unity run `Tools → Bal Ganesha Game → Build First 5 Playable Levels`, assign new sprites in `ART_C01_00N`, and play Levels 1–5. The supplied crawl sprite is automatically assigned when the art sets are first created.
5. Approve Ganesha's final turnaround with a cultural reviewer before cutting a rig or recording dialogue.
