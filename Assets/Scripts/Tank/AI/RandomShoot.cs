using UnityEngine;

namespace UnityTankBattalion
{
    [RequireComponent(typeof(TankWeapon))]
    public class RandomShoot : MonoBehaviour, IPoolable
    {
        #region Public Variables

        /// <summary>
        /// The minimum amount of time that can pass before we randomise shooting
        /// </summary>
        public float MinFrequency = 3f;

        /// <summary>
        /// The maximum amount of time that can pass before we randomise shooting
        /// </summary>
        public float MaxFrequency = 5f;

        #endregion

        #region Private Variables

        /// <summary>
        /// Our timer
        /// </summary>
        private float mTimer;

        /// <summary>
        /// Whether we can shoot
        /// </summary>
        private bool mCanShoot;

        /// <summary>
        /// Our weapon
        /// </summary>
        private TankWeapon mTankWeapon;

        /// <summary>
        /// Our health
        /// </summary>
        private Health mHealth;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Initialise this component
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

        private void Update()
        {
            // Update our timer
            UpdateTimer();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when we are spawned
        /// </summary>
        public void OnPoolSpawn()
        {
            // Set we can shoot
            mCanShoot = true;

            // Get our next timer value
            DetermineNextFrequency();
        }

        /// <summary>
        /// Called when we are de-spawned
        /// </summary>
        public void OnPoolUnSpawn()
        {
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialise this component
        /// </summary>
        private void Init()
        {
            // Grab our components
            mTankWeapon = GetComponent<TankWeapon>();
            mHealth = GetComponent<Health>();

            // If we do not have a weapon attached then return out
            if (!mTankWeapon)
            {
                return;
            }

            // Set we can shoot
            mCanShoot = true;

            // Get our next timer value
            DetermineNextFrequency();
        }

        /// <summary>
        /// Updates our timer
        /// </summary>
        private void UpdateTimer()
        {
            // If we cannot shoot return out
            if (!mCanShoot)
            {
                return;
            }

            // Update our timer
            mTimer -= Time.deltaTime;

            // Check if our timer has elapsed
            if (mTimer <= 0f)
            {
                // Randomise our timer
                DetermineNextFrequency();

                // Decide on some movement
                FireWeapon();
            }
        }

        /// <summary>
        /// Fires our weapon
        /// </summary>
        /// <returns></returns>
        private void FireWeapon()
        {
            // Check we have a weapon
            if (!mTankWeapon)
            {
                return;
            }

            // Fire the weapon
            mTankWeapon.FireWeapon();
        }

        /// <summary>
        /// Determines our next frequency value for the timer
        /// </summary>
        /// <returns></returns>
        private void DetermineNextFrequency()
        {
            mTimer = Random.Range(MinFrequency, MaxFrequency);
        }

        /// <summary>
        /// Called when we have been killed
        /// </summary>
        private void OnKilled()
        {
            // Set we cannot shoot
            mCanShoot = false;

            // Reset our timer
            mTimer = 0f;
        }

        #endregion
    }
}