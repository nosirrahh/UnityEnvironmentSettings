using System;
using UnityEngine;
using UnityEngine.Events;

namespace Nosirrahh.UnityEnvironmentSettings.Runtime
{
    /// <summary>
    /// Em construção.
    /// </summary>
    public static class EnvironmentSettingsManager
    {
        #region Fields

        /// <summary>
        /// Em construção.
        /// </summary>
        private static EnvironmentSettings environmentSettings;

        #endregion

        #region Properties

        /// <summary>
        /// Em construção.
        /// </summary>
        public static EnvironmentSettings EnvironmentSettings { get { return environmentSettings; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Em construção.
        /// </summary>
        /// <param name="environmentLoader">Em construção.</param>
        /// <param name="onCompleted">Em construção.</param>
        public static void Load (IEnvironmentLoader environmentLoader, UnityAction<EnvironmentSettings> onCompleted)
        {
            try
            {
                environmentLoader.Load (
                    (EnvironmentSettings settings) =>
                    {
                        environmentSettings = settings == null ? new EnvironmentSettings (string.Empty) : settings;
                        onCompleted?.Invoke (environmentSettings);
                    }
                );
            }
            catch (Exception exception)
            {
                Debug.LogError ($"[{nameof (EnvironmentSettingsManager)}] exception: {exception}");
                onCompleted?.Invoke (environmentSettings);
            }
        }

        #endregion
    }
}