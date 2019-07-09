using UnityEngine;

namespace UnityTankBattalion
{
    public class ProjectileWeapon : BaseTankWeapon
    {
        #region Public Variables

        /// <summary>
        /// The projectile prefab
        /// </summary>
        [Header("Projectile")] public GameObject ProjectilePrefab;
        
        /// <summary>
        /// The projectiles speed
        /// </summary>
        public float ProjectileSpeed;

        #endregion

        #region Public Methods

        /// <summary>
        /// Fires the weapon
        /// </summary>
        public override void FireWeapon()
        {
            // Spawn the projectile
            GameObject projectile = Pooling.GetFromPool(ProjectilePrefab, transform.position, transform.rotation);

            // Grab its Rigidbody2D
            Rigidbody2D rb2d = projectile.GetComponent<Rigidbody2D>();

            // Set its speed
            rb2d.velocity = ProjectileSpeed * transform.up;
        }

        #endregion
    }
}