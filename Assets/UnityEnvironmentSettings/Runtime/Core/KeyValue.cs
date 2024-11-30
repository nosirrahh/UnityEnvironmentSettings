using System;

namespace Nosirrahh.UnityEnvironmentSettings.Runtime
{
    /// <summary>
    /// Represents a key-value pair for storing environment settings.
    /// </summary>
    [Serializable]
    public struct KeyValue
    {
        #region Fields

        /// <summary>
        /// The key that uniquely identifies a setting.
        /// </summary>
        public string key;
        /// <summary>
        /// The value associated with the key.
        /// </summary>
        public string value;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValue"/> structure with the specified key and value.
        /// </summary>
        /// <param name="key">
        /// The configuration key.
        /// </param>
        /// <param name="value">
        /// The value associated with the key.
        /// </param>
        public KeyValue (string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        #endregion
    }
}
