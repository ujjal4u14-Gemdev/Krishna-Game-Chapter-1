# Story, Character and Art Bible

## Chapter premise

On the morning of the Festival of Wisdom, Maa Parvati prepares blessed modaks for the village celebration. A playful gust scatters the serving jars through the palace and garden. Bal Ganesha volunteers to recover them. Guided by patience rather than haste, he uncovers the jars, chooses quiet routes and uses lotus seeds to release baskets that are hanging out of reach.

After the festival preparations, Ganesha's young companions Mitra and Veer follow a trail of glowing petals into the forest. The trail leads to the lair of the **Shadow Naga**, an original storybook creature that feeds on fear and confusion. Ganesha follows, makes wise choices, rescues his friends and clears the cave's shadow vines. The Naga's darkness fades when Ganesha lights the final shrine, and the friends return with a lesson: a calm mind finds a path through every obstacle.

## Narrative boundaries

- Keep the tone playful, warm and non-violent. Enemies dissolve into harmless shadow, retreat or learn; they are not graphically injured.
- Ganesha succeeds through observation, compassion and cleverness. Avoid presenting food appetite as ridicule.
- Do not state that the Shadow Naga, Mitra or Veer comes from scripture. They are original characters for this adaptation.
- Use simple visual storytelling. Each level needs at most one setup line and one success line.
- Record dialogue only after cultural and localization review.

## Arc structure

| Arc | Levels | Narrative function | Gameplay progression | Palette |
|---|---:|---|---|---|
| The Missing Modaks | 1–25 | Introduce Ganesha, Parvati, Mushika and festival goal | Reveal → choice → precision shooting | Saffron, turquoise, warm sandstone, marigold yellow |
| The Shadow Naga Cave | 26–54 | Separate the friends, reveal danger, rescue them and restore light | Reveal → route choice → hazard shooting | Forest green → indigo cave → gold restoration |

## Cast

| ID | Character | Function | Level use | Rig/animation scope |
|---|---|---|---:|---|
| `CHR_GANESHA_CHILD` | Bal Ganesha | Player hero | 1–54 | Full 2D cutout rig; trunk is a separate aim/reach chain |
| `CHR_PARVATI` | Maa Parvati | Loving guardian and festival organizer | 8–10, story cards | Medium rig; idle, walk, notice, surprised, approving |
| `CHR_MUSHIKA` | Mushika | Companion, hint giver and comic reaction | 1–25, selected cave beats | Small reusable rig; idle, scurry, point, celebrate, startle |
| `CHR_MITRA` | Mitra | Cautious young companion | 27–32, 37–38, ending | Shared child base rig with unique head/costume |
| `CHR_VEER` | Veer | Bold young companion | 27–32, 37–38, ending | Same base rig as Mitra; palette and silhouette swap |
| `CHR_SHADOW_NAGA` | Shadow Naga | Original cave guardian/obstacle | 29, 32, 36–39, 54 | Modular eye, jaw, head and body plates; shadow dissolve |
| `CHR_TEMPLE_CALF` | Temple calf | Gentle route obstacle | 10, 30–31 | Idle, eat, walk, trot, react |
| `CHR_SPIDER` | Forest spider | Harmless web reaction | 28 | Two-pose idle/scuttle |

## Character art requirements

### Bal Ganesha

- Readable silhouette at 18–22% of portrait screen height.
- Canonical visual reference: `Assets/_Project/Art/Characters/CHR_GANESHA_CHILD/Source/Ganesha_Canonical_Reference.png`. The supplied image, rather than the reference-game character, controls identity and costume.
- Peach-pink skin, two arms, two legs, curled trunk, two short tusks, red forehead mark, tall ornate gold crown with a central red jewel, red-gem gold ornaments, sacred thread, and orange dhoti with yellow trim must stay consistent in every pose.
- Separate layers for rig production: head, ears, eyes, brows, mouth, trunk segments, torso, left/right upper and lower arms, two hands, legs, crown, ornaments, dhoti and held prop.
- Hand set: relaxed, point, hold modak, aim seed, release, shield, celebrate. Keep exactly two visible arms in the approved game design.
- Expression set: neutral, curious, thinking, happy, surprised, effort, gentle fail.
- Never mirror sacred marks or asymmetrical jewelry accidentally; approve a turnaround derived from the supplied character image before rigging.

### Maa Parvati and companions

- Parvati uses soft authority; failure reactions are concern or playful discovery, never anger or physical punishment.
- Mitra and Veer must share one skeleton and clip library. Their color, hair and costume silhouettes must remain distinct in shadow.
- Mushika must remain readable beside Ganesha without becoming an input target unless the level definition explicitly enables it.

### Shadow Naga

- Design as dark blue-violet mist with leaf and stone motifs, not realistic gore.
- Mouth-interior scenes use magical cave shapes; avoid anatomical detail.
- Damage feedback is a pulse, color desaturation and smoke release. Final state is calm, small and non-threatening.

