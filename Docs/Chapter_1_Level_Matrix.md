# Chapter 1 Production Bible — Levels 1–54

## Source and confidence

This document was produced from the supplied 9:45.87 portrait walkthrough. The level label was read directly from gameplay frames and used to create the timestamp boundaries below. “Observed solution” describes what succeeds in the video; it is a production description, not copied dialogue. Character names not shown in the video are intentionally recorded as role names until the licensed script confirms them.

The requested 54 levels cover two in-app story cards:

| Story card | Levels | Story function |
|---|---:|---|
| Collect Makhan | 1–25 | Krishna searches for butter, passes household obstacles, then breaks hanging pots with a projectile. |
| Aghasura's Cave | 26–54 | Krishna and two friends enter the forest, discover Aghasura, survive the serpent encounter, and clear hazards in the cave. |

The video begins showing Level 55 at 09:25, but that level is outside this requested 1–54 scope.

## Mechanic inventory

| Reusable family | Levels | Count | Engine module |
|---|---|---:|---|
| Erase / Reveal | 1–5, 11–14, 26–29 | 13 | `EraseRevealEvaluator` + visual eraser adapter |
| Binary Choice / Route | 6–10, 30–39 | 15 | `BinaryChoiceEvaluator` + two-option UI |
| Aim & Shoot | 15–25, 40–54 | 26 | `AimAndShootEvaluator` + projectile/trajectory adapter |
| **Total** | 1–54 | **54** | Three modules, configured by data |

## Complete level matrix

Animation codes: `I` intro/setup, `S` success, `F` failure/reset. All levels also use shared HUD progress, input lock and transition sequences.

