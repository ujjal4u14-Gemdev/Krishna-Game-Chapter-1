# Bal Ganesha — Levels 1–5 Art Prompt Pack

This pack is the production brief for the first playable slice. The walkthrough video was used only to verify puzzle topology and object placement. All characters, costumes, environments, props, color design and animation must be original.

## Global art direction

### Master style prompt

> Original premium 2D mobile game illustration for children ages 5–10, joyful Bal Ganesha adventure, warm Indian palace architecture, hand-painted storybook shapes, clean readable silhouettes, soft rounded forms, rich saffron, turquoise, coral and marigold palette, gentle volumetric sunlight, subtle textile and painted-plaster texture, expressive friendly faces, culturally respectful jewelry and clothing, polished casual-game finish, portrait 9:16 composition, large uncluttered gameplay area, layered parallax-ready environment, no text, no UI, no watermark, no photorealism.

### Negative prompt

> copyrighted game assets, direct screenshot recreation, Krishna character, realistic child anatomy, frightening imagery, sharp weapons, dark horror, busy pattern behind interactive objects, tiny target, hidden collider, text, logo, watermark, frame, UI buttons, fake mobile screenshot, blur, low resolution, extra limbs, malformed hands, modern objects.

### Technical target

| Item | Requirement |
|---|---|
| Canvas | 1080 × 1920 portrait working composition |
| Safe gameplay area | X 90–990 px; Y 250–1690 px |
| Camera | Orthographic-looking 2D, eye level approximately 35% above floor |
| Character scale | Bal Ganesha occupies 18–24% of canvas height |
| Interactive silhouette | Minimum 140 px on its shortest important axis |
| Masters | Layered PSD/KRA; sRGB; transparent padding retained |
| Unity exports | PNG-24/32, tight but consistent canvas, no baked shadow unless requested |
| Required groups | `BG_Far`, `BG_Mid`, `Gameplay`, `FG_Near`, `Interactive_*`, `Repaint` |

Do not generate the UI inside scene art. Do not flatten removable objects into the background. Each prompt below produces a concept/composition; the artist must rebuild approved output into the named production layers.

## Shared asset prompts

### Bal Ganesha crawl pose

**Slot:** `CHR_Ganesha_Crawl`

> Full-body transparent-background character sprite of young Bal Ganesha crawling with playful curiosity, facing right in three-quarter side view, childlike proportions, soft coral-pink skin, large friendly elephant ears, small curved trunk, warm golden dhoti, modest gold ornaments, tiny crown with turquoise accent, joyful intelligent eyes, one hand reaching forward, readable silhouette at mobile size, polished hand-painted 2D storybook game art, consistent top-left light, no background, no text, no shadow cut off, no extra limbs.

Required variants: neutral crawl, curious look-up, delighted success, reaching toward modaks. Keep the body registration point and foot/floor line identical.

### Palace modular environment kit

**Slots:** `BG_Far`, `BG_Mid_Palace`, `BG_Floor`, `ENV_Palace_Arch`

> Modular warm palace storeroom environment kit for a portrait 2D children's puzzle game, original Indian-inspired painted plaster, carved sandstone arches, deep wall niches, low terracotta floor, subtle marigold festival details, saffron and muted turquoise palette, soft morning sunlight, broad quiet shapes, empty central gameplay area, separate far wall, mid-wall, floor and foreground modules, no characters, no jars, no text, no UI, seamless edges for reuse across five levels.

## Level 1 — The First Modak Jar

### Gameplay lock

- Start: Bal Ganesha crawls at lower-left and looks toward a hanging jar at upper-right.
- Player action: rub/erase the single vertical rope.
- Success: rope vanishes, jar falls, breaks safely on the floor and reveals modaks.
- Decoys/hazards: none. Decorative background pots must not look interactive.

### Composition prompt

