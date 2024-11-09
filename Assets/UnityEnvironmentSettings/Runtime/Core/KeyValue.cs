using System;

namespace Nosirrahh.UnityEnvironmentSettings.Runtime
{
    /// <summary>
    /// Em construção.
    /// </summary>
    [Serializable]
    public struct KeyValue
    {
        #region Fields

        /// <summary>
        /// Em construção.
        /// </summary>
        public string key;
        /// <summary>
        /// Em construção.
        /// </summary>
        public string value;

        #endregion

        #region Constructors

        public KeyValue (string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        #endregion
    }
}
