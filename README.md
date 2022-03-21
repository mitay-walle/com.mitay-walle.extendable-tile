# Extendable Tile
control color, sprite, transform, with optional modules - TileExtensions

# Like it? Buy me a candy
If you like my work, you can support me on [Patreon](https://www.patreon.com/mitaywalle)

## Navigation
- [Problems](https://github.com/mitay-walle/Extendable-Tile#problems)
- [Solution](https://github.com/mitay-walle/Extendable-Tile#solution)
- [Extendable Tile](https://github.com/mitay-walle/Extendable-Tile#extendable-tile)
- [Demo](https://github.com/mitay-walle/Extendable-Tile#demo)
- [Contents](https://github.com/mitay-walle/Extendable-Tile#contents)
- [Script types](https://github.com/mitay-walle/Extendable-Tile#script-types)
- [TileExtension list](https://github.com/mitay-walle/Extendable-Tile#tileextension-list)
- [How To](https://github.com/mitay-walle/Extendable-Tile#how-to)
- [Requriments](https://github.com/mitay-walle/Extendable-Tile#requriments)
- [Known issues](https://github.com/mitay-walle/Extendable-Tile#known-issues)
- [Planned](https://github.com/mitay-walle/Extendable-Tile#known-issues)

# Problems
[Existing tilemap tiles](https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@1.5/manual/Tiles.html) are rigid realizations with:
- lack of basic features ( color, transform, sprite manipulations ) which are allowed by TileData / AnimatedTileData
- feature set of any Tile is fixed, no customization / optional modules, that have to add repeatatively byself
- lack of Inspector Undo
- lack of Copy / Paste
- Rigid custom Inspectors, disallowing use [Odin](https://odininspector.com/), or built-in Range / Header / Space Attributes for Inspector customization

## Solution
- [SerializeReference](https://docs.unity3d.com/2019.3/Documentation/ScriptReference/SerializeReference.html) to have optional modules
- no use custom Inspector / PropertyDrawers , to allow Odin, save built-in Undo, Copy / Paste, etc

## Demo
![alt text](https://github.com/mitay-walle/Extendable-Tile/blob/master/ExtendableTile/Documentation/demo_preview.png?raw=true)

## Contents
1. Demo Scene, tiles, extensions
2. if your project contains [Odin Inspector](https://odininspector.com/) - ExpandableTile will use it, if not:
###### Script types:
- ExtendableTile - CustomTile, that aggregates TileExtensions
- TileExtensionSO - ScriptableObject, containing TileExtension, can be referenced from ScriptableObjectEx
- TypeToLabelAttribute and custom ContextMenu to use

###### TileExtension list:
1. AnimateSpriteEx - analogue to [Animated Tile](https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@1.6/manual/AnimatedTile.html)
2. WeightRandomSpriteEx - analogue to [Weight Random Tile](https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@1.5/manual/WeightedRandomTile.html)
3. PipelineTileEx - analogue to [Pipeline Tile](https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@1.5/manual/PipelineTile.html)
4. ColorOutlineEx - evaluate Gradeint based on same-tile-neibhours-count, smooth sides
5. PositionMapEx - Remap tile position with split random MinMaxCurves for X and Y
6. RandomColorEx - Multiply tile color to MinMaxGradient.Evalute(), has Perlin module
7. RotateMapEx - Remap tile rotation with random MinMaxCurve for Z
8. ScaleMapEx - Remap tile localScale with random MinMaxCurve for XYZ

## How To
###### Create Tile or TileExtensionSO?
![alt text](https://github.com/mitay-walle/Extendable-Tile/blob/master/ExtendableTile/Documentation/Instruction_createTile_ProjectContextMenu.png?raw=true)
###### Change type of already created extension in Collection or in TileExtensionSO ?
![alt text](https://github.com/mitay-walle/Extendable-Tile/blob/master/ExtendableTile/Documentation/Instruction_setType_ContextMenu.png?raw=true)

## Requriments
- Unity 2019.3 ( [SerializeReference](https://docs.unity3d.com/2019.3/Documentation/ScriptReference/SerializeReference.html) Attribute appeared in this version )
 
## Known issues
- some TileExtensions need Enter / Exit PlayMode to Refresh
- at this moment position rerandomized after any changes in any listed ExtendableTile.Extensions
- Error log 'Use RegisterCompleteObjectUndo' - not braking anything

## Planned
- RefreshTile() implementations
- seed-based Randomization
- SiblingRuleEx (Existing [Rule Tile](https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@1.6/manual/RuleTile.html) has rigid Inspector, i've ported it to PropertyDrawer for RuleEx, but it's not ready-to-use)
- TerrainEx analogue to [Terrain Tile](https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@1.5/manual/TerrainTile.html)
