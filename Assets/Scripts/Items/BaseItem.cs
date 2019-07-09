using UnityEngine;

namespace UnityTankBattalion
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class BaseItem : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// Valid layers that can pick this item up
        /// </summary>
        public LayerMask ValidPickableLayers;

        #endregion

        #region Unity Methods

        protected void OnTriggerEnter2D(Collider2D other)
        {
            // Check if we have a valid layer
            if ((ValidPickableLayers.value & (1 << other.gameObject.layer)) > 0)
            {
                // Pickup the item
                PickupItem(other.gameObject);

                // Destroy the item
                Pooling.SendToPool(gameObject);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Performs an action when picking up the item
        /// </summary>
        /// <param name="other"></param>
        protected abstract void PickupItem(GameObject other);

        #endregion
    }
}