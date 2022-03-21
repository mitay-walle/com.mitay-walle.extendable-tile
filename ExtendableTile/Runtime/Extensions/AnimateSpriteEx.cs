using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Plugins.Extendable.Runtime.Extensions
{
    [Serializable]
    public class AnimateSpriteEx : TileExtension
    {
        public Sprite[] m_AnimatedSprites;
        public float m_MinSpeed = 1;
        public float m_MaxSpeed = 1;
        public float m_AnimationStartTime;
        public int m_AnimationStartFrame;
        public Tile.ColliderType m_TileColliderType;

        public override void RefreshTile(Vector3Int position, ITilemap tilemap)
        {
            base.RefreshTile(position, tilemap);
        }

        public override TileData GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.transform = Matrix4x4.identity;

            if (m_AnimatedSprites != null && m_AnimatedSprites.Length > 0)
            {
                tileData.sprite = m_AnimatedSprites[m_AnimatedSprites.Length - 1];
                tileData.colliderType = m_TileColliderType;
            }

            return tileData;
        }
        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            if (m_AnimatedSprites.Length > 0)
            {
                tileAnimationData.animatedSprites = m_AnimatedSprites;
                tileAnimationData.animationSpeed = Random.Range(m_MinSpeed, m_MaxSpeed);
                tileAnimationData.animationStartTime = m_AnimationStartTime;

                if (0 < m_AnimationStartFrame && m_AnimationStartFrame <= m_AnimatedSprites.Length)
                {
                    var tilemapComponent = tilemap.GetComponent<UnityEngine.Tilemaps.Tilemap>();

                    if (tilemapComponent != null && tilemapComponent.animationFrameRate > 0)
                        tileAnimationData.animationStartTime = (m_AnimationStartFrame - 1) / tilemapComponent.animationFrameRate;
                }

                return true;
            }

            return false;
        }
    }
}
