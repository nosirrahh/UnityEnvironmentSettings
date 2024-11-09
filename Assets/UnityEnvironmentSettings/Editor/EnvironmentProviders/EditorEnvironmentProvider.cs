using Nosirrahh.UnityEnvironmentSettings.Runtime;
using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Em construção.
    /// </summary>
    [System.Serializable]
    public class EditorEnvironmentProvider : IEnvironmentProvider
    {
        /// <summary>
        /// Em construção.
        /// </summary>
        [SerializeField]
        private EnvironmentSettingsScriptableObject reference;

        /// <summary>
        /// Em construção.
        /// </summary>
        /// <param name="environmentSettings"><inheritdoc></inheritdoc></param>
        public void GetEnvironmentSettings (out EnvironmentSettings environmentSettings)
        {
            environmentSettings = reference.EnvironmentSettings;
        }
    }
}