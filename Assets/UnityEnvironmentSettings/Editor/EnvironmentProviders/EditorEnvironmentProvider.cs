using Nosirrahh.UnityEnvironmentSettings.Runtime;
using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Implementation of <see cref="IEnvironmentProvider"/> that provides environment settings
    /// from a reference to an <see cref="EnvironmentSettingsScriptableObject"/>.
    /// This class is used to load environment settings directly in the editor.
    /// </summary>
    [System.Serializable]
    public class EditorEnvironmentProvider : IEnvironmentProvider
    {
        /// <summary>
        /// Reference to the <see cref="EnvironmentSettingsScriptableObject"/> containing the environment settings.
        /// </summary>
        [SerializeField]
        private EnvironmentSettingsScriptableObject reference;

        /// <summary>
        /// Retrieves the environment settings from the <see cref="reference"/>.
        /// </summary>
        /// <param name="environmentSettings">The environment settings object that will be populated with settings stored in <see cref="reference"/>.</param>
        public void GetEnvironmentSettings (out EnvironmentSettings environmentSettings)
        {
            environmentSettings = reference.EnvironmentSettings;
        }
    }
}