# Unity Project Handoff Checklist

## Preferred route: GitHub

Provide the exact repository URL or `owner/repository` name. The repository should contain:

- `Assets/`
- `Packages/`
- `ProjectSettings/`
- `.gitignore`
- `.gitattributes` when Git LFS is used

Do not commit generated Unity folders:

- `Library/`
- `Temp/`
- `Logs/`
- `Obj/`
- Local builds
- IDE caches

The Unity version will be read from `ProjectSettings/ProjectVersion.txt`; it does not need to be typed manually if the project already exists.

## Alternative route: project ZIP

Upload a ZIP containing `Assets`, `Packages` and `ProjectSettings`. Exclude the generated folders listed above. The project can be organized locally first and moved to GitHub afterward.

## Required reference material

- The gameplay walkthrough as an MP4 or screen recording for exact per-level analysis
- Any current Unity project/repository
- Any existing character, environment and UI source files
- Figma link or UI screenshots if a visual style already exists

## Decisions that can wait

- Final Spine versus Unity Animator choice
- Ads provider
- Analytics provider
- Store/IAP SDK
- Final audio provider

Adapters keep these outside the puzzle framework.

## Completed framework milestone

1. Unity editor version and package manifest are locked.
2. Runtime, UI, test and editor assemblies are separated.
3. Chapter 1 evaluators and erase/aim scene controllers are implemented.
4. UI navigation, safe-area and gameplay binding foundations are included.
5. Evaluator tests and project validation tooling are included.
6. Progress persistence and scene loading are included.

## Next content milestone

1. Run `Tools > Bal Ganesha Game > Setup Project` after Unity imports the repository.
2. Greybox Levels 1, 6 and 15 using `Docs/Vertical_Slice_Setup.md`.
3. Add licensed/original character, environment, UI and audio assets.
4. Validate on the target Android device, including safe area and memory use.
5. Use the three approved scenes as templates for the remaining chapter content.
