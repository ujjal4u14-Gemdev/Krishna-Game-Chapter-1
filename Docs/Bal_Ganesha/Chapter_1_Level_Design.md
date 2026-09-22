# Chapter 1 Level Design — 54 Levels

## How to use this document

This is the artist and level-assembler brief. The puzzle family, successful action and obstacle topology match the verified reference matrix. Narrative dressing, characters, reward props, VFX and audio are new.

### Shared gameplay contract

| Family | Player input | Success | Soft failure | Shared hint |
|---|---|---|---|---|
| Erase/Reveal | Drag a finger repeatedly over the marked cover | Required erase threshold reveals or releases the target | Wrong cover reacts; level resets without losing progress | Correct surface glints and Mushika points |
| Binary Choice/Route | Tap one of two large action cards | Correct card plays the route/action | Wrong card plays a short reversible outcome | Correct card gains a slow gold rim pulse |
| Aim & Shoot | Drag from Ganesha to set angle/power; release to launch a lotus seed | Seed hits the valid target | Miss or hazard hit gives feedback and reloads | Dotted arc appears after repeated misses |

All levels use `Intro → Awaiting Input → Resolving → Outcome → Transition`. Input is locked during animations. A soft fail must return to Awaiting Input in under 2.5 seconds.

## Arc A — The Missing Modaks

### Levels 1–5: learn Erase/Reveal

| Lv / ID | Story and player objective | Gameplay and outcome | Scene/art composition | Animation, VFX and audio |
|---|---|---|---|---|
| 01 `LVL_C01_001` | Ganesha finds the first modak jar caught behind a festival ribbon. Clear the ribbon. | **Erase** the single ribbon/cover. Success drops and opens the jar; wrong dragging only gives dust feedback. | Palace store, low camera; Ganesha left, hanging jar upper-right, ribbon mask in front, Mushika near target. | Intro crawl/look; ribbon threads dissolve; jar falls and opens; gold crumbs, clay knock, happy chime. |
| 02 `LVL_C01_002` | A large serving jar is blocked by empty festival baskets. Clear a path. | **Erase** the marked foreground basket pile. Success lets Ganesha step to the jar; untouched shards remain decorative/noninteractive. | Same kit, alternate wall; basket pile center, large jar right, safe negative space for swipe. | Crawl, stand, lift jar; wrong area gives small shake; basket rustle and reward sting. |
| 03 `LVL_C01_003` | Three cupboards may contain the missing tray. Reveal the one marked by a lotus clue. | **Erase** the correct cupboard cover. Wrong cover reveals harmless utensils and resets. | Three equal cupboard doors across middle; lotus carving only on correct one; Ganesha/Mushika foreground. | Peek/thinking; correct door wipes to modak tray; wrong reveal + surprised blink; wood rub SFX. |
| 04 `LVL_C01_004` | A balanced shelf hides the festival jar. Remove the light cloth without upsetting it. | **Erase** the correct cloth/weight. Wrong object makes jars wobble, then resets. | Balance beam centered, two covered loads, large reward jar; keep silhouettes clearly different. | Think; cloth dissolves, beam settles, Ganesha carries jar; wobble fail, ceramic clinks. |
| 05 `LVL_C01_005` | Several covered jars look identical. Find the one with the marigold seal. | **Erase** the sealed cloth. Wrong cloth reveals an empty jar. | Three covered jars, distinct cloth hems; marigold seal visible at swipe start; warm storeroom light. | Inspect; cloth unwrap, modak sparkle, carry pose; fabric rub and sparkle arpeggio. |

### Levels 6–10: learn Binary Choice