| Lv | Video | Story beat and observed successful action | Family | Principal actors/props | Required animation recipe |
|---:|---:|---|---|---|---|
| 1 | 00:12–00:20 | Krishna reaches the first hanging butter pot; erase the indicated obstruction so the pot drops and opens. | Erase | Krishna, hanging pot, rope | I crawl/look; S pot fall/break + eat; F disappointed |
| 2 | 00:20–00:30 | Clear the broken-pot/foreground obstruction and let Krishna reach the large butter jar. | Erase | Krishna, shards, large jar | I crawl; S stand + collect; F stumble/cry |
| 3 | 00:30–00:59 | Search the cupboard by erasing the correct cabinet cover; wrong reveal produces a fail reaction. | Erase | Krishna, three cabinets, butter | I peek; S cupboard reveal + eat; F wrong reveal/hit reaction |
| 4 | 00:59–01:14 | Remove the correct object in the balanced-pot setup so Krishna can obtain the large jar. | Erase | Krishna, balance beam, covered pots | I think; S lift/carry jar; F falling-pot reaction |
| 5 | 01:14–01:41 | Erase the correct cloth/cover to identify the butter jar. | Erase | Krishna, covered jars | I inspect; S reveal + carry jar; F empty/wrong reveal |
| 6 | 01:41–01:51 | Choose the safe movement clue to follow the flower trail without alerting anyone. | Choice | Krishna, flower trail, two choice cards | I crawl/observe; S crawl onward; F wrong movement |
| 7 | 01:51–02:00 | Choose the carpet route and crawl below the bells without ringing them. | Choice | Krishna, bells, carpet | I crawl; S quiet crossing; F bell ring/startle |
| 8 | 02:00–02:13 | Select the hiding/movement solution that gets Krishna past the adult guardian. | Choice | Krishna, adult guardian, pot/basket | I hide/peek; S pass guardian; F caught reaction |
| 9 | 02:13–02:26 | Pick the doorway/path that safely continues the butter search. | Choice | Krishna, two doors/routes, guardian | I approach fork; S enter correct route; F guardian intercepts |
| 10 | 02:26–02:45 | Use the cow as the correct distraction so Krishna can escape the pursuing guardian. | Choice | Krishna, guardian, cow, tether | I crawl/chase setup; S cow moves/route clears; F caught |
| 11 | 02:45–02:55 | Erase the curtain/cover that conceals the butter pot. | Erase | Krishna, curtains, pot | I inspect; S reveal + eat; F wrong curtain |
| 12 | 02:55–03:03 | Use Krishna's thought clue and erase the correct cupboard door. | Erase | Krishna, three cupboards, clue bubble | I think; S reveal + eat; F empty cupboard |
| 13 | 03:03–03:14 | Remove the foreground concealment to expose and break the butter container. | Erase | Krishna, sacks/vessels, hanging pot | I search; S pot break + eat; F wrong object |
| 14 | 03:14–03:29 | Erase the correct cloth/door obstruction in the storeroom and reveal the target pot. | Erase | Krishna, storeroom props, pot | I think/search; S reveal + eat; F wrong reveal |
| 15 | 03:29–03:35 | First projectile tutorial: aim and hit the single hanging butter pot. | Aim | Krishna, launcher, pot | I aim tutorial; S shot + break + eat; F miss |
| 16 | 03:35–03:43 | Hit the pot in the second trajectory layout; broken-pot debris/mice provide payoff. | Aim | Krishna, pot, debris/mice | I aim; S projectile arc + break; F miss |
| 17 | 03:43–03:49 | Hit the butter pot while avoiding the adjacent bell. | Aim | Krishna, pot, bell | I aim; S pot break; F bell ring/startle |
| 18 | 03:49–03:58 | Thread the shot through a denser bell-and-pot layout. | Aim | Krishna, multiple bells/pots | I aim; S target break; F bell/decoy hit |
| 19 | 03:58–04:10 | Use the safe trajectory to strike the pot between hanging bells. | Aim | Krishna, pot, two bells | I aim; S target break; F bell ring |
| 20 | 04:10–04:15 | Short precision beat: strike the exposed pot. | Aim | Krishna, single pot | I aim; S break + eat; F miss |
| 21 | 04:15–04:20 | Hit the pot without contacting the close bell hazard. | Aim | Krishna, pot, bell | I aim; S break; F bell ring |
| 22 | 04:20–04:26 | Shoot through the two-bell corridor to the butter target. | Aim | Krishna, two bells, pot | I aim; S break; F hazard hit |
| 23 | 04:26–04:31 | Choose the correct target/angle in a vertical pot–bell–pot arrangement. | Aim | Krishna, two pots, center bell | I aim; S correct pot break; F bell/decoy |
| 24 | 04:31–04:39 | Clear the multi-pot layout from the platform without falling or hitting a decoy. | Aim | Krishna, three pots, platform gap | I aim; S target break; F miss/decoy |
| 25 | 04:39–04:47 | Final butter challenge: hit the valid pot in the mixed pot-and-bell layout. | Aim | Krishna, pots, bell | I aim; S break + feast; F bell/miss |
| 26 | 04:47–04:55 | Forest opening: erase the vine trapping Krishna's foot. | Erase | Krishna, root/vine | I struggle; S vine clears + stand; F continued struggle |
| 27 | 04:55–05:04 | Erase the mango stems so fruit drops for Krishna and his friends. | Erase | Krishna, two boys, mangoes | I point/ask; S fruit fall + celebrate; F wrong erase |
| 28 | 05:04–05:15 | Erase the spider web blocking the forest route. | Erase | Krishna, boys, spider/web | I discover web; S web clears/rejoin; F spider scare |
| 29 | 05:15–05:24 | Erase the leafy disguise and reveal the enormous serpent/cave mouth. | Erase | Krishna, Aghasura head, leaves | I approach; S reveal/serpent opens; F scare beat |
| 30 | 05:24–05:35 | Choose fruit rather than the cow interaction; Krishna eats safely. | Choice | Krishna, cow, fruit tree, choice cards | I consider; S fruit shower/eat; F cow reaction |
| 31 | 05:35–05:50 | Choose the correct distraction to move the cow and clear the friends' path. | Choice | Krishna, two boys, cow | I blocked; S cow runs/path opens; F failed distraction |
| 32 | 05:50–06:07 | Select the correct route for the boys at the forest ledge; the path leads toward Aghasura. | Route | Two boys, Aghasura eye/mouth | I route preview; S boys proceed; F unsafe branch |
| 33 | 06:07–06:21 | Krishna is separated; choose the correct direction around the large tree. | Route | Krishna, tree, two route cards | I confused/look; S walk correct way; F loop/backtrack |
| 34 | 06:21–06:30 | Pick the valid branch through the cracked looping forest path. | Route | Krishna, forked path | I run/stop; S correct turn; F dead end |
| 35 | 06:30–06:42 | Choose the passable route around fallen logs and flower patches. | Route | Krishna, logs, path cards | I inspect; S cross route; F blocked route |
| 36 | 06:42–06:53 | Choose the safe response when the Aghasura mouth blocks Krishna's path. | Choice | Krishna, Aghasura, two action cards | I serpent reveal; S enter/advance; F fear/capture |
| 37 | 06:53–07:02 | Inside Aghasura, choose the rescue action for the trapped friend. | Choice | Krishna, trapped boy, mouth interior | I discover friend; S rescue setup; F wrong item/action |
| 38 | 07:02–07:15 | Choose Krishna's expansion/standing action to overpower the serpent and free the children. | Choice | Krishna, boys, Aghasura body | I struggle; S expand/jump/free; F ineffective pose |
| 39 | 07:15–07:30 | Select the correct dodge/stance when the serpent lunges again. | Choice | Krishna, Aghasura head | I lunge anticipation; S dodge/serpent passes; F caught |
| 40 | 07:30–07:34 | Cave projectile tutorial: shoot the first hanging poisonous vine target. | Aim | Krishna, vine, poison VFX | I aim; S target burst; F miss |
| 41 | 07:34–07:41 | Hit the vine/snake growth beside the tree trunk from the platform. | Aim | Krishna, tree, crawling hazard | I aim; S hazard clears; F hazard remains |
| 42 | 07:41–07:47 | Strike the poisonous growth near the stump to open the route. | Aim | Krishna, stump, poison growth | I aim; S poison burst/dissipate; F miss |
| 43 | 07:47–07:53 | Use the clear trajectory to remove the next overhead cave hazard. | Aim | Krishna, tall trunk, vine/snake | I aim; S hazard clears; F miss |
| 44 | 07:53–08:02 | Shoot the suspended poisonous target across the gap. | Aim | Krishna, hanging vine, poison VFX | I aim; S burst/clear; F miss |
| 45 | 08:02–08:09 | Shoot the suspended rock/support so it defeats the ground snake. | Aim | Krishna, rock, stump, snake | I aim/target clue; S rock drop + snake clear; F wrong target |
| 46 | 08:09–08:15 | Repeat the rock-drop idea with a tighter line over the snake. | Aim | Krishna, rock, snake, stump | I aim; S rock drop; F miss |
| 47 | 08:15–08:21 | Remove the hanging vine/snake hazard spanning the next gap. | Aim | Krishna, vine/snake | I aim; S poison burst; F miss |
| 48 | 08:21–08:30 | Hit the coiled hazard above the stump while keeping the shot clear of the platform. | Aim | Krishna, stump, coiled snake | I aim; S snake clears; F blocked shot |
| 49 | 08:30–08:39 | Clear the hanging snake/vine so Krishna can jump to the next stump. | Aim | Krishna, stump, hanging hazard | I aim/jump setup; S clear + jump; F collision |
| 50 | 08:39–08:46 | Shoot the poisonous snake growth beside the large trunk. | Aim | Krishna, trunk, snake, poison VFX | I aim; S poison burst; F miss |
| 51 | 08:46–08:58 | Drop the suspended rock into the vine/snake arrangement to clear the passage. | Aim | Krishna, rock, vine, trunk | I aim; S rock drop/hazard clear; F wrong hit |
| 52 | 08:58–09:04 | Make the open shot against the overhead snake/vine. | Aim | Krishna, hanging snake/vine | I aim; S clear; F miss |
| 53 | 09:04–09:12 | Shoot past the suspended object to eliminate the coiled hazard. | Aim | Krishna, suspended prop, snake | I aim; S hazard clear; F prop/miss |
| 54 | 09:12–09:25 | Final Chapter 1 layout: use the hanging rock and correct trajectory to defeat the last poisonous snake. | Aim | Krishna, rock, stump, snake | I aim; S rock drop + route clear; F miss/wrong target |

