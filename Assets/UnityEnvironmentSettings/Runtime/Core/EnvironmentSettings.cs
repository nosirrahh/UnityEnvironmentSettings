using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Runtime
{
    [Serializable]
    public class EnvironmentSettings
    {
        #region Fields

        /// <summary>
        /// Em construção.
        /// </summary>
        [SerializeField]
        private string environment;
        /// <summary>
        /// Em construção.
        /// </summary>
        [SerializeField]
        private List<KeyValue> settings;

        #endregion

        #region Properties

        /// <summary>
        /// Em construção.
        /// </summary>
        public string Environment { get { return environment; } }

        /// <summary>
        /// Em construção.
        /// </summary>
        public List<KeyValue> Settings { get { return settings; } }

        #endregion

        #region Constructors

        public EnvironmentSettings (string environment)
        {
            this.environment = environment;
            settings = new List<KeyValue> ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Em construção.
        /// </summary>
        /// <param name="key">Em construção.</param>
        /// <param name="value">Em construção.</param>
        /// <returns>Em construção.</returns>
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
        /// Em construção.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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