| Lv / ID | Story and player objective | Gameplay and outcome | Scene/art composition | Animation, VFX and audio |
|---|---|---|---|---|
| 06 `LVL_C01_006` | Follow a petal trail quietly to the next store room. | **Choose** quiet crawl, not noisy run. Correct action advances; wrong action scatters petals and resets. | Palace corridor with flower trail; two bottom cards show crawl/run silhouettes. | Observe, crawl success; run-stop fail; soft footsteps vs fast foot taps. |
| 07 `LVL_C01_007` | Cross beneath hanging temple bells without ringing them. | **Choose** carpet route, not the bare floor/jump route. | Bells upper-middle, rolled carpet forming low safe lane; action cards never cover route. | Low crawl; wrong route rings bells and startles Mushika; bell one-shot must be gentle. |
| 08 `LVL_C01_008` | Maa Parvati enters while Ganesha carries a surprise tray. Hide it until the festival reveal. | **Choose** large basket cover, not small pot. Correct hides Ganesha/tray; wrong leaves ears visible. | Parvati right doorway; basket and pot options; playful hide-and-seek framing. | Peek/hide; Parvati passes smiling; wrong choice gives amused notice, not anger. |
| 09 `LVL_C01_009` | Pick the doorway leading to the courtyard store. | **Choose** marigold-marked door, not kitchen return. | Two doors with clear icon clues; Parvati patrol line in background; Ganesha at fork. | Approach, choose, door open; wrong door loops back; wooden latch and route chime. |
| 10 `LVL_C01_010` | A temple calf blocks the courtyard while Parvati approaches. Move it gently. | **Choose** offer grass, not pull its bell. Grass moves calf and clears route; wrong choice makes it sit. | Courtyard exit; calf center, grass bundle and bell choice cards; Parvati far background. | Chase setup; calf eats/trots; wrong sit + Ganesha puzzled; hoof taps and munch. |

### Levels 11–14: advanced Erase/Reveal

| Lv / ID | Story and player objective | Gameplay and outcome | Scene/art composition | Animation, VFX and audio |
|---|---|---|---|---|
| 11 `LVL_C01_011` | A curtain conceals the next modak basket. Find the side with the lotus tassel. | **Erase** correct curtain panel; wrong panel shows shelves. | Two curtain panels, lotus tassel clue, basket silhouette behind correct panel. | Inspect, reveal, eat one modak; fabric sweep and tiny Mushika cheer. |
| 12 `LVL_C01_012` | Ganesha remembers Parvati's sun-symbol clue. Open the matching cupboard. | **Erase** correct door using thought bubble clue. | Three cupboard fronts; sun, moon and leaf marks; thought bubble above Ganesha. | Think loop; correct reveal; wrong cupboard empty with comic echo. |
| 13 `LVL_C01_013` | Festival sacks conceal a hanging jar and its release knot. | **Erase** foreground sack/cover to expose the correct hanging jar. | Layered sacks bottom, rope target upper-center, decoy vessels at sides. | Search; reveal and jar break/open; wrong sack puff; burlap rub and clay pop. |
| 14 `LVL_C01_014` | The final store-room tray is behind a painted screen. | **Erase** the lotus panel, not the peacock panel. | Hero storeroom composition; two ornate screens; reward tray backlit. | Think/search; panel dissolves into particles; success pose transitions to gallery. |

### Levels 15–25: Aim & Shoot with lotus seeds

