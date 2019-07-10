namespace UnityTankBattalion.Tests
{
    using System.Collections;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;

    public class ExplosionTests
    {
        #region Public Methods

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestExplosionDestroysAfterDelay()
        {
            // Create a new GameObject for the explosion
            GameObject go = new GameObject("Explosion");

            // Add explosion component
            Explosion explosion = go.AddComponent<Explosion>();

            // Set our delay before despawn
            explosion.DelayBeforeDespawn = 0.1f;

            // Ensure we have initialised
            explosion.OnPoolSpawn();

            // Wait until we should have despawned
            yield return new WaitForSeconds(explosion.DelayBeforeDespawn);

            // Wait another 0.1 seconds to ensure Unity has had time to destroy
            yield return new WaitForSeconds(0.1f);

            // Check that our object is now null
            Assert.IsTrue(go == null);
        }

        #endregion
    }
}