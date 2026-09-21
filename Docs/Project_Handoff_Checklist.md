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

## First repository milestone

1. Confirm Unity editor version and packages.
2. Add project folder and assembly structure.
3. Install the runtime puzzle framework.
4. Add the UI navigation framework.
5. Add automated evaluator tests.
6. Build one greybox Tap Select level.
7. Build one greybox Drag & Drop level.
8. Verify Android portrait build.

