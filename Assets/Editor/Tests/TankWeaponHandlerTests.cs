namespace UnityTankBattalion.Tests
{
    using UnityEngine;
    using UnityEngine.TestTools;
    using System.Collections;
    using NUnit.Framework;

    public class TankWeaponHandlerTests : MonoBehaviour
    {
        #region Public Methods

        [UnityTest]
        public IEnumerator TestWeaponHandlerCanSwitchWeapons()
        {
            // Create a TankWeaponHandler
            GameObject tankWeaponHandlerGO = new GameObject("TankWeaponHandler");
            TankWeaponHandler tankWeaponHandler = tankWeaponHandlerGO.AddComponent<TankWeaponHandler>();
            tankWeaponHandler.runInEditMode = true;

            // Setup TankWeaponHandler
            tankWeaponHandler.BarrelEndTransform = tankWeaponHandlerGO.transform;

            // Create a weapon
            GameObject testWeaponGO = new GameObject("TestWeapon");
            BaseTankWeapon testWeapon = testWeaponGO.AddComponent<ProjectileWeapon>();

            // Ensure we have no weapon to begin with
            Assert.IsTrue(tankWeaponHandler.CurrentWeapon == null);

            // Switch Weapon
            tankWeaponHandler.SwapWeapon(testWeaponGO);

            yield return null;

            // Make sure the new weapon is now active
            Assert.IsTrue(tankWeaponHandler.CurrentWeapon != null);
        }

        #endregion
    }
}