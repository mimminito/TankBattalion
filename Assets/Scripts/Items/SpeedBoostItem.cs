using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTankBattalion
{
    public class SpeedBoostItem : BaseItem
    {
        #region Public Variables

        /// <summary>
        /// The boosted speed value
        /// </summary>
        [Header("Speed Boost")] public float BoostedSpeedValue = 2f;

        /// <summary>
        /// The duration the boosted speed lasts
        /// </summary>
        public float BoostedSpeedDuration = 10f;

        #endregion

        #region Private Methods

        /// <inheritdoc />
        protected override void PickupItem(GameObject other)
        {
            // Check we have a Tank Movement script
            TankMovement tankMovement = other.GetComponent<TankMovement>();
            if (!tankMovement)
            {
                return;
            }

            // Boost the speed
            tankMovement.BoostSpeed(BoostedSpeedValue, BoostedSpeedDuration);
        }

        #endregion
    }
}