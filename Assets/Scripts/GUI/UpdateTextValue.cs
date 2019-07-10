using UnityEngine;
using UnityEngine.UI;

namespace UnityTankBattalion
{
    public class UpdateTextValue : MonoBehaviour
    {
        #region Private Variables

        /// <summary>
        /// Our text component
        /// </summary>
        private Text mText;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Grab our text component
            mText = GetComponent<Text>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the text value from anm integer
        /// </summary>
        /// <param name="newValue"></param>
        public void SetTextValue(int newValue)
        {
            if (!mText)
            {
                return;
            }

            mText.text = newValue.ToString();
        }

        /// <summary>
        /// Sets the text value from a string
        /// </summary>
        /// <param name="newValue"></param>
        public void SetTextValue(string newValue)
        {
            if (!mText)
            {
                return;
            }

            mText.text = newValue;
        }

        /// <summary>
        /// Sets the text value from a boolean
        /// </summary>
        /// <param name="newValue"></param>
        public void SetTextValue(bool newValue)
        {
            if (!mText)
            {
                return;
            }

            mText.text = newValue ? "True" : "False";
        }

        #endregion
    }
}