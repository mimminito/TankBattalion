using UnityEditor.AnimatedValues;
using UnityEngine;

namespace UnityTankBattalion
{
    public class TankWeaponHandler : MonoBehaviour
    {
        #region Public Methods

        /// <summary>
        /// Our initial weapon
        /// </summary>
        public GameObject InitialWeapon;

        [Header("Weapon Config")] public Transform BarrelEndTransform;

        #endregion

        #region Private Variables

        /// <summary>
        /// The current active weapon
        /// </summary>
        private BaseTankWeapon mCurrentActiveWeapon;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Initialise
            Init();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Fires the weapon
        /// </summary>
        public void FireWeapon()
        {
            // Check if we have a weapon
            if (!mCurrentActiveWeapon)
            {
                return;
            }

            // Fire the weapon
            mCurrentActiveWeapon.FireWeapon();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialise
        /// </summary>
        private void Init()
        {
            // Check to see if we have an initial weapon
            if (InitialWeapon != null)
            {
                mCurrentActiveWeapon = Pooling.GetFromPool(InitialWeapon, BarrelEndTransform.position, Quaternion.identity).GetComponent<BaseTankWeapon>();
                mCurrentActiveWeapon.transform.SetParent(transform);
                mCurrentActiveWeapon.transform.localScale = Vector3.one;
            }
        }

        #endregion
    }
}