using UnityEngine;

namespace UnityTankBattalion
{
    public abstract class BaseTankWeapon : MonoBehaviour
    {
        
        #region Public Methods

        /// <summary>
        /// Fires the weapon
        /// </summary>
        public abstract void FireWeapon();

        #endregion
    }
}