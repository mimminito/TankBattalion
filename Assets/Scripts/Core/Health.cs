using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.PlayerLoop;

namespace UnityTankBattalion
{
    public class Health : MonoBehaviour, IPoolable
    {
        #region Public Variables

        [Header("Health")] public int MaxHealth = 100;

        [Header("Death")] public float DeathDelay = 2f;
        public bool DeathDisableCollisions = true;

        [Header("Animations")] public Animator ObjectAnimator;
        public string DeathAnimTrigger;

        [Header("Events")] public UnityEvent OnKilled;

        #endregion

        #region Private Variables

        /// <summary>
        /// Our collider
        /// </summary>
        private Collider2D mCollider2D;

        /// <summary>
        /// Our current health
        /// </summary>
        private int mCurrentHealth;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Init();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when this item is spawned
        /// </summary>
        public void OnPoolSpawn()
        {
            Init();
        }

        /// <summary>
        /// Called when this item is despawned
        /// </summary>
        public void OnPoolUnSpawn()
        {
        }

        /// <summary>
        /// Kills this object
        /// </summary>
        public void Kill()
        {
            if (ObjectAnimator && !string.IsNullOrEmpty(DeathAnimTrigger))
            {
                ObjectAnimator.SetTrigger(DeathAnimTrigger);
            }

            if (DeathDisableCollisions)
            {
                mCollider2D.enabled = false;
            }

            if (DeathDelay > 0f)
            {
                Invoke(nameof(DestroyUs), DeathDelay);
            }
            else
            {
                DestroyUs();
            }
        }

        /// <summary>
        /// Performs damage to this object
        /// </summary>
        /// <param name="damage"></param>
        public void Damage(int damage)
        {
            // Check if we are already dead
            if (mCurrentHealth <= 0)
            {
                return;
            }

            // Subtract the damage from our health
            mCurrentHealth -= damage;

            // Check if we are dead now
            if (mCurrentHealth <= 0)
            {
                // Make sure our health is 0
                mCurrentHealth = 0;

                // Kill
                Kill();
            }
        }

        /// <summary>
        /// Updates the current health by a specific value
        /// </summary>
        /// <param name="healthToAdd"></param>
        public void AddHealth(int healthToAdd)
        {
            mCurrentHealth = Mathf.Min(mCurrentHealth + healthToAdd, MaxHealth);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialises us with health
        /// </summary>
        private void Init()
        {
            // Grab our collider
            mCollider2D = GetComponent<Collider2D>();
            mCollider2D.enabled = true;

            // Set our starting health
            mCurrentHealth = MaxHealth;
        }

        /// <summary>
        /// Destroys us
        /// </summary>
        private void DestroyUs()
        {
            // Fire our Unity Event when we have died
            OnKilled?.Invoke();

            Pooling.SendToPool(gameObject);
        }

        #endregion
    }
}