## Reusable environment kits

| Kit ID | Levels | Required modules | Unique hero art |
|---|---:|---|---|
| `ENV_PALACE_STORE` | 1–14 | Wall A/B, floor, cupboard ×3, curtain, shelf, door, sacks, jars, foreground masks | Level 1 festival establishing shot; Level 10 courtyard exit |
| `ENV_FESTIVAL_GALLERY` | 15–25 | Wall/platform variants, rope sockets, bells, modak baskets, breakable clay jars | Level 25 feast tableau |
| `ENV_FOREST_PATH` | 26–35 | Parallax canopy, trunks, roots, logs, flower path, ledges, fruit tree, calf dressing | Level 29 Naga reveal |
| `ENV_NAGA_THRESHOLD` | 36–39 | Naga head exterior, magical mouth interior, shadow-body plates, rescue platform | Level 38 light-expansion payoff |
| `ENV_SHADOW_CAVE` | 40–54 | Cave plates A/B/C, stumps, gaps, hanging sockets, rocks, shadow vines, shrine | Level 54 restored-cave finale |

All gameplay objects are independent sprites/prefabs. Never paint a target, collision blocker, breakable jar, bell, rock, route marker or removable cover into the background.

## Prop replacement map

| Reference function | Bal Ganesha skin | Gameplay behavior |
|---|---|---|
| Butter pot | Modak jar or woven festival basket | Target/reward; same bounds and socket |
| Butter | Modaks and golden blessing particles | Success payoff |
| Launcher/ball | Ganesha's trunk + glowing lotus seed | Same drag-to-aim trajectory |
| Guardian | Maa Parvati | Same patrol/catch presentation, softened tone |
| Cow | Temple calf | Same blocker/distraction footprint |
| Aghasura | Shadow Naga | Same mouth/head/body scene geometry |
| Poisonous snake/vine | Shadow vine or Naga wisp | Same projectile target collider |
| Poison burst | Violet mist becoming gold fireflies | Same VFX timing |

## Animation library

| Owner | Reusable clips/actions |
|---|---|
| Ganesha locomotion | idle, crawl, walk, run, stop, look-left/right, jump, land |
| Ganesha acting | think, point, peek, hide, struggle, startle, gentle-fail, celebrate, eat-modak |
| Ganesha aim | aim-start, trunk-aim additive pose, release-seed, recoil, follow-through |
| Ganesha magic | wisdom-glow start/loop/end, shield, expansion silhouette |
| Parvati | idle, walk, notice, approach, surprised, smile/approve |
| Friends | idle, walk, run, point, trapped-loop, rescued, celebrate |
| Mushika | idle, scurry, sniff, point, hide, startle, celebrate |
| Naga | eye-open, mouth-open, mist-breathe, lunge, recoil, calm, dissolve |
| Props | cover-erase, curtain-open, cupboard-open, jar-swing/fall/break, bell-ring, rock-fall, vine-dissolve |

Target **75% or more reuse** from this library. A level may receive unique animation only if it is an arc opener, reveal, rescue, transformation or finale.

## Art delivery specification

- Master canvas: 2340 × 5064; Unity reference: 1170 × 2532 portrait.
- Keep required interactions inside the central 900 × 1540 reference-safe area. Backgrounds must bleed to the full canvas.
- Deliver layered `.psd` source through Git LFS and exported lossless PNG sprites. Do not flatten interactive layers.
- Background depth: far, middle, gameplay plane, near foreground. Provide repaint beneath every erased or moved object.
- Transparent sprite padding: 16 px at reference resolution; no clipped glows or motion limbs.
- Pivots: feet for characters, hinge for doors, rope connection for hanging props, center of mass for projectiles.
- Use one pixels-per-unit decision per asset class and lock it before final production.
- Sprite atlases are organized by environment kit and lifecycle, not by artist or level number.

## Audio direction

| Bus | Content | Reuse plan |
|---|---|---|
| Music | Festival, forest mystery, cave tension, restoration | Four looping themes with clean loop points |
| Ambience | Palace, courtyard, forest, cave, restored cave | Five loops; one per environment kit |
| UI | Tap, card select, correct, soft fail, hint, transition | Shared globally |
| Puzzle | Erase, seed launch, target hit, bell, jar break, rock fall, vine dissolve | Shared event IDs; randomized variants |
| Character | Efforts, laughs, surprise, short reactions | Per character, language-neutral where possible |
| Voice | Setup and success lines | Localized by key; optional download group later |

Music, ambience, SFX, voice and UI must route to separate mixer groups so the settings screen can control them independently.

## UI skin

The existing UI framework remains unchanged. Reskin with carved sandalwood panels, rounded lotus corners and high-contrast cream text. The choice cards must show large, unambiguous action silhouettes. Never depend on red/green alone for correct and wrong feedback; combine color with shape, motion and sound.