## Character and creature production count

The safest production estimate is **five primary story roles plus four reusable creature roles**:

| Type | Role | Appears in | Rig recommendation |
|---|---|---|---|
| Primary | Krishna | All levels | Full cutout rig; 12–16 reusable base clips |
| Primary | Adult guardian/mother | 8–10 | Medium rig; idle, walk, chase, catch, react |
| Primary | Cowherd Boy A | 27–28, 31–32, 37–38 | Shared child skeleton with palette/head swap |
| Primary | Cowherd Boy B | 27–28, 31–32, 37–38 | Variant of the same child rig |
| Primary | Aghasura | 29, 32, 36–39 | Head/mouth/eye/body rig; lunge and mouth states |
| Creature | Cow | 10, 30–31 | Idle, walk/run, react |
| Creature | Mouse group | Projectile success beats | One short run/scatter loop, instanced |
| Creature | Spider | 28 | Small idle/scare loop |
| Creature | Cave snake/vine | 40–54 | One modular hazard rig with scale/pose variants |

Names for the guardian and boys must be confirmed from the owned narrative/script before recording voice or locking localization keys.

## Environment and prop scope

Use **five modular environment kits**, not 54 painted scenes:

1. Village/home: exterior lane, interior wall, cupboards, curtains, jars, doors.
2. Butter projectile wall: reusable wall/platform plus pot, bell and rope sockets.
3. Forest: path, trees, flowers, logs, ledges, mango tree and cow dressing.
4. Aghasura: exterior head/mouth, mouth interior and serpent body plates.
5. Cave: parallax walls, stumps/platforms, hanging sockets, poison VFX, rock and snake variants.