| Lv / ID | Story and player objective | Gameplay and outcome | Scene/art composition | Animation, VFX and audio |
|---|---|---|---|---|
| 15 `LVL_C01_015` | A basket hangs out of reach. Ganesha learns to flick a lotus seed at its rope clasp. | **Aim** at one large clasp; no hazards. Show drag/release tutorial. | Festival gallery template; Ganesha lower-left, single basket upper-right, wide clear arc. | Trunk aim/release; clasp pops, basket lowers; seed whistle and success bell. |
| 16 `LVL_C01_016` | Release the next jar while Mushika waits below to catch a modak. | **Aim** at exposed clasp in a second arc layout. | Target upper-left; decorative broken clay and two mice/Mushika reactions below. | Seed arc, jar opens, crumbs fall, companions scatter/celebrate; miss puff. |
| 17 `LVL_C01_017` | Free a basket without striking the nearby temple bell. | **Aim** at basket clasp; bell collider is failure. | Basket and bell adjacent with clear gap; high contrast silhouettes. | Target pop; hazard hit swings/rings bell and resets; visual red ripple plus sound. |
| 18 `LVL_C01_018` | Thread a seed through a denser row of bells. | **Aim** through the safe corridor to the valid basket; bells/decoy jar fail. | Multiple hanging objects at different heights; unobstructed dotted hint path available. | Correct jar opens; wrong object sway; layered bell tones kept below voice bus. |
| 19 `LVL_C01_019` | Strike the clasp between two hanging bells. | **Aim** through narrow central gap. | Symmetrical bells, target center-high; uncluttered background to read trajectory. | Precision hit uses brief time-scale emphasis; fail rings left/right bell distinctly. |
| 20 `LVL_C01_020` | Quickly release an exposed basket for the waiting servers. | **Aim** at single target; shorter tutorial-free beat. | Open gallery, target upper-middle, festival helpers as noninteractive silhouettes. | Fast seed, basket lower, approving nod; bright one-second payoff. |
| 21 `LVL_C01_021` | Avoid a bell hanging very close to the clasp. | **Aim** with fine power control; bell hit fails. | Target right, bell just below/left; Ganesha lower-left. | Near-miss sparkle; bell fail; target success with modak shower. |
| 22 `LVL_C01_022` | Send the seed through a two-bell corridor. | **Aim** between two hazard colliders to the basket. | Vertical corridor of bells; target beyond; arc remains visible while dragging. | Bells wobble on fail; clean target snap on success; stereo-free mobile-friendly mix. |
| 23 `LVL_C01_023` | Choose the true modak seal in a vertical jar–bell–jar stack. | **Aim** at the jar with gold seal; center bell/decoy jar fail. | Vertical stack, seal readable without text; target collider matches visible seal. | Correct jar glow/break; decoy gives hollow thunk; bell gives ring. |
| 24 `LVL_C01_024` | Release the correct basket across a platform gap. | **Aim** around decoy jars; landing/fall is presentation only. | Ganesha on left balcony, three hanging jars, gap and festival courtyard depth. | Seed hit; bridge flag unfurls; miss falls into sparkle, never depicts injury. |
| 25 `LVL_C01_025` | Recover the final festival basket from the mixed bell-and-jar display. | **Aim** at valid gold-clasp jar; other jars/bell fail. | Hero gallery tableau; multiple objects form clear final challenge; room for success group shot. | Basket opens into feast table; Parvati, Mushika and Ganesha celebrate; arc-complete music sting. |

## Arc B — The Shadow Naga Cave

### Levels 26–29: forest Erase/Reveal

| Lv / ID | Story and player objective | Gameplay and outcome | Scene/art composition | Animation, VFX and audio |
|---|---|---|---|---|
| 26 `LVL_C01_026` | In the forest, a shadow root catches Ganesha's ankle. Clear it. | **Erase** the binding vine/root. Success frees foot; incomplete erase continues struggle. | Forest path opener; root foreground, Ganesha center, friends ahead in distance. | Struggle, vine turns to fireflies, stand; leaf rub and low shadow hum. |
| 27 `LVL_C01_027` | Mitra and Veer need fruit before continuing. Release the ripe mango cluster. | **Erase** the highlighted stems so fruit drops; wrong leaves shake only. | Mango canopy top, friends below, two clusters with different ripeness colors. | Point; mango fall/catch; group celebrate; leaves and soft fruit drops. |
| 28 `LVL_C01_028` | A harmless spider web blocks the narrow trail. Clear it gently. | **Erase** web strands, not spider. Spider is non-target feedback. | Tree tunnel; web spans central route; small spider at edge, friends beyond. | Discover; web sparkles away, spider scuttles safely; wrong touch startles group. |
| 29 `LVL_C01_029` | The petal trail ends at a leafy hill. Reveal what is behind it. | **Erase** leafy disguise to reveal the giant Shadow Naga cave mouth. | Full-screen reveal composition; leaves/branches form removable mask over eyes/mouth. | Slow approach; eye opens, leaves blow away, mouth opens; bass swell without horror. |

### Levels 30–39: choices, routes and rescue

