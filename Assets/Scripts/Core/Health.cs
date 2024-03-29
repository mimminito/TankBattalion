﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.PlayerLoop;
using UnityTankBattalion.Events;

namespace UnityTankBattalion
{
    public class Health : MonoBehaviour, IPoolable
    {
        #region Public Variables

        /// <summary>
        /// The maximum health for this object
        /// </summary>
        [Header("Health")] public int MaxHealth = 100;

        /// <summary>
        /// The delay before we are destroyed in the game
        /// </summary>
        [Header("Death")] public float DeathDelay = 2f;

        /// <summary>
        /// Whether to disable collisions when killed
        /// </summary>
        public bool DeathDisableCollisions = true;

        /// <summary>
        /// The points to give when killed
        /// </summary>
        [Header("Points")] public int PointsWhenKilled = 1000;

        /// <summary>
        /// Our animator
        /// </summary>
        [Header("Animations")] public Animator ObjectAnimator;

        /// <summary>
        /// The trigger to fire on the animator when we are killed
        /// </summary>
        public string DeathAnimTrigger;

        /// <summary>
        /// Fired when this object is killed
        /// </summary>
        [Header("Events")] public UnityEvent OnKilled;

        /// <summary>
        /// Fired when we need to give points
        /// </summary>
        public IntEventListener.UnityIntEvent OnGivePoints;

        #endregion

        #region Private Variables

        /// <summary>
        /// Our collider
        /// </summary>
        private Collider2D mCollider2D;

        #endregion

        #region Properties

        /// <summary>
        /// Our current health
        /// </summary>
        public int CurrentHealth { get; private set; }

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
        /// Called when this item is de-spawned
        /// </summary>
        public void OnPoolUnSpawn()
        {
        }

        /// <summary>
        /// Kills this object
        /// </summary>
        public void Kill()
        {
            // Update the animator
            if (ObjectAnimator && !string.IsNullOrEmpty(DeathAnimTrigger))
            {
                ObjectAnimator.SetTrigger(DeathAnimTrigger);
            }

            // Should we turn off collisions
            if (DeathDisableCollisions)
            {
                if (mCollider2D)
                {
                    mCollider2D.enabled = false;
                }
            }

            // Fire the give points event
            FireGivePointsEvent();

            // Fire our Unity Event when we have died
            OnKilled?.Invoke();

            // Should we wait before destroying
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
            if (CurrentHealth <= 0)
            {
                return;
            }

            // Subtract the damage from our health
            CurrentHealth -= damage;

            // Check if we are dead now
            if (CurrentHealth <= 0)
            {
                // Make sure our health is 0
                CurrentHealth = 0;

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
            CurrentHealth = Mathf.Min(CurrentHealth + healthToAdd, MaxHealth);
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
            if (mCollider2D)
            {
                mCollider2D.enabled = true;
            }

            // Set our starting health
            CurrentHealth = MaxHealth;
        }

        /// <summary>
        /// Destroys us
        /// </summary>
        private void DestroyUs()
        {
            Pooling.SendToPool(gameObject);
        }

        /// <summary>
        /// Fires the give points event
        /// </summary>
        private void FireGivePointsEvent()
        {
            // Fire the give points event
            OnGivePoints?.Invoke(PointsWhenKilled);
        }

        #endregion
    }
}