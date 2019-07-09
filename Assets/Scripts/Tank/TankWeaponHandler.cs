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

        public void SwapWeapon(GameObject newWeapon)
        {
            // Check the new weapon is valid
            if (newWeapon == null)
            {
                Debug.Log("Cannot swap to a NULL weapon");
                return;
            }

            // If we have a weapon already, destroy it
            if (mCurrentActiveWeapon != null)
            {
                Pooling.SendToPool(mCurrentActiveWeapon.gameObject);
                mCurrentActiveWeapon = null;
            }

            // Assign the new weapon
            AssignWeapon(newWeapon);
        }

        /// <summary>
        /// Restores the initial weapon
        /// </summary>
        public void RestoreInitialWeapon()
        {
            // Check the initial weapon was valid
            if (InitialWeapon == null)
            {
                return;
            }

            // Swap to our initial weapon
            SwapWeapon(InitialWeapon);
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
                AssignWeapon(InitialWeapon);
            }
        }

        /// <summary>
        /// Assigns a new weapon to use
        /// </summary>
        /// <param name="newWeapon"></param>
        private void AssignWeapon(GameObject newWeapon)
        {
            mCurrentActiveWeapon = Pooling.GetFromPool(newWeapon, BarrelEndTransform.position, transform.rotation).GetComponent<BaseTankWeapon>();
            mCurrentActiveWeapon.transform.SetParent(transform);
            mCurrentActiveWeapon.transform.localScale = Vector3.one;
        }

        #endregion
    }
}