The same pot, bell, rope, rock, stump, vine and choice-card prefabs should be positioned by level data. Avoid baking interaction state into background art.

## Animation plan for a short schedule

Create a compact reusable animation library and assemble level-specific sequences from it:

| Library | Minimum clips/actions |
|---|---|
| Krishna | idle, crawl, walk, run, look, think, point, aim, release, jump/dodge, struggle, celebrate, eat-butter, eat-fruit, fail/cry |
| Guardian | idle, walk, chase, catch, surprised |
| Boys | idle, walk/run, celebrate, trapped/struggle, rescued |
| Aghasura | eye-open, mouth-open, lunge, swallow/interior pulse, defeated reaction |
| Props | pot swing/fall/break, bell swing/ring, cloth reveal, cupboard open, rock fall, poison burst, vine retract |

Each level references `IntroSequence`, `SuccessSequence` and `FailureSequence` assets. Most sequences should be composed from shared clips plus transforms/VFX; only story close-ups need unique animation. This is the key time-saving strategy.

## Production order

1. Vertical slice Levels 1, 6 and 15 (all three mechanics).
2. Finish reusable Krishna, pot/bell, choice-card and common feedback prefabs.
3. Batch Levels 1–14 from data and modular home art.
4. Batch Levels 15–25 from one projectile scene template.
5. Build forest/Aghasura story scenes, Levels 26–39.
6. Batch Levels 40–54 from one cave projectile template.
7. Add save/progression, localization, analytics, accessibility and device QA.

