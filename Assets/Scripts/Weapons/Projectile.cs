using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityTankBattalion
{
    public class Projectile : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// Amount of damage this projectile will deal to another entity
        /// </summary>
        [Header("Damage")] public int DamageToDeal = 10;

        /// <summary>
        /// Whether to kill this projectile on collision
        /// </summary>
        [Header("Death")] public bool KillOnCollision = true;

        #endregion

        #region Private Variables

        /// <summary>
        /// Our health component
        /// </summary>
        private Health mHealth;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Initialise
            Init();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // Check to see if the other entity has a health component
            Health otherHealth = other.gameObject.GetComponent<Health>();
            if (otherHealth)
            {
                // Deal damage to the other entity
                otherHealth.Damage(DamageToDeal);
            }

            CheckIfCollidedWithTilemap(other);

            // If we should kill ourselves on collision
            if (KillOnCollision)
            {
                // First make sure we have a health component
                if (mHealth)
                {
                    // Kill ourselves
                    mHealth.Kill();
                }
                else
                {
                    // Check to see if we were spawned using Pooling
                    if (GetComponent<Pooling.PoolMember>() != null)
                    {
                        // If so, send back to the pool
                        Pooling.SendToPool(gameObject);
                    }
                    else
                    {
                        // If not, destroy 
                        Destroy(gameObject);
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialises the component
        /// </summary>
        private void Init()
        {
            // Grab our health component
            mHealth = GetComponent<Health>();
        }

        /// <summary>
        /// Checks to see if we have collided with a tilemap
        /// </summary>
        private void CheckIfCollidedWithTilemap(Collision2D other)
        {
            // Check to see if it has a tilemap component
            Tilemap tilemap = other.gameObject.GetComponent<Tilemap>();
            if (!tilemap)
            {
                return;
            }

            ContactPoint2D contactPoint2D = other.GetContact(0);


            // Grab the tile
            Vector2 hitPos = Vector2.zero;
            hitPos.x = contactPoint2D.point.x - 0.01f * contactPoint2D.normal.x;
            hitPos.y = contactPoint2D.point.y - 0.01f * contactPoint2D.normal.y;

            TileBase tile = tilemap.GetTile(tilemap.WorldToCell(hitPos));
            if (tile is DestructibleTile destructibleTile)
            {
                destructibleTile.Damage(DamageToDeal);
            }
        }

        #endregion
    }
}