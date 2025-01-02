using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Runtime
{
    /// <summary>
    /// Represents a ScriptableObject that encapsulates environment settings for use in Unity.
    /// </summary>
    [CreateAssetMenu (fileName = "EnvironmentSettings", menuName = "Packages/UnityEnvironmentSettings/EnvironmentSettings", order = 1)]
    public class EnvironmentSettingsScriptableObject : ScriptableObject
    {
        #region Fields

        /// <summary>
        /// Contains the associated environment settings.
        /// </summary>
        [SerializeField]
        private EnvironmentSettings environmentSettings;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the environment settings encapsulated in this ScriptableObject.
        /// </summary>
        public EnvironmentSettings EnvironmentSettings { get { return environmentSettings; } }

        #endregion
    }
}
