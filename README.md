# Extendable-Tile
control color, sprite, transform, with optional modules
## Demo
![alt text](https://github.com/mitay-walle/Extendable-Tile/blob/master/ExtendableTile/Documentation/demo_preview.png?raw=true)

## Contents
- ExtendableTile - CustomTile, that aggregates TileExtensions
- TileExtensionSO - ScriptableObject, containing TileExtension, can be referenced from ScriptableObjectEx
- No Custom Inspectors / PropertyDrawers
- if your project contains [Odin Inspector](https://odininspector.com/) - ExpandableTile will use it, if not:
- TypeToLabelAttribute and custom ContextMenu to use
- TileExtension list:
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

## Problem?
Existing Custom Tiles that I found are rigid realizations with 

## Solution?
#### [SerializeReference](https://docs.unity3d.com/2019.3/Documentation/ScriptReference/SerializeReference.html) used, to have optional modules

## Requriments
- Unity 2019.3 ( [SerializeReference](https://docs.unity3d.com/2019.3/Documentation/ScriptReference/SerializeReference.html) Attribute appeared in this version )
 
## Known issues
- some TileExtensions need Enter / Exit PlayMode to Refresh
- at this moment position rerandomized after any changes in any listed ExtendableTile.Extensions
- 'Use RegisterCompleteObjectUndo Error' - not braking anything

## Planned
- RefreshTile() implementations
- seed-based Randomization
- RuleEx (Existing [RuleTile](https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@1.6/manual/RuleTile.html) has rigid Inspector, i've ported it to PropertyDrawer for RuleEx, but it's not ready-to-use)
