using UnityEngine;

namespace UnityTankBattalion
{
    public class TankWeapon : MonoBehaviour
    {
        #region Public Methods

        [Header("Projectile")] public GameObject ProjectilePrefab;

        [Header("Weapon Config")] public Transform BarrelEndTransform;
        public float ProjectileSpeed;

        #endregion

        #region Public Methods

        /// <summary>
        /// Fires the weapon
        /// </summary>
        public void FireWeapon()
        {
            GameObject projectile = Pooling.GetFromPool(ProjectilePrefab, BarrelEndTransform.position, BarrelEndTransform.rotation);
            Rigidbody2D rb2d = projectile.GetComponent<Rigidbody2D>();
            rb2d.velocity = ProjectileSpeed * BarrelEndTransform.up;
        }

        #endregion
    }
}