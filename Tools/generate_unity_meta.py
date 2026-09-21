#!/usr/bin/env python3
"""Create stable Unity .meta files for repository assets that do not have one."""

from hashlib import sha256
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]
ASSETS = ROOT / "Assets"


def guid(path: Path) -> str:
    relative = path.relative_to(ROOT).as_posix()
    return sha256(relative.encode("utf-8")).hexdigest()[:32]


def folder_meta(path: Path) -> str:
    return f"""fileFormatVersion: 2
guid: {guid(path)}
folderAsset: yes
DefaultImporter:
  externalObjects: {{}}
  userData:
  assetBundleName:
  assetBundleVariant:
"""


def file_meta(path: Path) -> str:
    if path.suffix == ".cs":
        importer = """MonoImporter:
  externalObjects: {}
  serializedVersion: 2
  defaultReferences: []
  executionOrder: 0
  icon: {instanceID: 0}
  userData:
  assetBundleName:
  assetBundleVariant:
"""
    elif path.suffix == ".asmdef":
        importer = """AssemblyDefinitionImporter:
  externalObjects: {}
  userData:
  assetBundleName:
  assetBundleVariant:
"""
    elif path.suffix in {".json", ".csv"}:
        importer = """TextScriptImporter:
  externalObjects: {}
  userData:
  assetBundleName:
  assetBundleVariant:
"""
    else:
        importer = """DefaultImporter:
  externalObjects: {}
  userData:
  assetBundleName:
  assetBundleVariant:
"""
    return f"fileFormatVersion: 2\nguid: {guid(path)}\n{importer}"


def main() -> None:
    paths = sorted((path for path in ASSETS.rglob("*") if path.suffix != ".meta"), key=str)
    created = 0
    for path in paths:
        meta = path.with_name(path.name + ".meta")
        if meta.exists():
            continue
        meta.write_text(folder_meta(path) if path.is_dir() else file_meta(path), encoding="utf-8")
        created += 1
    print(f"Created {created} Unity meta files.")


if __name__ == "__main__":
    main()