> Portrait 9:16 children's mobile puzzle scene inside a sunlit palace courtyard entrance. Young Bal Ganesha crawls at the lower-left, looking up with curiosity. A large terracotta modak jar hangs in the upper-right from one clearly visible vertical rope. Keep a wide clean finger path around the rope. Decorative pots sit softly in distant wall niches and have lower contrast. The landing space below the jar is empty and readable. Warm saffron plaster, carved arch, marigold accents, turquoise details, original hand-painted storybook style, strong depth separation, no text, no UI, no watermark. Deliver background repaint behind the rope and jar.

### Required exports

| Slot / file stem | State | Pivot |
|---|---|---|
| `BG_C01_001_Palace` | Clean environment with repaint | Center |
| `CHR_Ganesha_Crawl` | Start pose | Bottom-center |
| `INT_C01_001_Target` | Full vertical rope only | Top-center |
| `PROP_Jar_Hanging` | Intact hanging jar | Top-center at rope knot |
| `PROP_Jar_Falling` | Optional motion pose | Center |
| `PROP_Jar_Broken` | Safe broken halves | Bottom-center |
| `PROP_Modaks_Pile` | Reward pile | Bottom-center |
| `VFX_Rope_Dust` | 6–10 frame dust/thread puff | Center |
| `VFX_Reward_Glow` | Loopable sparkle | Center |

## Level 2 — A Messy Path

### Gameplay lock

- Start: Ganesha is on the lower-left; a large serving jar is on the right.
- Player action: rub away the butter spill and scattered clay pieces blocking the floor route.
- Success: the route clears and Ganesha crawls/reaches the large jar.
- Decoys/hazards: distant pots and wall decoration only. This is not a basket-pile puzzle.

### Composition prompt

> Portrait 9:16 palace exterior floor puzzle for a premium children's mobile game. Bal Ganesha crawls from lower-left toward a very large terracotta serving jar on the lower-right. Between them is one continuous readable obstruction made from a pale golden butter spill and several rounded harmless clay fragments. The obstruction forms a broad horizontal finger-rub area but does not hide the destination jar. Warm palace wall and courtyard floor, bright morning light, friendly rounded shapes, uncluttered path, original hand-painted storybook art, no basket pile, no text, no UI. Provide a completely clean floor repaint beneath the spill and fragments.

### Required exports

| Slot / file stem | State | Pivot |
|---|---|---|
| `BG_C01_002_PalaceFloor` | Clean path repaint | Center |
| `CHR_Ganesha_Crawl` | Start pose | Bottom-center |
| `INT_C01_002_Target` | Spill + removable blocking fragments as one transparent surface | Center |
| `PROP_Clay_Shards` | Noninteractive edge dressing, visibly subdued | Bottom-center |
| `PROP_Jar_Large` | Destination jar | Bottom-center |
| `CHR_Ganesha_ReachJar` | Success pose | Bottom-center |
| `VFX_Clear_Dust` | Soft wipe particles | Center |

## Level 3 — The Cupboard Secret

### Gameplay lock

- Start: one closed wall cupboard is centered/right; a floor jar and column provide context.
- Player action: rub away the single cupboard door.
- Success: three bowls of modaks are revealed on shelves.
- Decoys/hazards: none. This level has one cupboard, not three.

### Composition prompt

> Portrait 9:16 warm palace storeroom puzzle. One large closed wooden wall cupboard occupies the middle-right at child eye level, with a simple broad door that is easy to rub. Young Bal Ganesha waits at lower-left and looks at it. A thick palace column and one large floor jar frame the scene without competing for attention. Behind the removable door are two shelves holding exactly three small golden bowls of modaks. Soft amber interior light, carved but simple wood, turquoise knob, original polished 2D storybook game art, no other cupboard doors, no text, no UI. Deliver the open cupboard interior and wall repaint as complete artwork beneath the separate door.

### Required exports

| Slot / file stem | State | Pivot |
|---|---|---|
| `BG_C01_003_Storeroom` | Room, column and floor jar | Center |
| `ENV_Cupboard_Frame` | Frame and open shelf interior | Bottom-center |
| `INT_C01_003_Target` | One closed cupboard door | Hinge side |
| `PROP_Modak_Bowl_1..3` | Three separate reward bowls | Bottom-center |
| `CHR_Ganesha_Curious` | Look/point pose | Bottom-center |
| `VFX_Cupboard_Reveal` | Warm dust + sparkle | Center |

