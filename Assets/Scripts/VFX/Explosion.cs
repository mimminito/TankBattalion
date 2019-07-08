using System.Collections;
using UnityEngine;

namespace UnityTankBattalion
{
    public class Explosion : MonoBehaviour, IPoolable
    {
        #region Public Variables

        /// <summary>
        /// Our animator
        /// </summary>
        public Animator Anim;

        /// <summary>
        /// Trigger to be called when we explode
        /// </summary>
        public string ExplosionAnimTrigger;

        /// <summary>
        /// The delay before we despawn
        /// </summary>
        public float DelayBeforeDespawn = 2f;

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when we are spawned
        /// </summary>
        public void OnPoolSpawn()
        {
            if (Anim)
            {
                Anim.SetTrigger(ExplosionAnimTrigger);
            }

            // Wait until we need to despawn
            StartCoroutine(DelayDespawn());
        }

        /// <summary>
        /// Called when we de-spawn
        /// </summary>
        public void OnPoolUnSpawn()
        {
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Delays the despawn
        /// </summary>
        /// <returns></returns>
        private IEnumerator DelayDespawn()
        {
            // Wait before we despawn
            yield return new WaitForSeconds(DelayBeforeDespawn);

            // Despawn
            Pooling.SendToPool(gameObject);
        }

        #endregion
    }
}