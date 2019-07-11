using UnityEngine;

namespace UnityTankBattalion
{
    public class TankWeaponHandler : MonoBehaviour, IPoolable
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
        /// Our health component
        /// </summary>
        private Health mHealth;

        /// <summary>
        /// Whether we can fire
        /// </summary>
        private bool mCanFire;

        #endregion

        #region Properties

        /// <summary>
        /// Our current weapon
        /// </summary>
        public BaseTankWeapon CurrentWeapon { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Initialise
            Init();
        }

        private void OnEnable()
        {
            if (mHealth)
            {
                mHealth.OnKilled.AddListener(OnKilled);
            }
        }

        private void OnDisable()
        {
            if (mHealth)
            {
                mHealth.OnKilled.RemoveListener(OnKilled);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when we are spawned
        /// </summary>
        public void OnPoolSpawn()
        {
            // Set we can fire
            mCanFire = true;
        }

        /// <summary>
        /// Called when we are de-spawned
        /// </summary>
        public void OnPoolUnSpawn()
        {
            // Set we cannot fire
            mCanFire = false;
        }

        /// <summary>
        /// Fires the weapon
        /// </summary>
        public void FireWeapon()
        {
            // Check we can fire
            if (!mCanFire)
            {
                return;
            }

            // Check if we have a weapon
            if (!CurrentWeapon)
            {
                return;
            }

            // Fire the weapon
            CurrentWeapon.FireWeapon();
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
            if (CurrentWeapon != null)
            {
                Pooling.SendToPool(CurrentWeapon.gameObject);
                CurrentWeapon = null;
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

            // Grab our health component
            mHealth = GetComponent<Health>();

            // Set we can fire
            mCanFire = true;
        }

        /// <summary>
        /// Assigns a new weapon to use
        /// </summary>
        /// <param name="newWeapon"></param>
        private void AssignWeapon(GameObject newWeapon)
        {
            CurrentWeapon = Pooling.GetFromPool(newWeapon, BarrelEndTransform.position, transform.rotation).GetComponent<BaseTankWeapon>();
            CurrentWeapon.transform.SetParent(transform);
            CurrentWeapon.transform.localScale = Vector3.one;
        }

        /// <summary>
        /// Called when we are killed
        /// </summary>
        private void OnKilled()
        {
            // Set we cannot fire
            mCanFire = false;
        }

        #endregion
    }
}