| Lv / ID | Story and player objective | Gameplay and outcome | Scene/art composition | Animation, VFX and audio |
|---|---|---|---|---|
| 30 `LVL_C01_030` | A nervous calf guards the fruit trail. Choose a peaceful snack. | **Choose** fruit from tree, not touch calf's bell. | Calf left, fruit tree right, two action cards; Naga shadow hinted in distance. | Fruit shower/eat/share; wrong bell makes calf step back; gentle animal audio. |
| 31 `LVL_C01_031` | The calf blocks Mitra and Veer's path. Move it kindly. | **Choose** offer fruit/grass, not shout. Correct distraction opens route. | Friends behind calf, Ganesha at side; route line readable after calf moves. | Calf trots to food; friends pass; wrong action makes calf remain seated. |
| 32 `LVL_C01_032` | Mitra and Veer reach a ledge while Ganesha is delayed. Pick the petal-marked route. | **Choose route** with glowing petals; wrong branch approaches a Naga eye then resets. | Friends foreground; forked ledges, distant Naga eye/mouth, two route cards. | Route preview; friends proceed; unsafe branch shadow pulse and retreat. |
| 33 `LVL_C01_033` | Ganesha finds the fork but the friends are gone. Choose around the ancient tree. | **Choose route** matching scarf/thread clue on correct side. | Large central tree divides two paths; tiny friend-cloth clue on correct branch. | Confused look; correct walk; wrong route loops behind tree. |
| 34 `LVL_C01_034` | Shadow cracks make one looping forest branch unsafe. | **Choose route** with intact stepping stones, not cracked ground. | Top-down-leaning fork, crack decals, flower trail continuation. | Run/stop; safe turn; wrong dead end releases harmless mist. |
| 35 `LVL_C01_035` | Fallen logs block one branch. Find the passable flower path. | **Choose route** around logs. | Log silhouettes and flower patches clearly mark two lanes; cave glow beyond. | Inspect; cross correct lane; wrong lane stops at log and resets. |
| 36 `LVL_C01_036` | The Shadow Naga mouth blocks the trail. Ganesha must follow his friends inside. | **Choose** wisdom-light/shield action, not run away. | Naga head fills upper frame; Ganesha small but confident; two action cards. | Eye/mouth reveal; golden shield, advance; wrong retreat loops to resolve pose. |
| 37 `LVL_C01_037` | Inside the magical cave, Mitra is trapped by a shadow loop. | **Choose** extend trunk/rescue hand, not strike the shadow. | Abstract mouth cave; Mitra right in loop, Ganesha left, Veer deeper background. | Discover; trunk reaches and loosens loop; wrong action strengthens mist briefly. |
| 38 `LVL_C01_038` | The cave walls close around both friends. Fill the chamber with wisdom light. | **Choose** stand-and-glow/expand aura, not crouch/hide. | Naga body plates curve inward; friends center; Ganesha foreground. | Struggle; silhouette/aura expands, walls open, friends freed; gold radial VFX. |
| 39 `LVL_C01_039` | The Naga lunges as the group reaches the exit. Choose the safe stance. | **Choose** shield-and-dodge, not freeze. | Exterior threshold; Naga head from side, clear dodge lane and two pose cards. | Anticipation; dodge/shield; Naga passes and becomes mist; wrong caught silhouette resets. |

### Levels 40–54: clear the Shadow Cave

