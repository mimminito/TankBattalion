namespace UnityTankBattalion.Tests
{
    using NUnit.Framework;
    using UnityEditor.SceneManagement;
    using UnityEngine;

    public class HealthTests
    {
        #region Public Methods - Tests

        /// <summary>
        /// Tests whether we can deal damage
        /// </summary>
        /// <returns></returns>
        [Test]
        public void TestDealDamage()
        {
            // Create a health object
            Health health = CreateHealthObject();

            // Deal damage based on their max health
            int damageToDeal = 1;
            health.Damage(damageToDeal);

            // Check the unit has been dealt the correct damage
            Assert.AreEqual(health.CurrentHealth, health.MaxHealth - damageToDeal);
        }

        /// <summary>
        /// Tests dealing damage to kill the health component
        /// </summary>
        [Test]
        public void TestKill()
        {
            // Create a health object
            Health health = CreateHealthObject();

            // Deal damage based on their max health
            int damageToDeal = health.MaxHealth;
            health.Damage(damageToDeal);

            // Check the unit has died
            Assert.AreEqual(health.CurrentHealth, 0);
        }

        /// <summary>
        /// Tests dealing damage and then giving back health
        /// </summary>
        [Test]
        public void TestGiveHealth()
        {
            // Create a health object
            Health health = CreateHealthObject();

            // Deal damage based on their max health
            int damageToDeal = 20;
            health.Damage(damageToDeal);

            // Check our damage has been dealt
            Assert.AreEqual(health.CurrentHealth, health.MaxHealth - damageToDeal);

            // Add health
            health.AddHealth(damageToDeal);

            // Check we health has been gained
            Assert.AreEqual(health.CurrentHealth, health.MaxHealth);
        }

        [Test]
        public void TestCannotAddMoreHealthThanMaxHealth()
        {
            // Create a health object
            Health health = CreateHealthObject();

            // Ensure we are at maximum health
            Assert.AreEqual(health.CurrentHealth, health.MaxHealth);

            // Add health
            health.AddHealth(10);

            // Ensure we are not above our max health
            Assert.AreEqual(health.CurrentHealth, health.MaxHealth);
        }

        [Test]
        public void TestHealthCannotGoBelowZero()
        {
            // Create a health object
            Health health = CreateHealthObject();

            // Deal damage greater than our current health
            int damageToDeal = health.CurrentHealth + 1;
            health.Damage(damageToDeal);

            // Ensure our health is not below zero
            Assert.GreaterOrEqual(health.CurrentHealth, 0);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a health object to be used during testing
        /// </summary>
        /// <returns></returns>
        private Health CreateHealthObject()
        {
            // Create a temp GameObject
            GameObject tempGO = new GameObject("Health");

            // Add health to it
            Health health = tempGO.AddComponent<Health>();

            // Ensure Awake is called
            health.runInEditMode = true;

            // Ensure the component was added
            Assert.IsNotNull(health);

            return health;
        }

        #endregion

        #region Setup Methods

        /// <summary>
        /// Resets the scene for us
        /// </summary>
        [SetUp]
        public void ResetScene()
        {
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
        }

        #endregion
    }
}