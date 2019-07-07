using UnityEngine;

namespace UnityTankBattalion
{
    /// <summary>
    /// Singleton class for MonoBehaviours
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        #region Private Variables

        /// <summary>
        /// The singleton
        /// </summary>
        private static T sInstance;

        #endregion

        #region Properties

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static T Instance
        {
            get
            {
                if (sInstance == null)
                {
                    sInstance = FindObjectOfType<T>();

                    if (sInstance == null)
                    {
                        GameObject go = new GameObject();
                        sInstance = go.AddComponent<T>();
                    }
                }

                return sInstance;
            }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Check to make sure we are playing
            if (!Application.isPlaying)
            {
                return;
            }

            sInstance = this as T;
        }

        #endregion
    }
}