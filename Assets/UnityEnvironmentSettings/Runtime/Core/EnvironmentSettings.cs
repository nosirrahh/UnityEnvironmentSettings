using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Runtime
{
    /// <summary>
    /// Represents the environment settings for an application, including specific configuration variables and their context.
    /// </summary>
    [Serializable]
    public class EnvironmentSettings
    {
        #region Fields

        /// <summary>
        /// The name or type of the environment.
        /// </summary>
        [SerializeField]
        private string environment;
        /// <summary>
        /// List of key-value configuration settings specific to the current environment.
        /// </summary>
        [SerializeField]
        private List<KeyValue> settings;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the current environment.
        /// </summary>
        public string Environment { get { return environment; } }

        /// <summary>
        /// Gets the list of key-value pairs representing the environment's settings.
        /// </summary>
        public List<KeyValue> Settings { get { return settings; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="EnvironmentSettings"/> with the specified environment name.
        /// </summary>
        /// <param name="environment">The name of the environment.</param>
        public EnvironmentSettings (string environment)
        {
            this.environment = environment;
            settings = new List<KeyValue> ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds or updates a configuration setting in the environment with the specified key and value.
        /// </summary>
        /// <param name="key">The configuration key.</param>
        /// <param name="value">The value associated with the key.</param>
        public void AddValue (string key, string value)
        {
            try
            {
                KeyValue keyValue = new KeyValue (key, value);
                
                settings ??= new List<KeyValue> ();

                int index = settings.FindIndex ((KeyValue kv) => kv.key == key);
                if (index == -1)
                    settings.Add (keyValue);
                else
                    settings[index] = keyValue;
            }
            catch (Exception exception)
            {
                Debug.LogWarning ($"[{nameof (EnvironmentSettings)}] exception: {exception}");
            }
        }

        /// <summary>
        /// Attempts to retrieve the value associated with a specific key.
        /// </summary>
        /// <param name="key">The configuration key.</param>
        /// <param name="value">When this method returns, contains the value associated with the key, if found; otherwise, the default value for the type.</param>
        /// <returns><c>true</c> if the key was found; otherwise, <c>false</c>.</returns>
        public bool TryGetValue (string key, out string value)
        {
            try
            {
                KeyValue item = settings.Find ((KeyValue kv) => kv.key == key);
                if (item.Equals (default (KeyValue)))
                {
                    value = default;
                    return false;
                }

                value = item.value;
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogWarning ($"[{nameof (EnvironmentSettings)}] exception: {exception}");
                value = default;
                return false;
            }
        }

        #endregion

    }
}