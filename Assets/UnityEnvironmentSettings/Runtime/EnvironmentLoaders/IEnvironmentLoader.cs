using System.Collections.Generic;
using UnityEngine.Events;

namespace Nosirrahh.UnityEnvironmentSettings.Runtime
{
    /// <summary>
    /// Defines the interface for loading and initializing environment settings.
    /// </summary>
    public interface IEnvironmentLoader
    {
        /// <summary>
        /// Initializes the environment loader with the provided settings.
        /// </summary>
        /// <param name="settings">
        /// A dictionary containing key-value pairs representing the initial environment settings.
        /// </param>
        public void Init (Dictionary<string, string> settings);
        /// <summary>
        /// Asynchronously loads the environment settings.
        /// </summary>
        /// <param name="onCompleted">
        /// A callback action to be executed when the loading is completed.
        /// The callback receives an <see cref="EnvironmentSettings"/> object containing the loaded settings.
        /// </param>
        public void Load (UnityAction<EnvironmentSettings> onCompleted);
    }
}