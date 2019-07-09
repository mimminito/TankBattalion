using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityTankBattalion
{
    public class GiveLifeItem : BaseItem
    {
        #region Public Variables

        [Header("Unity Events")] public UnityEvent OnGiveLife;

        #endregion

        #region Private Methods

        /// <inheritdoc />
        protected override void PickupItem(GameObject other)
        {
            // Fire our event
            OnGiveLife?.Invoke();
        }

        #endregion
    }
}