## Level 4 — The Balance Trick

### Gameplay lock

- Start: a wooden balance beam rests on a central fulcrum with two differently weighted covered loads.
- Player action: rub away the smaller/light removable weight on the left.
- Success: the beam rotates and presents the large modak jar.
- Decoys/hazards: the large right covered load remains; silhouettes must clearly differ.

### Composition prompt

> Portrait 9:16 palace storeroom puzzle featuring a large child-friendly wooden balance beam across the center on a sturdy triangular fulcrum. A small clearly removable wrapped weight sits on the left end; a larger covered terracotta jar sits on the right end. Bal Ganesha watches from lower-left in a thinking pose. Leave clear rotation space around the beam. Use simple contrasting silhouettes and colors so the small target is readable without text. Warm amber storeroom, friendly physics-toy feeling, original hand-painted 2D children's game art, no dangerous machinery, no UI, no text. Supply the beam, fulcrum, small weight, covered jar and revealed reward jar as separate layers.

### Required exports

| Slot / file stem | State | Pivot |
|---|---|---|
| `BG_C01_004_Storeroom` | Clean environment | Center |
| `PROP_Balance_Fulcrum` | Static base | Top-center |
| `PROP_Balance_Beam` | Rotating beam | Exact geometric center |
| `INT_C01_004_Target` | Small/light removable weight | Bottom-center |
| `PROP_CoveredJar_Heavy` | Right load | Bottom-center |
| `PROP_Jar_Reward` | Revealed jar state | Bottom-center |
| `CHR_Ganesha_Think` | Start pose | Bottom-center |
| `CHR_Ganesha_Celebrate` | Success pose | Bottom-center |

## Level 5 — Which Pot Has Modaks?

### Gameplay lock

- Start: three covered pots form a triangle: upper-left, upper-right and lower-center.
- Player action: rub away the correct lower-center cover.
- Success: a full modak jar is revealed.
- Decoys/hazards: two upper covered pots remain untouched and must have distinct but equally plausible cloth designs.

### Composition prompt

> Portrait 9:16 warm palace storeroom selection puzzle. Exactly three covered terracotta pots form a clear triangle: one upper-left, one upper-right, and one lower-center closer to the player. Each has a distinct festive cloth color and hem pattern, while the lower-center cloth has one subtle fair-play clue such as a tiny golden crumb near its base. Bal Ganesha observes from lower-left. Keep generous empty space between covers so finger input cannot overlap. Under the lower-center removable cloth is a rich terracotta jar filled with golden modaks. Original polished hand-painted children's storybook game art, warm lamp light, no text, no UI, no watermark. Deliver all three covers and all pot states separately, with repaint behind every removable object.

### Required exports

| Slot / file stem | State | Pivot |
|---|---|---|
| `BG_C01_005_Storeroom` | Clean environment | Center |
| `PROP_CoveredJar_Decoy_A` | Upper-left cover and pot | Bottom-center |
| `PROP_CoveredJar_Decoy_B` | Upper-right cover and pot | Bottom-center |
| `INT_C01_005_Target` | Lower-center removable cloth only | Top-center |
| `PROP_Jar_Reward` | Lower-center jar beneath cloth | Bottom-center |
| `CHR_Ganesha_Inspect` | Start pose | Bottom-center |
| `CHR_Ganesha_Celebrate` | Success pose | Bottom-center |
| `VFX_Reward_Glow` | Sparkle burst/loop | Center |

## Review gate before final rendering

Approve a flat color composition for all five levels together before detailed painting. The review must confirm:

- Target position and count match the gameplay lock.
- A child can identify the interactive area without instructional text.
- Target, character and reward do not overlap the top HUD or bottom feedback area.
- Every removable surface has a complete repaint below it.
- Shared assets keep identical scale, palette, light direction and pivots.
- The five scenes look related but not like duplicated full-screen backgrounds.