| Lv / ID | Story and player objective | Gameplay and outcome | Scene/art composition | Animation, VFX and audio |
|---|---|---|---|---|
| 40 `LVL_C01_040` | A hanging shadow vine blocks the cave path. Learn to clear it with a light seed. | **Aim** at one large vine core; no secondary hazard. | Cave tutorial; Ganesha lower-left, luminous core upper-right, wide trajectory space. | Aim/release; core bursts from violet to gold fireflies; new cave hit SFX. |
| 41 `LVL_C01_041` | Clear a crawling shadow growth beside an old trunk. | **Aim** from stump platform at the growth core. | Platform left, tall trunk right, ground hazard beneath; collider matches core. | Seed impact, vine retract; miss leaves slow pulse; cave ambience continues. |
| 42 `LVL_C01_042` | Remove the growth near the next stump to open the route. | **Aim** at exposed poison/shadow bulb. | Stump mid-right, bulb just beyond; readable open arc. | Burst/dissipate; Ganesha steps forward; soft chime confirms path open. |
| 43 `LVL_C01_043` | An overhead vine hangs from a tall root column. | **Aim** at hanging core, avoiding trunk collider. | Tall vertical composition, target upper-middle, trunk edge as geometry. | Vine curls upward on hit; blocked shot sparks and reloads. |
| 44 `LVL_C01_044` | Clear a suspended shadow pod across a wide gap. | **Aim** with more power at pod. | Large gap, hanging pod far-right; parallax cave depth emphasizes distance. | Long seed trail, pod bursts; fireflies form a temporary bridge cue. |
| 45 `LVL_C01_045` | A ground wisp cannot be hit directly. Drop a hanging rock onto its shadow anchor. | **Aim** at rope/support, not wisp. Rock drop clears hazard. | Rock above ground wisp, stump foreground; support is visually highlighted. | Target clue, rope snap, rock lands, wisp dissolves; safe magical impact. |
| 46 `LVL_C01_046` | Repeat the rock-drop solution with a tighter opening. | **Aim** through narrow line at support. | Rock aligned above wisp; trunk/ledge narrows shot without hiding target. | Rock drop and mist puff; miss chip spark; slightly faster reset. |
| 47 `LVL_C01_047` | A long hanging vine spans the next gap. | **Aim** at central vine core. | Vine diagonal across gap, core center, Ganesha on left stump. | Core burst travels along vine; entire hazard retracts; whoosh and sparkle. |
| 48 `LVL_C01_048` | A coiled shadow sits above a stump, partly protected by stone. | **Aim** around platform edge to exposed core. | Stump right, coil on top, blocker below; silhouette must show valid opening. | Coil unravels into fireflies; blocked hit flashes stone dust. |
| 49 `LVL_C01_049` | Clear a hanging hazard so Ganesha can jump to the next stump. | **Aim** at vine; success automatically plays jump. | Two stumps with gap, hazard hanging between them; landing area visible. | Hit, dissolve, jump/land; fail contact is a shield bounce, not injury. |
| 50 `LVL_C01_050` | Remove a shadow growth beside the largest cave trunk. | **Aim** at glowing node next to trunk. | Massive trunk dominates middle; node on far side; frame target with rim light. | Burst, gold light climbs trunk; miss ricochet sparkle. |
| 51 `LVL_C01_051` | Drop a suspended stone into a tangled vine arrangement. | **Aim** at support; stone must strike central knot. | Rock top-center, vine knot below, trunk right; decoy vine is non-solution. | Support breaks, rock drops, multiple vines retract; layered impact/dissolve audio. |
| 52 `LVL_C01_052` | Take an open shot at the final overhead vine corridor. | **Aim** directly at hanging core. | Clean breathing-space composition after dense Level 51; target upper-right. | Quick burst and route advance; music begins shifting from indigo to gold theme. |
| 53 `LVL_C01_053` | Shoot past a suspended stone to reach a coiled shadow anchor. | **Aim** around stone at coil; hitting stone is soft failure. | Suspended stone foreground, coil behind/right, visible safe lane. | Correct coil dissolves; stone hit swings and resets; spatially distinct sounds. |
| 54 `LVL_C01_054` | The final Naga shadow surrounds the cave shrine. Drop the light stone onto its anchor and restore the chamber. | **Aim** at the correct hanging support; wrong target/miss resets. | Hero finale: Ganesha left, shrine center-right, stone above, final coil around base; friends safe in background. | Stone drops, shadow clears, shrine lights, Naga becomes calm mist; group celebration and chapter-complete music. |

## Complexity and unique-art gates

| Tier | Levels | Rule |
|---|---|---|
| Standard | 2–7, 9, 11–13, 15–23, 26–28, 30–35, 37, 40–44, 46–50, 52–53 | Must be assembled entirely from a kit plus level layout/configuration. |
| Enhanced | 8, 10, 14, 24, 31–33, 36, 39, 45, 51 | One unique composition or short animation, but no new puzzle code. |
| Hero | 1, 25, 29, 38, 54 | May receive unique story art, camera work and animation after standard levels prove the pipeline. |

## Per-level acceptance checklist

- The successful player action still matches the same family and obstacle topology as the reference level.
- Correct, wrong and noninteractive objects are visually distinguishable without reading text.
- The artist delivered background repaint beneath every removable object.
- Every interactive item is a separate prefab candidate with a stable entity ID.
- Intro, success and failure/reset fit the animation budget and do not block input invisibly.
- SFX event IDs, dialogue keys and localization keys exist before final integration.
- The level works with music and voice muted, and at phone/tablet safe-area extremes.
