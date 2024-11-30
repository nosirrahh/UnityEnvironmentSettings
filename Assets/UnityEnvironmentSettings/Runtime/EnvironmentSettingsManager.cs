using System;
using UnityEngine;
using UnityEngine.Events;

namespace Nosirrahh.UnityEnvironmentSettings.Runtime
{
    /// <summary>
    /// Manages the loading and centralized access to environment settings.
    /// </summary>
    public static class EnvironmentSettingsManager
    {
        #region Fields

        /// <summary>
        /// The current instance of the loaded environment settings.
        /// </summary>
        private static EnvironmentSettings environmentSettings;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the currently loaded environment settings.
        /// </summary>
        public static EnvironmentSettings EnvironmentSettings { get { return environmentSettings; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the environment settings using the specified loader.
        /// </summary>
        /// <param name="environmentLoader">
        /// An instance of <see cref="IEnvironmentLoader"/> responsible for loading the environment settings.
        /// </param>
        /// <param name="onCompleted">
        /// A callback to be executed once the loading process is complete.
        /// The callback receives an instance of <see cref="EnvironmentSettings"/> representing the loaded settings.
        /// </param>
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